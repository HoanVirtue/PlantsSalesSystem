export enum PlantStatusEnum {
  Available = 0,
  Reserved = 1,
  Sold = 2
}

export const PlantStatusLabels: Record<PlantStatusEnum, string> = {
  [PlantStatusEnum.Available]: 'Available',
  [PlantStatusEnum.Reserved]: 'Reserved',
  [PlantStatusEnum.Sold]: 'Sold'
};
