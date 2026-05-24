import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PlantListDto } from '../../../core/models/plant.model';
import { CurrencyVndPipe } from '../../pipes/currency-vnd.pipe';

@Component({
  selector: 'app-plant-card',
  standalone: true,
  imports: [CommonModule, RouterModule, CurrencyVndPipe],
  templateUrl: './plant-card.component.html',
  styleUrls: ['./plant-card.component.scss']
})
export class PlantCardComponent {
  @Input() plant!: PlantListDto;
}
