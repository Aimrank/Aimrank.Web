import { onBeforeUnmount, ref } from "vue";
import { GraphQLError } from "graphql";
import { FetchResult, SubscriptionOptions } from "@apollo/client/core";
import { apolloClient } from "~/graphql/apolloClient";

type SubscriptionErrorCallback = (errors?: readonly GraphQLError[]) => void;
type SubscriptionResultCallback<T> = (result: T) => void;

export const useSubscription = <T = any, TVariables = Record<string, any>>(
  options: SubscriptionOptions<TVariables>,
  lazy = false
) => {
  const errors = ref<readonly GraphQLError[]>([]);
  const result = ref<T>();

  let onResultCallback: SubscriptionResultCallback<T> | undefined;
  let onErrorCallback: SubscriptionErrorCallback | undefined;

  const onResult = (callback: SubscriptionResultCallback<T>) => onResultCallback = callback;
  const onError = (callback: SubscriptionErrorCallback) => onErrorCallback = callback;

  let subscription: ZenObservable.Subscription | undefined;

  const subscriptionHandler = (res: FetchResult<any, Record<string, any>, Record<string, any>>) => {
    errors.value = [];

    if (res.extensions?.unauthorized) {
      errors.value = [
        {
          name: "unauthorized",
          path: [],
          nodes: [],
          locations: [],
          positions: [],
          originalError: null,
          message: "Unauthorized",
          extensions: {
            message: "Unauthorized",
            code: "unauthorized"
          },
          source: {
            name: "",
            body: "",
            locationOffset: {
              line: 0,
              column: 0
            }
          }
        }
      ];

      if (onErrorCallback) {
        onErrorCallback(errors.value);
      }

      return;
    }

    if (res.errors) {
      errors.value = res.errors;

      if (onErrorCallback) {
        onErrorCallback(errors.value);
      }

      return;
    }

    if (res.data) {
      result.value = res.data;

      if (onResultCallback) {
        onResultCallback(res.data);
      }

      return;
    }
  }

  const subscribe = (variables?: TVariables) => {
    if (subscription) {
      return;
    }

    subscription = apolloClient
      .subscribe({
        variables,
        ...options
      })
      .subscribe(subscriptionHandler);
  }

  const unsubscribe = () => {
    if (subscription && !subscription.closed) {
      subscription.unsubscribe();
      subscription = undefined;
    }
  }

  if (!lazy) {
    subscribe();
  }

  onBeforeUnmount(() => unsubscribe());

  return {
    subscribe,
    unsubscribe,
    errors,
    result,
    onResult,
    onError
  };
}
