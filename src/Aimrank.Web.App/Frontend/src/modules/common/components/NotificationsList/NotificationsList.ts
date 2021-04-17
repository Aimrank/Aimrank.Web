import { defineComponent } from "vue";
import { useNotifications } from "@/common/hooks/useNotifications";
import Notification from "@/common/components/Notification";

const NotificationsList = defineComponent({
  components: {
    Notification
  },
  setup() {
    return useNotifications();
  }
});

export default NotificationsList;
