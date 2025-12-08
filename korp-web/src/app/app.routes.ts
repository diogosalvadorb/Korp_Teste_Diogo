import { Routes } from '@angular/router';
import { HomeComponent } from './feature/home/home.component';
import { BillingComponent } from './feature/billing/billing.component';
import { ProductComponent } from './feature/product/product.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },

  { path: 'home', component: HomeComponent },
  { path: 'billing', component: BillingComponent },
  { path: 'product', component: ProductComponent },
];
