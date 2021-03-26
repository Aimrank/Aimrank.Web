import { defineComponent, reactive, watch } from "vue";
import { useChangeLobbyConfiguration } from "~/graphql/types/types";
import { MatchMode } from "@/profile/models/MatchMode";
import BaseButton from "@/common/components/BaseButton";
import BaseDialog from "@/common/components/BaseDialog";

interface IState {
  isVisible: boolean;
  isLoading: boolean;
  maps: string[];
  mode: number;
}

interface ILobbyConfiguration {
  lobbyId: string;
  name: string;
  mode: number;
  maps: string[];
}

const LobbyConfigurationDialog = defineComponent({
  components: {
    BaseButton,
    BaseDialog
  },
  props: {
    configuration: {
      type: Object as () => ILobbyConfiguration,
      required: true
    }
  },
  setup(props) {
    const state = reactive<IState>({
      isVisible: false,
      isLoading: false,
      maps: props.configuration.maps,
      mode: props.configuration.mode
    });

    watch(
      () => props.configuration,
      () => {
        state.isVisible = false;
        state.isLoading = false;
        props.configuration.maps
        props.configuration.mode
      }
    );

    const { mutate: changeLobbyConfiguration } = useChangeLobbyConfiguration();

    const open = () => {
      state.isVisible = true;
    }

    const onSubmit = async () => {
      state.isLoading = true;

      const { success } = await changeLobbyConfiguration({
        input: {
          lobbyId: props.configuration.lobbyId,
          mode: state.mode,
          maps: state.maps,
          name: props.configuration.name
        }
      });

      if (!success) {
        state.isLoading = false;
      }
    }

    const onCancel = () => {
      state.isVisible = false;
    }

    return {
      state,
      open,
      onSubmit,
      onCancel,
      MatchMode: Object.freeze(MatchMode)
    };
  }
});

export default LobbyConfigurationDialog;
