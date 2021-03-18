import { onBeforeUnmount, ref } from "vue";
import { GraphQLError } from "graphql";
import { SubscriptionOptions } from "@apollo/client/core";
import { apolloClient } from "~/graphql/apolloClient";

export const useSubscription = <T = any, TVariables = Record<string, any>>(
  options: SubscriptionOptions<TVariables>
) => {
  const errors = ref<readonly GraphQLError[]>([]);
  const result = ref<T>();

  const subscription = apolloClient.subscribe(options)
    .subscribe(res => {
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

        return;
      }

      if (res.errors) {
        errors.value = res.errors;
      }

      if (res.data) {
        result.value = res.data;
      }
    });

  const unsubscribe = () => {
    if (subscription && !subscription.closed) {
      subscription.unsubscribe();
    }
  }

  onBeforeUnmount(() => unsubscribe());

  return {
    errors,
    result
  };
}
