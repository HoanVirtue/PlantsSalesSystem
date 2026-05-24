export enum PotStyleEnum {
  Tron = 0,
  Vuong = 1,
  ChuNhat = 2
}

export const PotStyleLabels: Record<PotStyleEnum, string> = {
  [PotStyleEnum.Tron]: 'Tron',
  [PotStyleEnum.Vuong]: 'Vuong',
  [PotStyleEnum.ChuNhat]: 'Chu Nhat'
};
