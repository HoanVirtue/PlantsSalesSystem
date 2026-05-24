---
name: angular-bancaycanh-fe
description: Setup Angular app for BanCayCanh with clean architecture - separation of concerns, testability, maintainability
disable-model-invocation: false
allowed-tools: Bash(ng *) Bash(npm *)
---

# Setup Angular Frontend for BanCayCanh - Clean Architecture

## Clean Architecture Principles
- **Separation of Concerns**: Core, Shared, Features layers
- **Dependency Rule**: Inner layers don't depend on outer layers
- **Testability**: Business logic separated from UI framework
- **Reusability**: Shared components/services accessible to all features
- **Scalability**: New features added without modifying existing code

## Folder Structure - Customer App & Admin App

```
customer-app/ or admin-app/
├── src/
│   ├── app/
│   │   ├── core/                        # Layer 1: Business logic, models
│   │   │   ├── models/
│   │   │   │   ├── plant.model.ts
│   │   │   │   ├── order.model.ts
│   │   │   │   ├── customer.model.ts
│   │   │   │   ├── auth.model.ts
│   │   │   │   ├── api-response.model.ts
│   │   │   │   └── enums/
│   │   │   │       ├── tree-shape.enum.ts
│   │   │   │       ├── pot-style.enum.ts
│   │   │   │       └── plant-status.enum.ts
│   │   │   │
│   │   │   ├── services/
│   │   │   │   ├── api/
│   │   │   │   │   ├── plant.service.ts        # HTTP calls only
│   │   │   │   │   ├── order.service.ts
│   │   │   │   │   ├── customer.service.ts
│   │   │   │   │   ├── auth.service.ts
│   │   │   │   │   ├── chatbot.service.ts
│   │   │   │   │   └── base-api.service.ts     # Common HTTP logic
│   │   │   │   │
│   │   │   │   ├── state/
│   │   │   │   │   ├── plant.facade.ts         # Business logic orchestrator
│   │   │   │   │   ├── order.facade.ts
│   │   │   │   │   └── auth.facade.ts
│   │   │   │   │
│   │   │   │   └── utils/
│   │   │   │       ├── storage.service.ts      # LocalStorage abstraction
│   │   │   │       ├── logger.service.ts       # Logging service
│   │   │   │       └── slug-generator.service.ts
│   │   │   │
│   │   │   ├── interceptors/
│   │   │   │   ├── auth.interceptor.ts         # JWT token injection
│   │   │   │   ├── error.interceptor.ts        # Global error handling
│   │   │   │   └── loading.interceptor.ts      # Loading indicator
│   │   │   │
│   │   │   ├── guards/
│   │   │   │   ├── auth.guard.ts               # Protect routes
│   │   │   │   └── unsaved-changes.guard.ts
│   │   │   │
│   │   │   └── core.module.ts
│   │   │
│   │   ├── shared/                             # Layer 2: Reusable UI components
│   │   │   ├── components/
│   │   │   │   ├── header/
│   │   │   │   │   ├── header.component.ts
│   │   │   │   │   ├── header.component.html
│   │   │   │   │   └── header.component.scss
│   │   │   │   │
│   │   │   │   ├── footer/
│   │   │   │   ├── navigation/
│   │   │   │   ├── breadcrumb/
│   │   │   │   ├── loading-spinner/
│   │   │   │   ├── confirmation-dialog/
│   │   │   │   ├── error-message/
│   │   │   │   ├── plant-card/               # Reusable plant grid item
│   │   │   │   ├── plant-image-carousel/     # Reusable carousel
│   │   │   │   └── data-table/               # Reusable table (admin)
│   │   │   │
│   │   │   ├── directives/
│   │   │   │   ├── highlight.directive.ts
│   │   │   │   ├── debounce-click.directive.ts
│   │   │   │   └── focus-on-init.directive.ts
│   │   │   │
│   │   │   ├── pipes/
│   │   │   │   ├── currency-vnd.pipe.ts       # Format Vietnamese currency
│   │   │   │   ├── tree-shape.pipe.ts         # Display Vietnamese names
│   │   │   │   ├── pot-style.pipe.ts
│   │   │   │   ├── plant-status.pipe.ts
│   │   │   │   ├── truncate.pipe.ts
│   │   │   │   └── format-phone.pipe.ts       # Vietnamese phone format
│   │   │   │
│   │   │   ├── validators/
│   │   │   │   ├── vietnamese-phone.validator.ts
│   │   │   │   ├── async-email.validator.ts
│   │   │   │   └── match-password.validator.ts
│   │   │   │
│   │   │   └── shared.module.ts               # Declare all above
│   │   │
│   │   ├── features/                          # Layer 3: Feature modules (lazy-loaded)
│   │   │   ├── home/
│   │   │   │   ├── pages/
│   │   │   │   │   └── home.component.ts      # Container component
│   │   │   │   ├── components/
│   │   │   │   │   ├── hero-section/
│   │   │   │   │   ├── featured-plants/
│   │   │   │   │   ├── categories-section/
│   │   │   │   │   └── testimonials/
│   │   │   │   ├── services/
│   │   │   │   │   └── home.facade.ts         # Feature-specific business logic
│   │   │   │   └── home.module.ts
│   │   │   │
│   │   │   ├── shop/
│   │   │   │   ├── pages/
│   │   │   │   │   └── shop-list.component.ts
│   │   │   │   ├── components/
│   │   │   │   │   ├── filter-sidebar/
│   │   │   │   │   │   ├── filter-sidebar.component.ts
│   │   │   │   │   │   ├── tree-shape-filter/
│   │   │   │   │   │   ├── price-range-filter/
│   │   │   │   │   │   ├── pot-style-filter/
│   │   │   │   │   │   └── pot-size-filter/
│   │   │   │   │   ├── plant-grid/
│   │   │   │   │   └── pagination/
│   │   │   │   ├── services/
│   │   │   │   │   └── shop.facade.ts
│   │   │   │   └── shop.module.ts
│   │   │   │
│   │   │   ├── plant-detail/
│   │   │   │   ├── pages/
│   │   │   │   │   └── plant-detail.component.ts
│   │   │   │   ├── components/
│   │   │   │   │   ├── plant-specs/
│   │   │   │   │   ├── related-plants/
│   │   │   │   │   └── order-form-modal/
│   │   │   │   ├── services/
│   │   │   │   │   └── plant-detail.facade.ts
│   │   │   │   └── plant-detail.module.ts
│   │   │   │
│   │   │   ├── chatbot/
│   │   │   │   ├── components/
│   │   │   │   │   └── chatbot-widget/
│   │   │   │   │       ├── chatbot-widget.component.ts
│   │   │   │   │       ├── chat-message/
│   │   │   │   │       └── chat-input/
│   │   │   │   ├── services/
│   │   │   │   │   └── chatbot.facade.ts
│   │   │   │   └── chatbot.module.ts
│   │   │   │
│   │   │   └── [admin-only features]
│   │   │       ├── admin-dashboard/
│   │   │       ├── plant-management/
│   │   │       ├── order-management/
│   │   │       ├── customer-management/
│   │   │       └── category-management/
│   │   │
│   │   ├── layout/
│   │   │   ├── main-layout/
│   │   │   │   ├── main-layout.component.ts    # Outlet for features
│   │   │   │   └── main-layout.component.html
│   │   │   │
│   │   │   └── admin-layout/ (admin-app only)
│   │   │       ├── admin-layout.component.ts
│   │   │       └── sidebar/
│   │   │
│   │   ├── app.component.ts
│   │   ├── app-routing.module.ts                # Root routes
│   │   └── app.module.ts
│   │
│   ├── assets/                                 # Static files
│   │   ├── alazea-template/  (customer-app)   # Alazea CSS, fonts, images
│   │   ├── focus2-template/  (admin-app)      # Focus-2 CSS, fonts, images
│   │   └── icons/
│   │
│   ├── styles/
│   │   ├── styles.scss                         # Global styles
│   │   ├── _variables.scss
│   │   ├── _mixins.scss
│   │   ├── _reset.scss
│   │   └── _typography.scss
│   │
│   ├── environments/
│   │   ├── environment.ts                      # Dev
│   │   ├── environment.prod.ts
│   │   └── environment.staging.ts
│   │
│   ├── main.ts
│   └── index.html
│
├── angular.json
├── tsconfig.json                               # Path aliases: @core, @shared, @features
├── package.json
└── README.md
```

