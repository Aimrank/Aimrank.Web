import { useQuery } from "~/graphql/useQuery";
import { useMutation } from "~/graphql/useMutation";
import { useSubscription } from "~/graphql/useSubscription";
import {
  AcceptLobbyInvitationMutation,
  AcceptLobbyInvitationMutationVariables,
  CancelLobbyInvitationMutation,
  CancelLobbyInvitationMutationVariables,
  GetLobbyInvitationsQuery,
  GetLobbyInvitationsQueryVariables,
  LobbyInvitationCreatedSubscription,
  LobbyInvitationCreatedSubscriptionVariables,
} from "~/graphql/types/types";

import GET_LOBBY_INVITATIONS from "./query/getLobbyInvitations.gql";

import ACCEPT_LOBBY_INVITATION from "./mutations/acceptLobbyInvitation.gql";
import CANCEL_LOBBY_INVITATION from "./mutations/cancelLobbyInvitation.gql";

import LOBBY_INVITATION_CREATED from "./subscriptions/lobbyInvitationCreated.gql";

export const useGetLobbyInvitations = () => useQuery<
  GetLobbyInvitationsQuery,
  GetLobbyInvitationsQueryVariables>({
    query: GET_LOBBY_INVITATIONS
  });

export const useAcceptLobbyInvitation = () => useMutation<
  AcceptLobbyInvitationMutation,
  AcceptLobbyInvitationMutationVariables>(ACCEPT_LOBBY_INVITATION);

export const useCancelLobbyInvitation = () => useMutation<
  CancelLobbyInvitationMutation,
  CancelLobbyInvitationMutationVariables>(CANCEL_LOBBY_INVITATION);

export const useLobbyInvitationCreated = () => useSubscription<
  LobbyInvitationCreatedSubscription,
  LobbyInvitationCreatedSubscriptionVariables>({
    query: LOBBY_INVITATION_CREATED
  });