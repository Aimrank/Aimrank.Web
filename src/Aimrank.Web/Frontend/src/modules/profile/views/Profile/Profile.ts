import { defineComponent, onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { userService } from "~/services";
import { IUserDto } from "@/profile/models/IUserDto";

const Profile = defineComponent({
  setup() {
    const route = useRoute();

    const user = ref<IUserDto | null>();

    onMounted(async () => {
      const result = await userService.getUserDetails(route.params.userId as string);

      if (result.isOk()) {
        user.value = result.value;
      }
    });

    return { user };
  }
});

export default Profile;
