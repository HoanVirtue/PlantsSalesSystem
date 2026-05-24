export interface ApiResponseDto<T> {
  success: boolean;
  data?: T;
  message?: string;
  errors?: string[];
}

export interface PagedResultDto<T> {
  data: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}
