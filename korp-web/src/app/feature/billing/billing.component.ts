import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BillingService } from './services/billing.service';
import { Invoice } from './models/invoice.model';
import { CreateInvoiceModalComponent } from './modals/create-invoice-modal/create-invoice-modal.component';

@Component({
  selector: 'app-billing',
  standalone: true,
  imports: [CommonModule, CreateInvoiceModalComponent],
  templateUrl: './billing.component.html',
  styleUrls: ['./billing.component.scss'],
})
export class BillingComponent implements OnInit {
  invoices: Invoice[] = [];
  products: any[] = [];
  isLoading = true;

  showCreateModal = false;

  constructor(private billingService: BillingService) {}

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices() {
    this.billingService.getAll().subscribe({
      next: (data) => {
        this.invoices = data;
        this.isLoading = false;
      },
      error: () => (this.isLoading = false),
    });
  }

  getStatusLabel(status: number): string {
    if (status === 1) return 'Aberto';
    if (status === 2) return 'Fechado';
    return 'Desconhecido';
  }

  getStatusClass(status: number): string {
    if (status === 1) return 'status-open';
    if (status === 2) return 'status-closed';
    return '';
  }

  viewInvoice(inv: Invoice) {
    console.log('Visualizar NF:', inv);
  }

  printInvoice(inv: Invoice) {
    console.log('Imprimir NF:', inv);
  }

  createInvoice() {
    this.showCreateModal = true;
  }

  saveInvoice(items: any[]) {
    const payload = {
      items: items.map((i) => ({
        productId: Number(i.productId),
        quantity: Number(i.quantity),
        unitPrice: Number(i.unitPrice),
      })),
    };

    this.billingService.create(payload).subscribe({
      next: () => {
        this.showCreateModal = false;
        this.loadInvoices();
      },
    });
  }
}
