import { reactive } from "vue";
import { userService } from "~/services";
import { IUserDto } from "@/profile/models/IUserDto";

interface IState {
  isDialogVisible: boolean;
  searchQuery: string;
  searchResults: IUserDto[];
}

const state = reactive<IState>({
  isDialogVisible: false,
  searchQuery: "",
  searchResults: []
});

const open = () => {
  state.isDialogVisible = true;
}

const onFetchResults = async () => {
  if (state.searchQuery.trim().length === 0) {
    state.searchResults = [];
    return;
  }

  const result = await userService.browse(state.searchQuery);

  if (result.isOk()) {
    state.searchResults = result.value;
  }
}

const onClose = () => {
  state.isDialogVisible = false;
  state.searchQuery = "";
  state.searchResults = [];
}

export const useUsersDialog = () => ({
  state,
  open,
  onFetchResults,
  onClose
});