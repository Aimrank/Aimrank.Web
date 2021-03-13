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

  let onDoneCallback: () => void = () => {}
  let onErrorCallback: () => void = () => {}

  const onDone = (callback: () => void) => onDoneCallback = callback;
  const onError = (callback: () => void) => onErrorCallback = callback;

  const mutate = async (variables?: TVariables) => {
    loading.value = true;

    const res = await apolloClient.mutate({
      mutation,
      variables,
      ...(options ?? {})
    });

    loading.value = false;

    if (res.errors) {
      errors.value = res.errors;
      onErrorCallback();
      return;
    }

    if (res.data) {
      result.value = res.data;
    }

    onDoneCallback();
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
