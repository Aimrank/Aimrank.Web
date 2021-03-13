import { OperationDefinitionNode } from "graphql";
import { ApolloClient, HttpLink, InMemoryCache, split } from "@apollo/client/core";
import { getMainDefinition } from "@apollo/client/utilities";
import { WebSocketLink } from "@apollo/client/link/ws";

const httpLink = new HttpLink({
  uri: `${window.location.origin}/graphql`
});

const wsLink = new WebSocketLink({
  uri: `ws://${window.location.host}/graphql`,
  options: {
    reconnect: true
  }
});

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
  cache: new InMemoryCache(),
  connectToDevTools: true
});
