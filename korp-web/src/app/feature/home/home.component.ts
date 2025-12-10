import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ProductService } from '../product/services/product.service';
import { BillingService } from '../billing/services/billing.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  totalProducts = 0;
  totalInvoices = 0;
  totalStock = 0;
  openInvoices = 0;
  loading = true;

  constructor(
    private productService: ProductService,
    private billingService: BillingService
  ) {}

  ngOnInit() {
    this.loadDashboardData();
  }

  loadDashboardData() {
    this.loading = true;

    forkJoin({
      products: this.productService.getAll(),
      invoices: this.billingService.getAll()
    }).subscribe({
      next: (data) => {
        this.totalProducts = data.products.length;
        this.totalStock = data.products.reduce((sum, p) => sum + p.stockQuantity, 0);
        this.totalInvoices = data.invoices.length;
        this.openInvoices = data.invoices.filter(i => i.status === 1).length;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }
}
