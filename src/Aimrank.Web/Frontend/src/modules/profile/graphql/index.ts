import { Ref } from "vue";

import { useQuery } from "~/graphql/useQuery";
import { useMutation } from "~/graphql/useMutation";
import { useSubscription } from "~/graphql/useSubscription";

import BLOCK_USER from "./mutations/blockUser.gql";
import UNBLOCK_USER from "./mutations/unblockUser.gql";
import DELETE_FRIENDSHIP from "./mutations/deleteFriendship.gql";
import ACCEPT_FRIENDSHIP_INVITATION from "./mutations/acceptFriendshipInvitation.gql";
import DECLINE_FRIENDSHIP_INVITATION from "./mutations/declineFriendshipInvitation.gql";
import INVITE_USER_TO_FRIENDS_LIST from "./mutations/inviteUserToFriendsList.gql";

import GET_FRIENDS_VIEW from "./query/getFriendsView.gql";
import GET_PROFILE_VIEW from "./query/getProfileView.gql";
import GET_MATCHES_VIEW from "./query/getMatchesView.gql";
import GET_SETTINGS_VIEW from "./query/getSettingsView.gql";
import GET_MATCHES from "./query/getMatches.gql";

import FRIENDSHIP_INVITATION_CREATED from "./subscriptions/friendshipInvitationCreated.gql";

import {
  AcceptFriendshipInvitationMutation,
  AcceptFriendshipInvitationMutationVariables,
  BlockUserMutation,
  BlockUserMutationVariables,
  DeclineFriendshipInvitationMutation,
  DeclineFriendshipInvitationMutationVariables,
  DeleteFriendshipMutation,
  DeleteFriendshipMutationVariables,
  FriendshipInvitationCreatedSubscription,
  FriendshipInvitationCreatedSubscriptionVariables,
  GetFriendsViewQuery,
  GetFriendsViewQueryVariables,
  GetMatchesQuery,
  GetMatchesQueryVariables,
  GetMatchesViewQuery,
  GetMatchesViewQueryVariables,
  GetProfileViewQuery,
  GetProfileViewQueryVariables,
  GetSettingsViewQuery,
  GetSettingsViewQueryVariables,
  InviteUserToFriendsListMutation,
  InviteUserToFriendsListMutationVariables,
  UnblockUserMutation,
  UnblockUserMutationVariables
} from "~/graphql/types/types";

export const useInviteUserToFriendsList = () => useMutation<
  InviteUserToFriendsListMutation,
  InviteUserToFriendsListMutationVariables>(INVITE_USER_TO_FRIENDS_LIST);

export const useAcceptFriendshipInvitation = () => useMutation<
  AcceptFriendshipInvitationMutation,
  AcceptFriendshipInvitationMutationVariables>(ACCEPT_FRIENDSHIP_INVITATION);

export const useDeclineFriendshipInvitation = () => useMutation<
  DeclineFriendshipInvitationMutation,
  DeclineFriendshipInvitationMutationVariables>(DECLINE_FRIENDSHIP_INVITATION);

export const useBlockUser = () => useMutation<
  BlockUserMutation,
  BlockUserMutationVariables>(BLOCK_USER);

export const useUnblockUser = () => useMutation<
  UnblockUserMutation,
  UnblockUserMutationVariables>(UNBLOCK_USER);

export const useDeleteFriendship = () => useMutation<
  DeleteFriendshipMutation,
  DeleteFriendshipMutationVariables>(DELETE_FRIENDSHIP);

export const useFriendsView = (userId: string | Ref<string>) => useQuery<
  GetFriendsViewQuery,
  GetFriendsViewQueryVariables>({
    query: GET_FRIENDS_VIEW,
    variables: {
      userId
    }
  });

export const useProfileView = (userId: string | Ref<string>) => useQuery<
  GetProfileViewQuery,
  GetProfileViewQueryVariables>({
    query: GET_PROFILE_VIEW,
    variables: {
      userId
    }
  });

export const useGetSettingsView = (userId: string | Ref<string>) => useQuery<
  GetSettingsViewQuery,
  GetSettingsViewQueryVariables>({
    query: GET_SETTINGS_VIEW,
    variables: {
      userId
    }
  });

export const useMatchesView = (
  userId: string | Ref<string>,
  mode: number | Ref<number>
) => useQuery<
  GetMatchesViewQuery,
  GetMatchesViewQueryVariables>({
    query: GET_MATCHES_VIEW,
    variables: {
      // @ts-ignore
      mode,
      userId
    }
  });

export const useMatches = (
  userId: string | Ref<string>,
  mode: number | Ref<number>
) => useQuery<
  GetMatchesQuery,
  GetMatchesQueryVariables>({
    query: GET_MATCHES,
    variables: {
      // @ts-ignore
      mode,
      userId
    }
  }, true);

export const useFriendshipInvitationCreated = () => useSubscription<
    FriendshipInvitationCreatedSubscription,
    FriendshipInvitationCreatedSubscriptionVariables>({
      query: FRIENDSHIP_INVITATION_CREATED
    });

