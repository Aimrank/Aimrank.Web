import { isRef } from "vue";
import { OperationDefinitionNode } from "graphql";
import { SubscriptionClient } from "subscriptions-transport-ws";
import { ApolloClient, ApolloLink, HttpLink, InMemoryCache, NextLink, Operation, split } from "@apollo/client/core";
import { getMainDefinition } from "@apollo/client/utilities";
import { WebSocketLink } from "@apollo/client/link/ws";

const httpLink = new HttpLink({
  uri: `${window.location.origin}/graphql`
});

class VueReactiveMiddlewareLink extends ApolloLink {
  request(operation: Operation, forward: NextLink) {
    for (const variable in operation.variables) {
      const value = operation.variables[variable];

      if (isRef(value)) {
        operation.variables[variable] = value.value;
      }
    }

    return forward(operation);
  }
}

const subscriptionClient = new SubscriptionClient(
  `${window.location.protocol.includes('https') ? 'wss' : 'ws'}://${window.location.host}/graphql`,
  {
    reconnect: true
  }
);

const wsLink = new WebSocketLink(subscriptionClient);

const link = split(
  ({ query }) => {
    const { kind, operation } = getMainDefinition(query) as OperationDefinitionNode;
    return kind === "OperationDefinition" && operation === "subscription";
  },
  wsLink,
  httpLink
);

export const apolloClient = new ApolloClient({
  link: ApolloLink.from([
    new VueReactiveMiddlewareLink(),
    link
  ]),
  cache: new InMemoryCache(),
  defaultOptions: {
    query: {
      fetchPolicy: "no-cache"
    },
    mutate: {
      fetchPolicy: "no-cache"
    }
  }
});

export const reconnect = () => {
  // Close websocket connection. It will reconnect automatically.
  subscriptionClient.close(true);
}
