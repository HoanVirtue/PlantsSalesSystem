import { Component, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-plant-search-bar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './plant-search-bar.component.html'
})
export class PlantSearchBarComponent {
  @Output() searched = new EventEmitter<string>();

  searchKeyword = '';

  onSearch(): void {
    if (this.searchKeyword.trim()) {
      this.searched.emit(this.searchKeyword);
    }
  }
}
