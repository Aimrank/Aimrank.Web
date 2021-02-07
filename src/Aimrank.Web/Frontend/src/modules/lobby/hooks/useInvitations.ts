import { reactive, readonly } from "vue";
import { ILobbyInvitationDto } from "../services/LobbyService";

interface IInvitationsState {
  invitations: ILobbyInvitationDto[];
}

const state = reactive<IInvitationsState>({
  invitations: []
});

export const useInvitations = () => {
  const setInvitations = (invitations: ILobbyInvitationDto[]) => {
    state.invitations = invitations;
  }

  const removeInvitation = (lobbyId: string) => {
    state.invitations = state.invitations.filter(i => i.lobbyId !== lobbyId);
  }

  return {
    state: readonly(state),
    setInvitations,
    removeInvitation
  };
}
