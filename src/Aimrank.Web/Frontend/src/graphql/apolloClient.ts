import { OperationDefinitionNode } from "graphql";
import { SubscriptionClient } from "subscriptions-transport-ws";
import { ApolloClient, concat, HttpLink, InMemoryCache, split } from "@apollo/client/core";
import { getMainDefinition, storeKeyNameFromField } from "@apollo/client/utilities";
import { WebSocketLink } from "@apollo/client/link/ws";
import { onError } from "@apollo/client/link/error";

const httpLink = new HttpLink({
  uri: `${window.location.origin}/graphql`
});

const subscriptionClient = new SubscriptionClient(
  `ws://${window.location.host}/graphql`,
  {
    reconnect: true
  }
);

const wsLink = concat(
  onError(({ response }) => {
    if (response &&
        response.errors &&
        response.errors[0] &&
        response.errors[0].extensions?.code === "HC0008")
    {
      response.errors = [];
      response.extensions = {
        ...response.extensions,
        unauthorized: true
      };
    }
  }),
  new WebSocketLink(subscriptionClient)
);

const link = split(
  ({ query }) => {
    const { kind, operation } = getMainDefinition(query) as OperationDefinitionNode;
    return kind === "OperationDefinition" && operation === "subscription";
  },
  wsLink,
  httpLink
);

export const apolloClient = new ApolloClient({
  link,
  cache: new InMemoryCache()
});

export const reconnect = () => {
  // Close websocket connection. It will reconnect automatically.
  subscriptionClient.close(true);
}
