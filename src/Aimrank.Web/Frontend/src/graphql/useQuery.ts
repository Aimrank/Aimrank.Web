import { ref } from "vue";
import { GraphQLError } from "graphql";
import { QueryOptions } from "@apollo/client/core";
import { apolloClient } from "~/graphql/apolloClient";

export const useQuery = <T = any, TVariables = Record<string, any>>(
  options: QueryOptions<TVariables>
) => {
  const errors = ref<readonly GraphQLError[]>([]);
  const result = ref<T>();
  const loading = ref(false);

  let onDoneCallback: () => void = () => {}
  let onErrorCallback: () => void = () => {}

  const onDone = (callback: () => void) => onDoneCallback = callback;
  const onError = (callback: () => void) => onErrorCallback = callback;

  const fetch = async () => {
    loading.value = true;

    const res = await apolloClient.query(options);

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

  fetch();

  return {
    fetch,
    errors,
    result,
    loading,
    onDone,
    onError
  };
}
