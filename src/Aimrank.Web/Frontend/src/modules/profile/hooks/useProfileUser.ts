import { computed } from "vue";
import { useRoute } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";

export const useProfileUser = () => {
  const auth = useAuth();
  const route = useRoute();

  const profileUserId = computed(() => route.params.userId as string || auth.state.user!.id);
  const currentUserId = computed(() => auth.state.user!.id);
  const isCurrentUserProfile = computed(() => profileUserId.value == currentUserId.value);

  return {
    profileUserId,
    currentUserId,
    isCurrentUserProfile
  };
}