import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';
import { ApiResponseDto, PagedResultDto } from '../../models/api-response.model';
import { PlantDetailDto, PlantFilterDto, PlantListDto } from '../../models/plant.model';

@Injectable({
  providedIn: 'root'
})
export class PlantService extends BaseApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getPlants(filter: PlantFilterDto): Observable<ApiResponseDto<PagedResultDto<PlantListDto>>> {
    let params = new HttpParams()
      .set('page', filter.page.toString())
      .set('pageSize', filter.pageSize.toString());

    if (filter.keyword) {
      params = params.set('keyword', filter.keyword);
    }

    if (filter.priceRangeIds?.length > 0) {
      params = params.set('priceRangeIds', filter.priceRangeIds.join(','));
    }

    if (filter.treeStyleIds?.length > 0) {
      params = params.set('treeStyleIds', filter.treeStyleIds.join(','));
    }

    if (filter.potShapeIds?.length > 0) {
      params = params.set('potShapeIds', filter.potShapeIds.join(','));
    }

    if (filter.potSizeIds?.length > 0) {
      params = params.set('potSizeIds', filter.potSizeIds.join(','));
    }

    if (filter.minPrice != null) {
      params = params.set('minPrice', filter.minPrice.toString());
    }

    if (filter.maxPrice != null) {
      params = params.set('maxPrice', filter.maxPrice.toString());
    }

    if (filter.sortBy) {
      params = params.set('sortBy', filter.sortBy);
    }

    return this.http.get<ApiResponseDto<PagedResultDto<PlantListDto>>>(
      `${this.apiUrl}/plants`,
      { params }
    );
  }

  getPlantBySlug(slug: string): Observable<ApiResponseDto<PlantDetailDto>> {
    return this.http.get<ApiResponseDto<PlantDetailDto>>(
      `${this.apiUrl}/plants/slug/${slug}`
    );
  }
}
