import { Component, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { map, debounceTime, takeUntil } from 'rxjs/operators';
import { CategoriesService } from '../../../../core/services/api/categories.service';
import { PlantFilterDto } from '../../../../core/models/plant.model';
import { TreeStyleDto, PotShapeDto, PotSizeDto } from '../../../../core/models/categories.model';

@Component({
  selector: 'app-filter-sidebar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './filter-sidebar.component.html'
})
export class FilterSidebarComponent implements OnInit, OnDestroy {
  @Output() filtersChanged = new EventEmitter<Partial<PlantFilterDto>>();

  treeStyles$!: Observable<TreeStyleDto[]>;
  potShapes$!: Observable<PotShapeDto[]>;
  potSizes$!: Observable<PotSizeDto[]>;

  minPrice: number | null = null;
  maxPrice: number | null = null;

  selectedFilters: Partial<PlantFilterDto> = {
    treeStyleIds: [],
    potShapeIds: [],
    potSizeIds: []
  };

  private priceChange$ = new Subject<void>();
  private destroy$ = new Subject<void>();

  constructor(private categoriesService: CategoriesService) {}

  ngOnInit(): void {
    this.treeStyles$ = this.categoriesService.getCategories().pipe(
      map(response => response.data?.treeStyles || [])
    );

    this.potShapes$ = this.categoriesService.getCategories().pipe(
      map(response => response.data?.potShapes || [])
    );

    this.potSizes$ = this.categoriesService.getCategories().pipe(
      map(response => response.data?.potSizes || [])
    );

    this.priceChange$.pipe(
      debounceTime(500),
      takeUntil(this.destroy$)
    ).subscribe(() => this.emitFilters());
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  isSelected(filterType: string, value: number): boolean {
    const key = filterType as keyof PlantFilterDto;
    const filterArray = this.selectedFilters[key] as number[];
    return filterArray?.includes(value) ?? false;
  }

  toggle(filterType: string, value: number): void {
    const key = filterType as keyof PlantFilterDto;
    const filterArray = (this.selectedFilters[key] as number[]) || [];

    if (this.isSelected(filterType, value)) {
      this.selectedFilters[key] = filterArray.filter(v => v !== value) as any;
    } else {
      this.selectedFilters[key] = [...filterArray, value] as any;
    }

    this.emitFilters();
  }

  onPriceInput(): void {
    this.priceChange$.next();
  }

  clearFilters(): void {
    this.selectedFilters = {
      treeStyleIds: [],
      potShapeIds: [],
      potSizeIds: []
    };
    this.minPrice = null;
    this.maxPrice = null;
    this.emitFilters();
  }

  private emitFilters(): void {
    this.filtersChanged.emit({
      ...this.selectedFilters,
      minPrice: this.minPrice ?? undefined,
      maxPrice: this.maxPrice ?? undefined
    });
  }
}
