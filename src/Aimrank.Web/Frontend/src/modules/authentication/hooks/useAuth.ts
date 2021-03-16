import { reactive, readonly } from "vue";
import { router } from "~/router";
import { useMutation } from "~/graphql/useMutation";
import {
  AuthenticateCommandInput,
  SignInMutationVariables,
  SignInMutation,
  RegisterNewUserCommandInput,
  SignUpMutationVariables,
  SignUpMutation
} from "~/graphql/types/types";

import SIGN_IN from "./signIn.gql";
import SIGN_UP from "./signUp.gql";
import SIGN_OUT from "./signOut.gql";

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

const signIn = async (command: AuthenticateCommandInput) => {
  const { mutate, result, errors } = useMutation<SignInMutation, SignInMutationVariables>(SIGN_IN);

  await mutate({ command });

  if (result.value?.signIn?.record) {
    setCurrentUser({
      id: result.value.signIn.record.id,
      email: result.value.signIn.record.email,
      username: result.value.signIn.record.username
    });
  }

  return {
    result: result.value?.signIn?.record,
    errors: errors.value
  };
}

const signUp = async (command: RegisterNewUserCommandInput) => {
  const { mutate, result, errors } = useMutation<SignUpMutation, SignUpMutationVariables>(SIGN_UP);

  await mutate({ command });

  if (result.value?.signUp?.record) {
    setCurrentUser({
      id: result.value.signUp.record.id,
      email: result.value.signUp.record.email,
      username: result.value.signUp.record.username
    });
  }

  return {
    result: result.value?.signUp?.record,
    errors: errors.value
  };
}

const signOut = async () => {
  const { mutate } = useMutation(SIGN_OUT);

  await mutate();

  setCurrentUser(null);

  await router.push({ name: "home" });
}

export const useAuth = () => ({
  state: readonly(state),
  signIn,
  signUp,
  signOut,
  setCurrentUser
});
