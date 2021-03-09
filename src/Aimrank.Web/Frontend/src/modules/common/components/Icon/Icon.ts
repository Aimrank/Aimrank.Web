import { computed, defineComponent } from "vue";

const icons = {
  times: "fas fa-times",
  check: "fas fa-check",
  steam: "fab fa-steam"
};

const Icon = defineComponent({
  props: {
    name: {
      type: String,
      required: true
    }
  },
  setup(props) {
    return {
      iconClass: computed(() => icons[props.name] ?? "")
    };
  }
});

export default Icon;
