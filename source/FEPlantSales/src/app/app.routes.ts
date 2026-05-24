import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout.component';
import { HomeComponent } from './features/home/home.component';
import { ShopListComponent } from './features/shop/pages/shop-list.component';
import { PlantDetailComponent } from './features/plant-detail/plant-detail.component';
import { ContactComponent } from './features/contact/contact.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        component: HomeComponent
      },
      {
        path: 'shop',
        component: ShopListComponent
      },
      {
        path: 'cay-canh/:slug',
        component: PlantDetailComponent
      },
      {
        path: 'lien-he',
        component: ContactComponent
      }
    ]
  }
];
