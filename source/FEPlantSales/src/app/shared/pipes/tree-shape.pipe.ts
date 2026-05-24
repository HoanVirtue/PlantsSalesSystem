import { Pipe, PipeTransform } from '@angular/core';
import { TreeShapeLabels } from '../../core/models/enums/tree-shape.enum';

@Pipe({
  name: 'treeShape',
  standalone: true
})
export class TreeShapePipe implements PipeTransform {
  transform(value: number | undefined): string {
    if (value === null || value === undefined) return '';
    return TreeShapeLabels[value as keyof typeof TreeShapeLabels] || '';
  }
}