## Layer Architecture Explanation

### **Layer 1: Core** (Business Logic)
- **Models**: Data structures matching backend DTOs
- **Services (API)**: HTTP calls, very thin - just API communication
- **Services (State/Facade)**: Orchestrates API calls + local state management
- **Interceptors**: Global HTTP handling (auth, errors, logging)
- **Guards**: Route protection
- **Utils**: Helper services (storage, logging, etc.)

**Rule**: Don't import from Shared or Features layers

```typescript
// core/models/plant.model.ts
export interface Plant {
  id: number;
  name: string;
  treeShape: TreeShapeEnum;
  price: number;
  potSize: number;
  status: PlantStatusEnum;
  createdAt: Date;
}

// core/services/api/plant.service.ts
@Injectable({ providedIn: 'root' })
export class PlantApiService {
  constructor(private http: HttpClient) {}

  getPlants(filters?: PlantFilters): Observable<ApiResponse<Plant[]>> {
    return this.http.get<ApiResponse<Plant[]>>('/api/v1/plants', { params: filters });
  }

  getPlantById(id: number): Observable<ApiResponse<Plant>> {
    return this.http.get<ApiResponse<Plant>>(`/api/v1/plants/${id}`);
  }
}

// core/services/state/plant.facade.ts
@Injectable({ providedIn: 'root' })
export class PlantFacade {
  private plantApiService = inject(PlantApiService);
  private storageService = inject(StorageService);
  private loggerService = inject(LoggerService);

  private plants$ = new BehaviorSubject<Plant[]>([]);
  private loading$ = new BehaviorSubject(false);
  private error$ = new BehaviorSubject<string | null>(null);

  getPlants() {
    this.loading$.next(true);
    return this.plantApiService.getPlants().pipe(
      tap(response => {
        this.plants$.next(response.data);
        this.loading$.next(false);
      }),
      catchError(err => {
        this.loggerService.error('Failed to load plants', err);
        this.error$.next('Không thể tải danh sách cây');
        this.loading$.next(false);
        return of([]);
      })
    );
  }

  plants = this.plants$.asObservable();
  loading = this.loading$.asObservable();
  error = this.error$.asObservable();
}
```

