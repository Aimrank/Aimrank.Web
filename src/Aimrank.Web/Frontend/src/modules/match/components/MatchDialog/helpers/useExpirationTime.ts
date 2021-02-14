import { onBeforeUnmount, ref } from "vue";

export const useExpirationTime = () => {
  const time = ref(0);

  let interval: any;

  const getTimeDifference = (t1: string, t2: string) => {
    const a = new Date(t1).getTime();
    const b = new Date(t2).getTime();

    return Math.round((a - b) / 1000);
  }

  onBeforeUnmount(() => clearInterval(interval));

  const start = (expiresAt: string) => {
    if (interval) {
      clearInterval(interval);
    }

    time.value = getTimeDifference(expiresAt, new Date().toUTCString());

    interval = setInterval(
      () => {
        time.value = getTimeDifference(expiresAt, new Date().toUTCString());

        if (time.value <= 0) {
          time.value = 0;

          clearInterval(interval);
        }
      },
      1000
    );
  }

  return { time, start };
}