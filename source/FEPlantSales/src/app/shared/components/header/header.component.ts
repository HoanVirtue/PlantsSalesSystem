import { Component, Output, EventEmitter, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  @Output() searchSubmit = new EventEmitter<string>();

  menuOpen = false;
  searchOpen = false;
  searchKeyword = '';
  isSticky = false;

  @HostListener('window:scroll', [])
  onScroll(): void {
    this.isSticky = window.scrollY > 80;
  }

  toggleMenu(): void { this.menuOpen = !this.menuOpen; }
  toggleSearch(): void { this.searchOpen = !this.searchOpen; }

  onSearch(): void {
    if (this.searchKeyword.trim()) {
      this.searchSubmit.emit(this.searchKeyword.trim());
      this.searchKeyword = '';
      this.searchOpen = false;
    }
  }
}
