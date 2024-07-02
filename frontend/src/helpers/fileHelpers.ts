export const bytesToMB = (bytes: number, decimals: number = 2) => {
  if (bytes === 0) return "0 MB";
  const k = 1024;
  return (bytes / (k * k)).toFixed(decimals) + " MB";
};
