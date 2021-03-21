import { ref } from "vue";
import { GraphQLError } from "graphql";
import { ApolloClient, MutationOptions, NormalizedCacheObject } from "@apollo/client/core";

type MutationErrorCallback = (errors?: readonly GraphQLError[]) => void;
type MutationDoneCallback<T> = (result: T) => void;

export type UseMutationOptions<TVariables> = MutationOptions<any, TVariables>;

export const useMutation = <T = any, TVariables = Record<string, any>>(
  apolloClient: ApolloClient<NormalizedCacheObject>,
  options: UseMutationOptions<TVariables>
) => {
  const errors = ref<readonly GraphQLError[]>([]);
  const result = ref<T>();
  const loading = ref(false);

  let onDoneCallback: MutationDoneCallback<T> | undefined;
  let onErrorCallback: MutationErrorCallback | undefined;

  const onDone = (callback: MutationDoneCallback<T>) => onDoneCallback = callback;
  const onError = (callback: MutationErrorCallback) => onErrorCallback = callback;

  const createResult = () => ({
    success: errors.value.length === 0,
    errors: errors.value,
    result: result.value
  });

  const mutate = async (variables?: TVariables) => {
    loading.value = true;

    try {
      const res = await apolloClient.mutate({
        ...options,
        variables: variables ?? options.variables
      });

      loading.value = false;

      if (res.errors) {
        errors.value = res.errors;

        if (onErrorCallback) {
          onErrorCallback(res.errors);
        }

        return createResult();
      }

      if (res.data) {
        result.value = res.data;
      }

      if (onDoneCallback) {
        onDoneCallback(res.data);
      }

      return createResult();
    } catch (error) {
      loading.value = false;

      if (error.graphQLErrors) {
        errors.value = error.graphQLErrors;
      }

      if (onErrorCallback) {
        onErrorCallback();
      }

      return createResult();
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
