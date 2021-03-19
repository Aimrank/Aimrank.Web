import { RouteLocationNormalized, NavigationGuardNext } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";

export const authenticate = (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
  const { isAuthenticated } = useAuth();

  if (isAuthenticated) {
    return next();
  }

  return next({
    name: "sign-in",
    query: { returnUrl: encodeURIComponent(to.fullPath) }
  });
}
