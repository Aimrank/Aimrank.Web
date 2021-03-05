import Chart from "chart.js";
import { computed, defineComponent, ref, watch } from "vue";
import { IMatchEntry } from "@/profile/models/MatchEntry";

const colors = {
  blue: "#007bff",
  blueTransparent: "#007bff50"
};

const RatingChart = defineComponent({
  props: {
    matches: {
      type: Array as () => IMatchEntry[],
      required: true
    }
  },
  setup(props) {
    const root = ref(null);

    watch (
      () => props.matches,
      () => {
        if (root) {
          renderChart();
        }
      }
    );

    const matchesSorted = computed(() => [...props.matches].reverse());

    const rating = computed(() => matchesSorted.value.map((m => m.matchPlayerResult?.rating ?? 0)));

    const renderChart = () => {
      new Chart(root.value, {
        type: "line",
        data: {
          labels: matchesSorted.value.map(m => new Date(m.finishedAt).toDateString().slice(0, 3)),
          datasets: [{
            fill: true,
            data: rating.value,
            borderColor: colors.blue,
            backgroundColor: colors.blueTransparent
          }]
        },
        options: {
          responsive: true,
          legend: {
            display: false
          },
          elements: {
            line: {
              tension: 0.4
            }
          },
          scales: {
            yAxes: [
              {
                ticks: {
                  suggestedMin: Math.max(0, Math.min(...rating.value) - 20),
                  suggestedMax: Math.max(...rating.value) + 20,
                }
              }
            ]
          }
        }
      });
    }

    return { root };
  }
});

export default RatingChart;
