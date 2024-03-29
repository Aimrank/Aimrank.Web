export const debounce = (func: (...args: any[]) => any, wait: number, immediate: boolean = false) => {
  let timeout;

	return (...args: any[]) => {
    const context = this;

		const later = () => {
			timeout = null;
			if (!immediate) func.apply(context, args);
    };

    const callNow = immediate && !timeout;
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
		if (callNow) func.apply(context, args);
	};
};
