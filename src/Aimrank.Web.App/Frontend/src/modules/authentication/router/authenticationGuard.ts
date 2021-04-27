import { RouteLocationNormalized, NavigationGuardNext } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";

export const authenticate = (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
  const { isAuthenticated } = useAuth();

  if (isAuthenticated.value) {
    return next();
  }

  return next({
    name: "sign-in",
    query: { returnUrl: encodeURIComponent(to.fullPath) }
  });
}

export const authenticateRoles = (...roles: string[]) =>
  (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
    const { isAuthenticated, currentUser } = useAuth();

    if (isAuthenticated.value && currentUser.value?.roles.some(role => roles.includes(role))) {
      return next();
    }

    return next({ name: isAuthenticated ? "app" : "home" });
  }