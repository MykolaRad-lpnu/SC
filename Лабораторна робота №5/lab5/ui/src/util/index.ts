export const toUpperFirstSumbol = (arr: string[]): string[] =>
  arr
    .map((item) => item[0].toUpperCase() + item.slice(1))
    .map((item) => item.replace(/_/g, " "));

export const toLowerFirstSumbol = (arr: string[]): string[] =>
  arr
    .map((item) => item[0].toLowerCase() + item.slice(1))
    .map((item) => item.replace(/ /g, "_"));

export function get(obj: any, path: string) {
  const keys = path.split(".");
  let result = obj;
  for (const key of keys) {
    if (typeof result !== "object" || !result) {
      break;
    }
    result = result[key];
  }
  return result;
}

export function set(obj: any, path: string, value: any) {
  const keys = path.split(".");
  let result = obj;
  for (const key of keys.slice(0, -1)) {
    if (typeof result !== "object" || !result) {
      break;
    }
    if (!result[key]) {
      result[key] = {};
    }
    result = result[key];
  }
  result[keys[keys.length - 1]] = value;
}
