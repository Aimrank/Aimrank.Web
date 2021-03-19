import { Ref } from "vue";
import { useQuery } from "~/graphql/useQuery";
import { useMutation } from "~/graphql/useMutation";
import { useSubscription } from "~/graphql/useSubscription";
import {
  AcceptLobbyInvitationMutation,
  AcceptLobbyInvitationMutationVariables,
  AcceptMatchMutation,
  AcceptMatchMutationVariables,
  CancelLobbyInvitationMutation,
  CancelLobbyInvitationMutationVariables,
  ChangeLobbyConfigurationMutation,
  ChangeLobbyConfigurationMutationVariables,
  CreateLobbyMutation,
  CreateLobbyMutationVariables,
  GetFriendsQuery,
  GetFriendsQueryVariables,
  GetLobbyInvitationsQuery,
  GetLobbyInvitationsQueryVariables,
  GetLobbyQuery,
  GetLobbyQueryVariables,
  InviteUserToLobbyMutation,
  InviteUserToLobbyMutationVariables,
  LeaveLobbyMutation,
  LeaveLobbyMutationVariables,
  LobbyConfigurationChangedSubscription,
  LobbyConfigurationChangedSubscriptionVariables,
  LobbyInvitationAcceptedSubscription,
  LobbyInvitationAcceptedSubscriptionVariables,
  LobbyInvitationCreatedSubscription,
  LobbyInvitationCreatedSubscriptionVariables,
  LobbyMemberLeftSubscription,
  LobbyMemberLeftSubscriptionVariables,
  LobbyMemberRoleChangedSubscription,
  LobbyMemberRoleChangedSubscriptionVariables,
  LobbyStatusChangedSubscription,
  LobbyStatusChangedSubscriptionVariables,
  MatchAcceptedSubscription,
  MatchAcceptedSubscriptionVariables,
  MatchCanceledSubscription,
  MatchCanceledSubscriptionVariables,
  MatchFinishedSubscription,
  MatchFinishedSubscriptionVariables,
  MatchPlayerLeftSubscription,
  MatchPlayerLeftSubscriptionVariables,
  MatchReadySubscription,
  MatchReadySubscriptionVariables,
  MatchStartedSubscription,
  MatchStartedSubscriptionVariables,
  MatchStartingSubscription,
  MatchStartingSubscriptionVariables,
  MatchTimedOutSubscription,
  MatchTimedOutSubscriptionVariables,
  StartSearchingForGameMutation,
  StartSearchingForGameMutationVariables,
} from "~/graphql/types/types";

// Query

import GET_LOBBY from "./query/getLobby.gql";
import GET_LOBBY_INVITATIONS from "./query/getLobbyInvitations.gql";
import GET_FRIENDS from "./query/getFriends.gql";

export const useGetLobby = () => useQuery<
  GetLobbyQuery,
  GetLobbyQueryVariables>({
    query: GET_LOBBY
  });
export const useGetLobbyInvitations = () => useQuery<
  GetLobbyInvitationsQuery,
  GetLobbyInvitationsQueryVariables>({
    query: GET_LOBBY_INVITATIONS
  });

export const useGetFriends = (
  userId: string | Ref<string>
) => useQuery<
  GetFriendsQuery,
  GetFriendsQueryVariables>({
    query: GET_FRIENDS,
    variables: {
      userId
    }
  }, true);

// Mutations

import ACCEPT_LOBBY_INVITATION from "./mutations/acceptLobbyInvitation.gql";
import CANCEL_LOBBY_INVITATION from "./mutations/cancelLobbyInvitation.gql";
import CREATE_LOBBY from "./mutations/createLobby.gql";
import LEAVE_LOBBY from "./mutations/leaveLobby.gql";
import INVITE_USER_TO_LOBBY from "./mutations/inviteUserToLobby.gql";
import CHANGE_LOBBY_CONFIGURATION from "./mutations/changeLobbyConfiguration.gql";
import START_SEARCHING_FOR_GAME from "./mutations/startSearchingForGame.gql";
import ACCEPT_MATCH from "./mutations/acceptMatch.gql";

export const useAcceptLobbyInvitation = () => useMutation<
  AcceptLobbyInvitationMutation,
  AcceptLobbyInvitationMutationVariables>(ACCEPT_LOBBY_INVITATION);

export const useCancelLobbyInvitation = () => useMutation<
  CancelLobbyInvitationMutation,
  CancelLobbyInvitationMutationVariables>(CANCEL_LOBBY_INVITATION);

export const useCreateLobby = () => useMutation<
  CreateLobbyMutation,
  CreateLobbyMutationVariables>(CREATE_LOBBY);

export const useLeaveLobby = () => useMutation<
  LeaveLobbyMutation,
  LeaveLobbyMutationVariables>(LEAVE_LOBBY);

export const useInviteUserToLobby = () => useMutation<
  InviteUserToLobbyMutation,
  InviteUserToLobbyMutationVariables>(INVITE_USER_TO_LOBBY);

