import { ref } from "vue";
import { GraphQLError } from "graphql";
import { DocumentNode, SubscriptionOptions } from "@apollo/client/core";
import { apolloClient } from "~/graphql/apolloClient";

export const useSubscription = <T = any, TVariables = Record<string, any>>(
  query: DocumentNode,
  options?: SubscriptionOptions<TVariables>
) => {
  const errors = ref<readonly GraphQLError[]>([]);
  const result = ref<T>();

  let subscription: ZenObservable.Subscription;

  const subscribe = (variables?: TVariables) => {
    subscription = apolloClient.subscribe({
      query,
      variables,
      ...(options ?? {})
    })
      .subscribe(res => {
        errors.value = [];

        if (res.errors) {
          errors.value = res.errors;
        }

        if (res.data) {
          result.value = res.data;
        }
      });
  }

  const unsubscribe = () => subscription && subscription.unsubscribe();

  return {
    subscribe,
    unsubscribe,
    errors,
    result
  };
}
