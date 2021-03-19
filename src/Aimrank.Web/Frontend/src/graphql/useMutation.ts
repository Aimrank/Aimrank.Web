import { ref } from "vue";
import { GraphQLError } from "graphql";
import { DocumentNode, MutationOptions } from "@apollo/client/core";
import { apolloClient } from "~/graphql/apolloClient";

type MutationErrorCallback = (errors?: readonly GraphQLError[]) => void;
type MutationDoneCallback<T> = (result: T) => void;

export const useMutation = <T = any, TVariables = Record<string, any>>(
  mutation: DocumentNode,
  options?: MutationOptions<any, TVariables>
) => {
  const errors = ref<readonly GraphQLError[]>([]);
  const result = ref<T>();
  const loading = ref(false);

  let onDoneCallback: MutationDoneCallback<T> | undefined;
  let onErrorCallback: MutationErrorCallback | undefined;

  const onDone = (callback: MutationDoneCallback<T>) => onDoneCallback = callback;
  const onError = (callback: MutationErrorCallback) => onErrorCallback = callback;

  const mutate = async (variables?: TVariables) => {
    loading.value = true;

    try {
      const res = await apolloClient.mutate({
        mutation,
        variables,
        ...(options ?? {})
      });

      loading.value = false;

      if (res.errors) {
        errors.value = res.errors;

        if (onErrorCallback) {
          onErrorCallback(res.errors);
        }

        return;
      }

      if (res.data) {
        result.value = res.data;
      }

      if (onDoneCallback) {
        onDoneCallback(res.data);
      }

      return result.value;
    } catch (error) {
      loading.value = false;

      if (error.graphQLErrors) {
        errors.value = error.graphQLErrors;
      }

      if (onErrorCallback) {
        onErrorCallback();
      }
    }
  }

  return {
    mutate,
    errors,
    result,
    loading,
    onDone,
    onError
  };
}
