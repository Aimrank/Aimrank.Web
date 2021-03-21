import { defineComponent } from "vue";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useChangeLobbyConfiguration } from "~/graphql/types/types";
import { MatchMode } from "@/profile/models/MatchMode";
import MapButton from "@/lobby/components/MapButton";

const maps = {
  aim_map: require("~/assets/images/aim_map.jpg").default,
  am_redline_14: require("~/assets/images/am_redline_14.jpg").default
};

const LobbyConfiguration = defineComponent({
  components: {
    MapButton
  },
  props: {
    lobbyId: {
      type: String,
      required: true
    },
    map: {
      type: String,
      required: true
    },
    name: {
      type: String,
      required: true
    },
    mode: {
      type: Number,
      required: true
    }
  },
  setup(props) {
    const notifications = useNotifications();

    const { mutate: changeLobbyConfiguration } = useChangeLobbyConfiguration();

    const onMapSelected = async (map: string) => {
      const { success, errors } = await changeLobbyConfiguration({
        input: {
          lobbyId: props.lobbyId,
          map,
          name: props.name,
          mode: props.mode
        }
      });

      if (!success) {
        notifications.danger(errors[0].message);
      }
    }

    const onModeSelected = async (mode: MatchMode) => {
      const { success, errors } = await changeLobbyConfiguration({
        input: {
          lobbyId: props.lobbyId,
          map: props.map,
          mode,
          name: props.name
        }
      });

      if (!success) {
        notifications.danger(errors[0].message)
      }
    }

    return {
      maps,
      MatchMode: Object.freeze(MatchMode),
      onMapSelected,
      onModeSelected
    };
  }
});

export default LobbyConfiguration;
