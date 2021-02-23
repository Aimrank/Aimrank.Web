import { defineComponent } from "vue";
import { lobbyService } from "~/services";
import { useLobby } from "@/lobby/hooks/useLobby";
import { useNotifications } from "@/common/hooks/useNotifications";
import { MatchMode } from "@/match/models/MatchMode";
import MapButton from "@/lobby/components/MapButton";

const maps = {
  aim_map: require("~/assets/images/aim_map.jpg").default,
  am_redline_14: require("~/assets/images/am_redline_14.jpg").default
};

const LobbyConfiguration = defineComponent({
  components: {
    MapButton
  },
  setup() {
    const notifications = useNotifications();

    const { state, isAvailable, setLobbyConfiguration } = useLobby();

    const onMapSelected = async (map: string) => {
      if (!isAvailable || map === state.lobby?.configuration.map) {
        return;
      }

      const result = await lobbyService.changeConfiguration(state.lobby!.id, {
        map,
        name: state.lobby!.configuration.name,
        mode: state.lobby!.configuration.mode
      });

      if (result.isOk()) {
        setLobbyConfiguration({ ...state.lobby!.configuration, map });
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onModeSelected = async (mode: MatchMode) => {
      if (!isAvailable || mode === state.lobby?.configuration.mode) {
        return;
      }

      const result = await lobbyService.changeConfiguration(state.lobby!.id, {
        mode,
        map: state.lobby!.configuration.map,
        name: state.lobby!.configuration.name,
      });

      if (result.isOk()) {
        setLobbyConfiguration({ ...state.lobby!.configuration, mode });
      } else {
        notifications.danger(result.error.title);
      }
    }

    return {
      maps,
      state,
      MatchMode: Object.freeze(MatchMode),
      onMapSelected,
      onModeSelected
    };
  }
});

export default LobbyConfiguration;
