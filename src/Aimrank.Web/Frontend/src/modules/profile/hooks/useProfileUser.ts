import { computed } from "vue";
import { useRoute } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";

export const useProfileUser = () => {
  const { currentUser } = useAuth();
  const route = useRoute();

  const profileUserId = computed(() => route.params.userId as string || currentUser.value!.id);
  const currentUserId = computed(() => currentUser.value!.id);
  const isCurrentUserProfile = computed(() => profileUserId.value == currentUserId.value);

  return {
    profileUserId,
    currentUserId,
    isCurrentUserProfile
  };
}