export const parseDate = (date: string): string => {
  return new Date(date).toLocaleString();
};