export const useChangeLobbyConfiguration = () => useMutation<
  ChangeLobbyConfigurationMutation,
  ChangeLobbyConfigurationMutationVariables>(CHANGE_LOBBY_CONFIGURATION);

export const useStartSearchingForGame = () => useMutation<
  StartSearchingForGameMutation,
  StartSearchingForGameMutationVariables>(START_SEARCHING_FOR_GAME);

export const useAcceptMatch = () => useMutation<
  AcceptMatchMutation,
  AcceptMatchMutationVariables>(ACCEPT_MATCH);

// Subscriptions

import LOBBY_INVITATION_CREATED from "./subscriptions/lobbyInvitationCreated.gql";
import LOBBY_INVITATION_ACCEPTED from "./subscriptions/lobbyInvitationAccepted.gql";
import LOBBY_CONFIGURATION_CHANGED from "./subscriptions/lobbyConfigurationChanged.gql";
import LOBBY_MEMBER_LEFT from "./subscriptions/lobbyMemberLeft.gql";
import LOBBY_MEMBER_ROLE_CHANGED from "./subscriptions/lobbyMemberRoleChanged.gql";
import LOBBY_STATUS_CHANGED from "./subscriptions/lobbyStatusChanged.gql";
import MATCH_ACCEPTED from "./subscriptions/matchAccepted.gql";
import MATCH_CANCELED from "./subscriptions/matchCanceled.gql";
import MATCH_FINISHED from "./subscriptions/matchFinished.gql";
import MATCH_PLAYER_LEFT from "./subscriptions/matchPlayerLeft.gql";
import MATCH_READY from "./subscriptions/matchReady.gql";
import MATCH_STARTED from "./subscriptions/matchStarted.gql";
import MATCH_STARTING from "./subscriptions/matchStarting.gql";
import MATCH_TIMED_OUT from "./subscriptions/matchTimedOut.gql";

export const useLobbyInvitationCreated = () => useSubscription<
  LobbyInvitationCreatedSubscription,
  LobbyInvitationCreatedSubscriptionVariables>({ query: LOBBY_INVITATION_CREATED });

export const useLobbyInvitationAccepted = () => useSubscription<
  LobbyInvitationAcceptedSubscription,
  LobbyInvitationAcceptedSubscriptionVariables>({
    query: LOBBY_INVITATION_ACCEPTED
  }, true);

export const useLobbyConfigurationChanged = () => useSubscription<
  LobbyConfigurationChangedSubscription,
  LobbyConfigurationChangedSubscriptionVariables>({
    query: LOBBY_CONFIGURATION_CHANGED
  }, true);

export const useLobbyMemberLeft = () => useSubscription<
  LobbyMemberLeftSubscription,
  LobbyMemberLeftSubscriptionVariables>({
    query: LOBBY_MEMBER_LEFT
  }, true);

export const useLobbyMemberRoleChanged = () => useSubscription<
  LobbyMemberRoleChangedSubscription,
  LobbyMemberRoleChangedSubscriptionVariables>({
    query: LOBBY_MEMBER_ROLE_CHANGED
  }, true);

export const useLobbyStatusChanged = () => useSubscription<
  LobbyStatusChangedSubscription,
  LobbyStatusChangedSubscriptionVariables>({
    query: LOBBY_STATUS_CHANGED
  }, true);

export const useMatchAccepted = (lobbyId: string) => useSubscription<
  MatchAcceptedSubscription,
  MatchAcceptedSubscriptionVariables>({ query: MATCH_ACCEPTED, variables: {lobbyId} });

export const useMatchCanceled = (lobbyId: string) => useSubscription<
  MatchCanceledSubscription,
  MatchCanceledSubscriptionVariables>({ query: MATCH_CANCELED, variables: {lobbyId} });

export const useMatchFinished = (lobbyId: string) => useSubscription<
  MatchFinishedSubscription,
  MatchFinishedSubscriptionVariables>({ query: MATCH_FINISHED, variables: {lobbyId} });

export const useMatchPlayerLeft = (lobbyId: string) => useSubscription<
  MatchPlayerLeftSubscription,
  MatchPlayerLeftSubscriptionVariables>({ query: MATCH_PLAYER_LEFT, variables: {lobbyId} });

export const useMatchReady = (lobbyId: string) => useSubscription<
  MatchReadySubscription,
  MatchReadySubscriptionVariables>({ query: MATCH_READY, variables: {lobbyId} });

export const useMatchStarted = (lobbyId: string) => useSubscription<
  MatchStartedSubscription,
  MatchStartedSubscriptionVariables>({ query: MATCH_STARTED, variables: {lobbyId} });

export const useMatchStarting = (lobbyId: string) => useSubscription<
  MatchStartingSubscription,
  MatchStartingSubscriptionVariables>({ query: MATCH_STARTING, variables: {lobbyId} });

export const useMatchTimedOut = (lobbyId: string) => useSubscription<
  MatchTimedOutSubscription,
  MatchTimedOutSubscriptionVariables>({ query: MATCH_TIMED_OUT, variables: {lobbyId} });
