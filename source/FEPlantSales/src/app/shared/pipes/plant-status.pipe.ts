import { Pipe, PipeTransform } from '@angular/core';
import { PlantStatusLabels } from '../../core/models/enums/plant-status.enum';

@Pipe({
  name: 'plantStatus',
  standalone: true
})
export class PlantStatusPipe implements PipeTransform {
  transform(value: number | undefined): string {
    if (value === null || value === undefined) return '';
    return PlantStatusLabels[value as keyof typeof PlantStatusLabels] || '';
  }
}
