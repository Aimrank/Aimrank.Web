import { computed, reactive } from "vue";
import { router } from "~/router";
import { reconnect } from "~/graphql/apolloClient";
import {
  AuthenticateCommandInput,
  RegisterNewUserCommandInput,
  useSignIn,
  useSignOut,
  useSignUp,
} from "~/graphql/types/types";

interface IAuthUser {
  id: string;
  email: string;
  username: string;
}

interface IAuthState {
  isAuthenticated: boolean;
  user: IAuthUser | null;
}

const state = reactive<IAuthState>({
  isAuthenticated: false,
  user: null
});

const setCurrentUser = (user: IAuthUser | null) => {
  state.isAuthenticated = user !== null;
  state.user = user;
}

const signIn = async (input: AuthenticateCommandInput) => {
  const { mutate, result, errors } = useSignIn();

  await mutate({ input });

  if (result.value?.signIn?.record) {
    setCurrentUser({
      id: result.value.signIn.record.id,
      email: result.value.signIn.record.email,
      username: result.value.signIn.record.username
    });

    reconnect();
  }

  return {
    result: result.value?.signIn?.record,
    errors: errors.value
  };
}

const signUp = async (input: RegisterNewUserCommandInput) => {
  const { mutate, result, errors } = useSignUp();

  await mutate({ input });

  if (result.value?.signUp?.record) {
    setCurrentUser({
      id: result.value.signUp.record.id,
      email: result.value.signUp.record.email,
      username: result.value.signUp.record.username
    });

    reconnect();
  }

  return {
    result: result.value?.signUp?.record,
    errors: errors.value
  };
}

const signOut = async () => {
  const { mutate } = useSignOut();

  await mutate();

  await router.push({ name: "home" });

  setCurrentUser(null);

  reconnect();
}

export const useAuth = () => ({
  currentUser: computed(() => state.user),
  isAuthenticated: computed(() => state.isAuthenticated),
  signIn,
  signUp,
  signOut,
  setCurrentUser
});
