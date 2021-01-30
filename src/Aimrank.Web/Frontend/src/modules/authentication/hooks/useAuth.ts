import { reactive, readonly } from "vue";
import { authService, httpClient } from "@/services";
import { router } from "@/router";
import { useUser } from "@/modules/user";
import { ISignInRequest, ISignUpRequest } from "../services/AuthService";

interface IAuthState {
  isAuthenticated: boolean;
}

const state = reactive<IAuthState>({
  isAuthenticated: false
});

const setAuthenticated = (isAuthenticated: boolean) => {
  state.isAuthenticated = isAuthenticated;
}

const signIn = async (request: ISignInRequest) => {
  const result = await authService.signIn(request);

  if (result.isOk()) {
    httpClient.setAuthorizationToken(
      result.value.jwt,
      result.value.refreshToken
    );
    
    await updateUser();
    setAuthenticated(true);
  }

  return result;
}

const signUp = async (request: ISignUpRequest) => {
  const result = await authService.signUp(request);

  if (result.isOk()) {
    httpClient.setAuthorizationToken(
      result.value.jwt,
      result.value.refreshToken
    );

    await updateUser();
    setAuthenticated(true);
  }

  return result;
}

const signOut = async () => {
  const { setUser } = useUser();

  setUser(null);
  setAuthenticated(false);

  httpClient.setAuthorizationToken(undefined, undefined);

  await router.push({ name: "home" });
}

const updateUser = async () => {
  const user = useUser();
  await user.updateUser(httpClient.getUserId()!);
}

export const useAuth = () => ({
  state: readonly(state),
  signIn,
  signUp,
  signOut,
  setAuthenticated
});
