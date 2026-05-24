export enum TreeShapeEnum {
  BanTra = 0,
  Truc = 1,
  TanRong = 2
}

export const TreeShapeLabels: Record<TreeShapeEnum, string> = {
  [TreeShapeEnum.BanTra]: 'Ban Tra',
  [TreeShapeEnum.Truc]: 'Truc',
  [TreeShapeEnum.TanRong]: 'Tan Rong'
};
