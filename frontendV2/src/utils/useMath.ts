function normaliseToRange(
  input: number,
  rangeStart: number,
  rangeEnd: number,
): number {
  if (input > rangeEnd) input = rangeEnd;

  if (input <= rangeStart) return 0;

  return (input - rangeStart) / (rangeEnd - rangeStart);
}

export { normaliseToRange };
