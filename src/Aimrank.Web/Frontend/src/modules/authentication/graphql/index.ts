import { useMutation } from "~/graphql/useMutation";

import {
  SignInMutation,
  SignInMutationVariables,
  SignOutMutation,
  SignOutMutationVariables,
  SignUpMutation,
  SignUpMutationVariables
} from "~/graphql/types/types";

import SIGN_IN from "./mutations/signIn.gql";
import SIGN_UP from "./mutations/signUp.gql";
import SIGN_OUT from "./mutations/signOut.gql";

export const useSignIn = () => useMutation<SignInMutation, SignInMutationVariables>(SIGN_IN);
export const useSignUp = () => useMutation<SignUpMutation, SignUpMutationVariables>(SIGN_UP);
export const useSignOut = () => useMutation<SignOutMutation, SignOutMutationVariables>(SIGN_OUT);
