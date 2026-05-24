export interface PlantListDto {
  id: number;
  code?: string;
  name: string;
  slug: string;
  treeStyleName?: string;
  potShapeName?: string;
  potSizeCm?: number;
  actualPrice?: number;
  displayPrice?: string;
  priceRangeLabel?: string;
  status: string;
  thumbnailUrl?: string;
  isFeatured: boolean;
}

export interface PlantDetailDto {
  id: number;
  code?: string;
  name: string;
  slug: string;
  treeStyleId?: number;
  treeStyleName?: string;
  potShapeName?: string;
  potSizeCm?: number;
  canopyWidth?: number;
  canopyHeight?: number;
  trunkCircumference?: number;
  fruitCount?: number;
  actualPrice?: number;
  importPrice?: number;
  displayPrice?: string;
  priceRangeLabel?: string;
  status: string;
  description?: string;
  shortDescription?: string;
  careInstruction?: string;
  thumbnailUrl?: string;
  isFeatured: boolean;
  isSold: boolean;
  images: PlantImageDto[];
}

export interface PlantImageDto {
  id: number;
  imageUrl: string;
  isPrimary: boolean;
}

export interface PlantFilterDto {
  keyword?: string;
  priceRangeIds: number[];
  treeStyleIds: number[];
  potShapeIds: number[];
  potSizeIds: number[];
  minPrice?: number;
  maxPrice?: number;
  sortBy?: string;
  page: number;
  pageSize: number;
}
