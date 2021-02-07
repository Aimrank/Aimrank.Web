import { defineComponent } from "vue";

const MapButton = defineComponent({
  emits: ["click"],
  props: {
    selected: Boolean,
    image: {
      type: String,
      required: true
    }
  },
  setup(props, { emit }) {
    const onButtonClick = () => {
      emit("click");
    }

    return { onButtonClick };
  }
});

export default MapButton;
