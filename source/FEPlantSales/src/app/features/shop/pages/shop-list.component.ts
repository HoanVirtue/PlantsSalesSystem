import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { ShopFacade } from '../services/shop.facade';
import { PlantListDto, PlantFilterDto } from '../../../core/models/plant.model';
import { PagedResultDto } from '../../../core/models/api-response.model';
import { BreadcrumbComponent } from '../../../shared/components/breadcrumb/breadcrumb.component';
import { LoadingSpinnerComponent } from '../../../shared/components/loading-spinner/loading-spinner.component';
import { FilterSidebarComponent } from '../components/filter-sidebar/filter-sidebar.component';
import { PlantGridComponent } from '../components/plant-grid/plant-grid.component';
import { ShopPaginationComponent } from '../components/shop-pagination/shop-pagination.component';

@Component({
  selector: 'app-shop-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    BreadcrumbComponent,
    LoadingSpinnerComponent,
    FilterSidebarComponent,
    PlantGridComponent,
    ShopPaginationComponent
  ],
  styleUrl: './shop-list.component.scss',
  templateUrl: './shop-list.component.html'
})
export class ShopListComponent implements OnInit {
  breadcrumbs = [{ label: 'Cửa hàng', url: '/shop' }];

  result$!: Observable<PagedResultDto<PlantListDto>>;
  loading$!: Observable<boolean>;
  currentPage = 1;
  pageSize = 12;
  sortBy = '';
  keyword = '';

  readonly pageSizeOptions = [9, 12, 18, 24];
  readonly sortOptions = [
    { value: '', label: 'Mặc định' },
    { value: 'newest', label: 'Mới nhất' },
    { value: 'price_asc', label: 'Giá: Thấp → Cao' },
    { value: 'price_desc', label: 'Giá: Cao → Thấp' },
    { value: 'name_asc', label: 'Tên: A → Z' },
    { value: 'name_desc', label: 'Tên: Z → A' }
  ];

  private searchTimeout: any;

  constructor(private shopFacade: ShopFacade) { }

  ngOnInit(): void {
    this.result$ = this.shopFacade.plants$;
    this.loading$ = this.shopFacade.getLoading$();
  }

  onKeywordChange(): void {
    if (this.searchTimeout) clearTimeout(this.searchTimeout);
    this.searchTimeout = setTimeout(() => {
      this.shopFacade.setFilters({ keyword: this.keyword.trim() || undefined });
    }, 300);
  }

  onSortChange(): void {
    this.currentPage = 1;
    this.shopFacade.setFilters({ sortBy: this.sortBy || undefined });
  }

  onPageSizeChange(): void {
    this.currentPage = 1;
    this.shopFacade.setFilters({ pageSize: this.pageSize });
  }

  onFiltersChanged(filters: Partial<PlantFilterDto>): void {
    this.currentPage = 1;
    this.shopFacade.setFilters(filters);
  }

  onPageChanged(page: number): void {
    this.currentPage = page;
    this.shopFacade.setPage(page);
  }
}
