import { ComputedRef } from "vue";

import { useQuery } from "~/graphql/useQuery";
import { useMutation } from "~/graphql/useMutation";

import BLOCK_USER from "./mutations/blockUser.gql";
import UNBLOCK_USER from "./mutations/unblockUser.gql";
import DELETE_FRIENDSHIP from "./mutations/deleteFriendship.gql";
import ACCEPT_FRIENDSHIP_INVITATION from "./mutations/acceptFriendshipInvitation.gql";
import DECLINE_FRIENDSHIP_INVITATION from "./mutations/declineFriendshipInvitation.gql";
import INVITE_USER_TO_FRIENDS_LIST from "./mutations/inviteUserToFriendsList.gql";

import GET_FRIENDS_VIEW from "./query/getFriendsView.gql";
import GET_PROFILE_VIEW from "./query/getProfileView.gql";

import {
  AcceptFriendshipInvitationMutation,
  AcceptFriendshipInvitationMutationVariables,
  BlockUserMutation,
  BlockUserMutationVariables,
  DeclineFriendshipInvitationMutation,
  DeclineFriendshipInvitationMutationVariables,
  DeleteFriendshipMutation,
  DeleteFriendshipMutationVariables,
  GetFriendsViewQuery,
  GetFriendsViewQueryVariables,
  GetProfileViewQuery,
  GetProfileViewQueryVariables,
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

export const useFriendsView = (userId: string | ComputedRef<string>) => useQuery<
  GetFriendsViewQuery,
  GetFriendsViewQueryVariables>({
    query: GET_FRIENDS_VIEW,
    variables: {
      userId
    }
  });

export const useProfileView = (userId: string | ComputedRef<string>) => useQuery<
  GetProfileViewQuery,
  GetProfileViewQueryVariables>({
    query: GET_PROFILE_VIEW,
    variables: {
      userId
    }
  });