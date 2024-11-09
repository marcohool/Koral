export enum CurrencyCode {
  GBP = 0,
  USD = 2,
  EUR = 3,
}

const CurrencyCodeNames = {
  [CurrencyCode.GBP]: '£',
  [CurrencyCode.USD]: '$',
  [CurrencyCode.EUR]: '€',
};

export function getCurrencyCodeName(currencyCode: CurrencyCode) {
  return CurrencyCodeNames[currencyCode];
}
