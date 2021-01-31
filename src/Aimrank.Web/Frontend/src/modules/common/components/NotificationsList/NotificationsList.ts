import { defineComponent } from "vue";
import { useNotifications } from "@/modules/common/hooks/useNotifications";
import Notification from "@/modules/common/components/Notification";

const NotificationsList = defineComponent({
  components: {
    Notification
  },
  setup() {
    return useNotifications();
  }
});

export default NotificationsList;
