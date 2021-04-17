import { ref } from "vue";
import { GraphQLError } from "graphql";
import { ApolloClient, NormalizedCacheObject, QueryOptions } from "@apollo/client/core";

type QueryDoneCallback = () => void;
type QueryErrorCallback = () => void;

export type UseQueryOptions<TVariables> = QueryOptions<TVariables> & { lazy?: boolean };

export const useQuery = <T = any, TVariables = Record<string, any>>(
  apolloClient: ApolloClient<NormalizedCacheObject>,
  options: UseQueryOptions<TVariables>
) => {
  const errors = ref<readonly GraphQLError[]>([]);
  const result = ref<T>();
  const loading = ref(false);

  let onDoneCallback: QueryDoneCallback | undefined;
  let onErrorCallback: QueryErrorCallback | undefined;

  const onDone = (callback: QueryDoneCallback) => onDoneCallback = callback;
  const onError = (callback: QueryErrorCallback) => onErrorCallback = callback;

  const fetch = async () => {
    loading.value = true;

    const res = await apolloClient.query(options);

    loading.value = false;

    if (res.errors) {
      errors.value = res.errors;

      if (onErrorCallback) {
        onErrorCallback();
      }

      return;
    }

    if (res.data) {
      result.value = res.data;
    }

    if (onDoneCallback) {
      onDoneCallback();
    }
  }

  if (!options.lazy) {
    fetch();
  }

  return {
    fetch,
    errors,
    result,
    loading,
    onDone,
    onError
  };
}
