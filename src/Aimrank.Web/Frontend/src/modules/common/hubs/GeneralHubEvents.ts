export interface ILobbyInvitationCreatedEvent {
  lobbyId: string;
  invitingUserId: string;
  invitedUserId: string;
}

export interface IFriendshipInvitationCreatedEvent {
  invitingUserId: string;
  invitedUserId: string;
}