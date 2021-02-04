import { computed, defineComponent, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useUser } from "@/modules/user";
import { lobbyService, matchService } from "@/services";
import { ILobbyDto } from "../../services/LobbyService";
import { IMatchDto } from "../../services/MatchService";
import BaseButton from "@/modules/common/components/BaseButton";
import FormFieldInput from "@/modules/common/components/FormFieldInput";

const useLobby = () => {
  const user = useUser();

  const lobby = ref<ILobbyDto | null>();
  const match = ref<IMatchDto | null>();
  const map = ref("");

  onMounted(async () => {
    if (!user.state.user) {
      return;
    }

    const result = await lobbyService.getByUserId(user.state.user.id);

    if (result.isOk()) {
      lobby.value = result.value;
      map.value = result.value.map;

      if (lobby.value.matchId) {
        const matchResult = await matchService.getById(lobby.value.matchId);

        if (matchResult.isOk()) {
          match.value = matchResult.value;
        }
      }
    }
  });

  return {
    lobby,
    match,
    map
  }
}

const Lobby = defineComponent({
  components: {
    BaseButton,
    FormFieldInput
  },
  setup() {
    const { lobby, match, map } = useLobby();

    const user = useUser();
    const router = useRouter();

    const onCloseLobbyClick = async () => {
      if (!lobby.value) {
        return;
      }

      const result = await lobbyService.close(lobby.value.id);

      if (result.isOk()) {
        alert("Serching for new game");
      } else {
        alert(result.error.title);
      }
    }

    const onChangeMapClick = async () => {
      if (!lobby.value) {
        return;
      }

      const result = await lobbyService.changeMap(lobby.value.id, { name: map.value });

      if (result.isOk()) {
        lobby.value = { ...lobby.value, map: map.value };
      } else {
        alert(result.error.title);
      }
    }

    const onLeaveLobbyClick = async () => {
      if (!lobby.value) {
        return;
      }

      const result = await lobbyService.leave(lobby.value.id);

      if (result.isOk()) {
        router.push({ name: "lobbies" });
      } else {
        alert(result.error.title);
      }
    }

    const member = computed(() => lobby.value?.members.find(m => m.userId === user.state.user?.id));

    return {
      map,
      lobby,
      match,
      member,
      onCloseLobbyClick,
      onChangeMapClick,
      onLeaveLobbyClick
    };
  }
});

export default Lobby;
