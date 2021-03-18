import { ref } from "vue";
import { GraphQLError } from "graphql";
import { DocumentNode, MutationOptions } from "@apollo/client/core";
import { apolloClient } from "~/graphql/apolloClient";

export const useMutation = <T = any, TVariables = Record<string, any>>(
  mutation: DocumentNode,
  options?: MutationOptions<any, TVariables>
) => {
  const errors = ref<readonly GraphQLError[]>([]);
  const result = ref<T>();
  const loading = ref(false);

  let onDoneCallback: (result: T) => void = () => {}
  let onErrorCallback: (errors?: readonly GraphQLError[]) => void = () => {}

  const onDone = (callback: (result: T) => void) => onDoneCallback = callback;
  const onError = (callback: (errors?: readonly GraphQLError[]) => void) => onErrorCallback = callback;

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
        onErrorCallback(res.errors);
        return;
      }

      if (res.data) {
        result.value = res.data;
      }

      onDoneCallback(res.data);

      return result.value;
    } catch (error) {
      loading.value = false;

      if (error.graphQLErrors) {
        errors.value = error.graphQLErrors;
      }

      onErrorCallback();
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