### **Layer 2: Shared** (Reusable UI Components)
- Components used in multiple features (Header, Footer, PlantCard, DataTable)
- Directives, Pipes, Validators
- Shared module declares everything
- **Rule**: Only depend on Core layer

```typescript
// shared/components/plant-card/plant-card.component.ts
@Component({
  selector: 'app-plant-card',
  template: `
    <div class="plant-card">
      <img [src]="plant.image" [alt]="plant.name">
      <h3>{{ plant.name }}</h3>
      <p class="price">{{ plant.price | currencyVnd }}</p>
      <p class="tree-shape">Dáng: {{ plant.treeShape | treeShapePipe }}</p>
      <button (click)="onBuyClick()">Mua hàng</button>
    </div>
  `,
  styles: [`...`],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlantCardComponent {
  @Input() plant!: Plant;
  @Output() buyClicked = new EventEmitter<Plant>();

  onBuyClick() {
    this.buyClicked.emit(this.plant);
  }
}

// shared/pipes/currency-vnd.pipe.ts
@Pipe({ name: 'currencyVnd', standalone: true })
export class CurrencyVndPipe implements PipeTransform {
  transform(value: number): string {
    return new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND'
    }).format(value);
  }
}

// shared/validators/vietnamese-phone.validator.ts
export const vietnamesePhoneValidator = (): ValidatorFn => {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) return null;
    const pattern = /^(03|05|07|08|09)\d{8}$/;
    return pattern.test(control.value) ? null : { invalidPhone: true };
  };
};
```

### **Layer 3: Features** (Feature Modules - Lazy Loaded)
- Pages (container components)
- Feature-specific components
- Feature-specific facades
- Each feature is a standalone module
- **Rule**: Can depend on Core + Shared, but not other features

```typescript
// features/shop/shop.module.ts
@NgModule({
  declarations: [ShopListComponent, FilterSidebarComponent, PlantGridComponent],
  imports: [CommonModule, SharedModule, ReactiveFormsModule]
})
export class ShopModule {}

// features/shop/components/filter-sidebar/filter-sidebar.component.ts
@Component({
  selector: 'app-filter-sidebar',
  template: `
    <aside class="filter-sidebar">
      <form [formGroup]="filterForm">
        <app-tree-shape-filter formControlName="treeShape"></app-tree-shape-filter>
        <app-price-range-filter formControlName="priceRange"></app-price-range-filter>
        <app-pot-style-filter formControlName="potStyle"></app-pot-style-filter>
        <button (click)="onApplyFilters()">Áp dụng</button>
      </form>
    </aside>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FilterSidebarComponent {
  @Output() filtersChanged = new EventEmitter<PlantFilters>();

  filterForm = new FormGroup({
    treeShape: new FormControl<TreeShapeEnum | null>(null),
    priceRange: new FormControl<number | null>(null),
    potStyle: new FormControl<PotStyleEnum | null>(null),
    potSize: new FormControl<number | null>(null)
  });

  onApplyFilters() {
    this.filtersChanged.emit(this.filterForm.value);
  }
}

