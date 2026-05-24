import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { debounceTime, switchMap, tap, catchError, shareReplay } from 'rxjs/operators';
import { of } from 'rxjs';
import { PlantService } from '../../../core/services/api/plant.service';
import { PlantFilterDto, PlantListDto } from '../../../core/models/plant.model';
import { PagedResultDto } from '../../../core/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class ShopFacade {
  private filters$ = new BehaviorSubject<PlantFilterDto>({
    priceRangeIds: [],
    treeStyleIds: [],
    potShapeIds: [],
    potSizeIds: [],
    page: 1,
    pageSize: 12,
    sortBy: undefined,
    minPrice: undefined,
    maxPrice: undefined
  });

  private loading$ = new BehaviorSubject<boolean>(false);

  plants$: Observable<PagedResultDto<PlantListDto>>;

  constructor(private plantService: PlantService) {
    this.plants$ = this.filters$.pipe(
      debounceTime(300),
      switchMap(filters => {
        this.loading$.next(true);
        return this.plantService.getPlants(filters).pipe(
          tap(() => this.loading$.next(false)),
          switchMap(response => {
            if (response.success && response.data) {
              return of(response.data);
            }
            return of({ data: [], total: 0, page: 1, pageSize: 12, totalPages: 0 });
          }),
          catchError(() => {
            this.loading$.next(false);
            return of({ data: [], total: 0, page: 1, pageSize: 12, totalPages: 0 });
          })
        );
      }),
      shareReplay(1)
    );
  }

  getLoading$(): Observable<boolean> {
    return this.loading$.asObservable();
  }

  setFilters(partial: Partial<PlantFilterDto>): void {
    const current = this.filters$.value;
    this.filters$.next({ ...current, ...partial, page: 1 });
  }

  setPage(page: number): void {
    const current = this.filters$.value;
    this.filters$.next({ ...current, page });
  }

  getCurrentFilters(): PlantFilterDto {
    return this.filters$.value;
  }
}
