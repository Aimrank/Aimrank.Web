import { ref } from "vue";
import { GetProfileViewQuery } from "~/graphql/types/types";

const state = ref<GetProfileViewQuery | undefined>();

const setFriendship = (friendship?: GetProfileViewQuery["friendship"]) => {
  if (state.value) {
    state.value = {
      ...state.value,
      friendship
    };
  }
}

const setState = (value: GetProfileViewQuery | undefined) => {
  state.value = value;
}

export const useProfileStore = () => ({
  state,
  setState,
  setFriendship
});
