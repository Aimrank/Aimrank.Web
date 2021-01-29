export const paramsSerializer = (params: any) => {
  const values: string[] = [];

  for (const param in params) {
    const value = params[param];

    if (Array.isArray(value)) {
      for (const item of value) {
        values.push(`${param}=${encodeURIComponent(item)}`);
      }
    } else {
      values.push(`${param}=${encodeURIComponent(value)}`);
    }
  }

  return values.length === 0 ? '' : values.join('&');
}
