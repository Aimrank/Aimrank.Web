import { computed, defineComponent } from "vue";
import { v4 as uuid } from "uuid";
import FormFieldErrors from "../FormFieldErrors";

const FormFieldInput = defineComponent({
  inheritAttrs: false,
  emits: ["update:modelValue"],
  components: {
    FormFieldErrors
  },
  props: {
    id: String,
    label: String,
    modelValue: String,
    errors: {
      type: Array as () => string[],
      default: () => []
    },
    errorsShowMultiple: Boolean
  },
  setup(props, { emit }) {
    const inputId = computed(() => props.id ?? uuid());

    const onInput = (event: any) => {
      emit("update:modelValue", event.target.value);
    }

    return {
      inputId,
      onInput
    };
  }
});

export default FormFieldInput;
