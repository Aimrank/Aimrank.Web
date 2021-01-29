type MapKeysResult<T> = {
  [key: string]: T
}

export const mapKeys = <T>(array: T[], keySelector: (item: T) => string): MapKeysResult<T> =>  {
  const result: MapKeysResult<T> = {};

  for (const element of array) {
    result[keySelector(element)] = element;
  };

  return result;
}
