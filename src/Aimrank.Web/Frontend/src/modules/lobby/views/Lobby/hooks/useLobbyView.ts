import { computed, onMounted } from "vue";
import { lobbyHub, lobbyService } from "~/services";
import { useLobby } from "@/lobby/hooks/useLobby";
import { useMatch } from "@/lobby/hooks/useMatch";
import { useUser } from "@/profile/hooks/useUser";

export const useLobbyView = () => {
  const user = useUser();
  const lobby = useLobby();
  const match = useMatch();

  const currentUserMembership = computed(() => lobby.state.lobby?.members.find(m => m.userId === user.state.user?.id));

  onMounted(async () => {
    if (!user.state.user) {
      return;
    }

    const result = await lobbyService.getForCurrentUser();

    if (result.isOk() && result.value) {
      lobby.setLobby(result.value);

      await lobbyHub.connect();

      const matchResult = await lobbyService.getMatch(result.value.id);

      if (matchResult.isOk() && matchResult.value) {
        match.setMatch(matchResult.value);
      }
    }
  });

  return {
    lobby,
    match,
    currentUserMembership,
  };
}
