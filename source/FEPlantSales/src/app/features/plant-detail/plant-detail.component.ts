import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PlantService } from '../../core/services/api/plant.service';
import { PlantDetailDto, PlantListDto } from '../../core/models/plant.model';
import { BreadcrumbComponent } from '../../shared/components/breadcrumb/breadcrumb.component';
import { CurrencyVndPipe } from '../../shared/pipes/currency-vnd.pipe';
import { PlantCardComponent } from '../../shared/components/plant-card/plant-card.component';

@Component({
  selector: 'app-plant-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, BreadcrumbComponent, CurrencyVndPipe, PlantCardComponent],
  styleUrls: ['./plant-detail.component.scss'],
  templateUrl: './plant-detail.component.html'
})
export class PlantDetailComponent implements OnInit {
  breadcrumbs: any[] = [];
  plant: PlantDetailDto | null = null;
  relatedPlants: PlantListDto[] = [];
  activeImageIndex = 0;
  activeTab = 'description';
  quantity = 1;
  lightboxOpen = false;

  constructor(
    private route: ActivatedRoute,
    private plantService: PlantService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const slug = params['slug'];
      this.activeImageIndex = 0;
      this.activeTab = 'description';
      this.quantity = 1;

      this.plantService.getPlantBySlug(slug).subscribe(response => {
        if (response.success && response.data) {
          this.plant = response.data;
          this.breadcrumbs = [
            { label: 'Cửa hàng', url: '/shop' },
            {
              label: response.data.code
                ? `${response.data.code} - ${response.data.name}`
                : response.data.name
            }
          ];
          this.loadRelatedPlants(response.data.treeStyleId);
        }
      });
    });
  }

  get activeImage(): string {
    if (!this.plant) return 'img/bg-img/49.jpg';
    if (this.plant.images.length > 0) {
      return this.plant.images[this.activeImageIndex]?.imageUrl ?? this.plant.thumbnailUrl ?? 'img/bg-img/49.jpg';
    }
    return this.plant.thumbnailUrl ?? 'img/bg-img/49.jpg';
  }

  get allImages(): string[] {
    if (!this.plant) return [];
    if (this.plant.images.length > 0) {
      return this.plant.images.map(img => img.imageUrl);
    }
    return this.plant.thumbnailUrl ? [this.plant.thumbnailUrl] : [];
  }

  setActiveImage(index: number): void {
    this.activeImageIndex = index;
  }

  openLightbox(index: number): void {
    this.activeImageIndex = index;
    this.lightboxOpen = true;
    document.body.style.overflow = 'hidden';
  }

  closeLightbox(): void {
    this.lightboxOpen = false;
    document.body.style.overflow = '';
  }

  prevImage(): void {
    this.activeImageIndex = this.activeImageIndex > 0
      ? this.activeImageIndex - 1
      : this.allImages.length - 1;
  }

  nextImage(): void {
    this.activeImageIndex = this.activeImageIndex < this.allImages.length - 1
      ? this.activeImageIndex + 1
      : 0;
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }

  increaseQty(): void {
    this.quantity++;
  }

  decreaseQty(): void {
    if (this.quantity > 1) this.quantity--;
  }

  private loadRelatedPlants(treeStyleId?: number): void {
    const filter: any = {
      priceRangeIds: [],
      treeStyleIds: treeStyleId ? [treeStyleId] : [],
      potShapeIds: [],
      potSizeIds: [],
      page: 1,
      pageSize: 4
    };

    this.plantService.getPlants(filter).subscribe(response => {
      if (response.success && response.data) {
        this.relatedPlants = response.data.data.filter(p => p.slug !== this.plant?.slug);
        if (this.relatedPlants.length < 4 && treeStyleId) {
          this.loadFallbackRelated();
        }
      }
    });
  }

  private loadFallbackRelated(): void {
    this.plantService.getPlants({
      priceRangeIds: [], treeStyleIds: [], potShapeIds: [], potSizeIds: [],
      page: 1, pageSize: 5
    }).subscribe(response => {
      if (response.success && response.data) {
        const others = response.data.data.filter(p => p.slug !== this.plant?.slug);
        this.relatedPlants = others.slice(0, 4);
      }
    });
  }
}
