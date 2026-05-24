export interface TreeStyleDto {
  id: number;
  name: string;
  slug?: string;
  description?: string;
}

export interface PotShapeDto {
  id: number;
  name: string;
  description?: string;
}

export interface PotSizeDto {
  id: number;
  sizeCm: number;
  description?: string;
}

export interface CategoriesDto {
  treeStyles: TreeStyleDto[];
  potShapes: PotShapeDto[];
  potSizes: PotSizeDto[];
}
