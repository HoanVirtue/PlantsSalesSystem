import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlantListDto } from '../../../../core/models/plant.model';
import { PlantCardComponent } from '../../../../shared/components/plant-card/plant-card.component';

@Component({
  selector: 'app-plant-grid',
  standalone: true,
  imports: [CommonModule, PlantCardComponent],
  templateUrl: './plant-grid.component.html'
})
export class PlantGridComponent {
  @Input() plants: PlantListDto[] = [];
}
