import { defineComponent, ref } from "vue";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useResponseErrors } from "@/common/hooks/useResponseErrors";
import { useAddSteamToken, useDeleteSteamToken, useGetSteamTokens } from "~/graphql/types/types";
import { ErrorResponse } from "@/common/hooks/ErrorResponse";
import BaseButton from "@/common/components/BaseButton";
import FormFieldInput from "@/common/components/FormFieldInput";

const SteamTokens = defineComponent({
  components: {
    BaseButton,
    FormFieldInput
  },
  setup() {
    const formErrors = useResponseErrors();
    const notifications = useNotifications();

    const token = ref("");
    const loadingAdd = ref(false);
    const loadingDelete = ref<Record<string, boolean>>({});

    const setLoading = (token: string, loading: boolean) => {
      loadingDelete.value = {
        ...loadingDelete.value,
        [token]: loading
      };
    }

    const { result: state } = useGetSteamTokens();
    const { mutate: addSteamToken } = useAddSteamToken();
    const { mutate: deleteSteamToken } = useDeleteSteamToken();

    const onAddSteamToken = async () => {
      loadingAdd.value = true;

      const { errors } = await addSteamToken({input: {token: token.value}});

      loadingAdd.value = false;

      if (errors[0]) {
        notifications.danger(errors[0].message);
        formErrors.setErrors(ErrorResponse.fromGraphQLError(errors[0]));
      } else if (state.value?.steamTokens) {
        state.value.steamTokens = [
          ...state.value.steamTokens,
          {
            __typename: "SteamToken",
            isUsed: false,
            token: token.value
          }
        ];

        token.value = "";
        formErrors.clearErrors();
      }
    }

    const onDeleteSteamToken = async (token: string) => {
      setLoading(token, true);

      const { errors } = await deleteSteamToken({input: {token}});

      setLoading(token, false);

      if (errors[0]) {
        notifications.danger(errors[0].message);
      } else if (state.value?.steamTokens) {
        state.value.steamTokens = state.value.steamTokens.filter(t => t.token !== token);
      }
    }

    return {
      state,
      token,
      loadingAdd,
      loadingDelete,
      errors: formErrors.state,
      onAddSteamToken,
      onDeleteSteamToken
    };
  }
});

export default SteamTokens;
