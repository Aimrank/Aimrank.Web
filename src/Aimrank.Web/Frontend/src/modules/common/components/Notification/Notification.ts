import { defineComponent } from "vue";
import { INotification, NotificationColor } from "@/common/hooks/useNotifications";
import Icon from "@/common/components/Icon";

const Notification = defineComponent({
  emits: ["close"],
  components: {
    Icon
  },
  props: {
    data: {
      type: Object as () => INotification,
      required: true,
    }
  },
  setup(props, { emit }) {
    const onClose = () => {
      emit("close");
    }

    return {
      NotificationColor: Object.freeze(NotificationColor),
      onClose
    };
  }
});

export default Notification;
