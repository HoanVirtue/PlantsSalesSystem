import { Pipe, PipeTransform } from '@angular/core';
import { PotStyleLabels } from '../../core/models/enums/pot-style.enum';

@Pipe({
  name: 'potStyle',
  standalone: true
})
export class PotStylePipe implements PipeTransform {
  transform(value: number | undefined): string {
    if (value === null || value === undefined) return '';
    return PotStyleLabels[value as keyof typeof PotStyleLabels] || '';
  }
}
