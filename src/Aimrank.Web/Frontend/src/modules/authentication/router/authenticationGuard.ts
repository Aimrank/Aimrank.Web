import { RouteLocationNormalized, NavigationGuardNext } from "vue-router";
import { useAuth } from "../hooks/useAuth";

export const authenticate = (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
  const { state } = useAuth();

  if (state.isAuthenticated) {
    return next();
  }

  return next({
    name: "sign-in",
    query: { returnUrl: encodeURIComponent(to.fullPath) }
  });
}
