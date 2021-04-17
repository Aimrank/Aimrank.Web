import { defineComponent } from "vue";

const BaseButton = defineComponent({
  props: {
    tag: {
      type: String,
      default: "button"
    },
    primary: Boolean,
    loading: Boolean,
    disabled: Boolean,
    small: Boolean
  }
});

export default BaseButton;