// features/shop/pages/shop-list.component.ts (Container Component)
@Component({
  selector: 'app-shop-list',
  template: `
    <div class="shop-container">
      <app-filter-sidebar (filtersChanged)="onFiltersChanged($event)"></app-filter-sidebar>
      <div class="shop-main">
        <app-plant-grid [plants]="plants$ | async" (buyClicked)="onBuyClicked($event)"></app-plant-grid>
      </div>
    </div>
  `
})
export class ShopListComponent {
  plants$ = this.shopFacade.getPlants();

  constructor(private shopFacade: ShopFacade) {}

  onFiltersChanged(filters: PlantFilters) {
    this.shopFacade.setFilters(filters);
  }

  onBuyClicked(plant: Plant) {
    // Navigate to detail or open modal
  }
}
```

## Routing Architecture

```typescript
// app-routing.module.ts
const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'shop', loadChildren: () => import('./features/shop/shop.module').then(m => m.ShopModule) },
      { path: 'plant/:slug', loadChildren: () => import('./features/plant-detail/plant-detail.module').then(m => m.PlantDetailModule) },
      { path: 'contact', component: ContactComponent }
    ]
  },
  {
    path: 'auth',
    children: [
      { path: 'login', component: LoginComponent }
    ]
  },
  // Admin routes (with auth guard)
  {
    path: 'admin',
    component: AdminLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'plants', loadChildren: () => import('./features/admin/plant-management/plant-management.module').then(m => m.PlantManagementModule) },
      { path: 'orders', loadChildren: () => import('./features/admin/order-management/order-management.module').then(m => m.OrderManagementModule) },
      { path: 'customers', loadChildren: () => import('./features/admin/customer-management/customer-management.module').then(m => m.CustomerManagementModule) }
    ]
  }
];
```

## Dependency Injection Pattern

```typescript
// tsconfig.json - Path aliases
{
  "compilerOptions": {
    "baseUrl": ".",
    "paths": {
      "@core/*": ["src/app/core/*"],
      "@shared/*": ["src/app/shared/*"],
      "@features/*": ["src/app/features/*"],
      "@environments/*": ["src/environments/*"]
    }
  }
}

// Usage in components
import { PlantFacade } from '@core/services/state/plant.facade';
import { PlantCardComponent } from '@shared/components/plant-card/plant-card.component';
```

## Component Design Patterns

### **Container vs Presentational**
```typescript
// CONTAINER (Smart Component)
// - Handles business logic
// - Manages state
// - Subscribes to observables
// - Passes data down, receives events up

@Component({
  selector: 'app-shop-list',
  template: `
    <app-plant-grid [plants]="(plants$ | async)" (buyClicked)="onBuyClicked($event)"></app-plant-grid>
  `
})
export class ShopListComponent {
  plants$ = this.facade.getPlants();
  constructor(private facade: ShopFacade) {}
}

// PRESENTATIONAL (Dumb Component)
// - Pure display logic
// - All data via @Input
// - Sends user actions via @Output
// - No service injection

@Component({
  selector: 'app-plant-grid',
  template: `
    <div class="grid">
      <app-plant-card *ngFor="let plant of plants" [plant]="plant" (buyClicked)="buyClicked.emit($event)"></app-plant-card>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlantGridComponent {
  @Input() plants!: Plant[];
  @Output() buyClicked = new EventEmitter<Plant>();
}
```

## Implementation Checklist

- [ ] Create folder structure per layers
- [ ] Define all models/enums in Core
- [ ] Create API services (HTTP only)
- [ ] Create Facade services (business logic)
- [ ] Create shared reusable components
- [ ] Create directives, pipes, validators in Shared
- [ ] Create interceptors (auth, error, loading)
- [ ] Create guards (auth protection)
- [ ] Create feature modules with lazy loading
- [ ] Setup routing with layouts
- [ ] Configure path aliases in tsconfig
- [ ] Test: Container components + Presentational components

## Best Practices

✅ **Do**:
- Keep components small and focused
- Use OnPush change detection strategy
- Use async pipe with observables
- Unsubscribe properly (takeUntil pattern)
- Create interfaces for all data
- Use trackBy in *ngFor

❌ **Don't**:
- Don't subscribe in templates (use async pipe)
- Don't have business logic in components
- Don't create circular dependencies between features
- Don't import from Features in Core/Shared
- Don't use two-way binding for complex state

## Next Steps

1. Run: `/angular-architect /bancaycanh-scaffold` to generate initial structure
2. Implement Core layer (models, services, facades)
3. Implement Shared layer (components, pipes, directives)
4. Implement Feature modules one by one with lazy loading
5. Add interceptors, guards, and routing
6. Test each layer independently
