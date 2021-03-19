import { ref } from "vue";
import { GraphQLError } from "graphql";
import { QueryOptions } from "@apollo/client/core";
import { apolloClient } from "~/graphql/apolloClient";

type QueryDoneCallback = () => void;
type QueryErrorCallback = () => void;

export const useQuery = <T = any, TVariables = Record<string, any>>(
  options: QueryOptions<TVariables>,
  lazy = false
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

  if (!lazy) {
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
