import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';
import { ApiResponseDto } from '../../models/api-response.model';
import { PriceRangeDto } from '../../models/price-range.model';

@Injectable({
  providedIn: 'root'
})
export class PriceRangeService extends BaseApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getPriceRanges(): Observable<ApiResponseDto<PriceRangeDto[]>> {
    return this.http.get<ApiResponseDto<PriceRangeDto[]>>(
      `${this.apiUrl}/categories/price-ranges`
    );
  }
}
