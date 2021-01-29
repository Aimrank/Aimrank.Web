import { defineComponent } from "vue";

const FormFieldErrors = defineComponent({
  props: {
    errors: {
      type: Array as () => string[],
      default: () => []
    },
    showMultiple: Boolean
  }
});

export default FormFieldErrors;
