import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BillingService } from './services/billing.service';
import { Invoice } from './models/invoice.model';
import { CreateInvoiceModalComponent } from './modals/create-invoice-modal/create-invoice-modal.component';
import { CreateInvoiceItem } from './models/create-invoice-item.model';
import { InvoicePrintComponent } from './modals/invoice-print/invoice-print.component';

@Component({
  selector: 'app-billing',
  standalone: true,
  imports: [CommonModule, CreateInvoiceModalComponent, InvoicePrintComponent],
  templateUrl: './billing.component.html',
  styleUrls: ['./billing.component.scss'],
})
export class BillingComponent implements OnInit {
  invoices: Invoice[] = [];
  products: any[] = [];

  selectedInvoice: Invoice | null = null;
  isLoading = true;
  isPrintModalOpen = false;
  showPrintButton = false;
  showCreateModal = false;
  loadingPrint = false;

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
    this.selectedInvoice = null;
    this.loadingPrint = true;

    this.showPrintButton = false;

    this.isPrintModalOpen = true;

    this.billingService.getById(inv.id).subscribe({
      next: (full) => {
        this.selectedInvoice = full;
        this.loadingPrint = false;
      },
      error: () => (this.loadingPrint = false),
    });
  }

  printInvoice(inv: Invoice) {
    this.isPrintModalOpen = true;
    this.loadingPrint = true;

    this.showPrintButton = true;

    this.billingService.getById(inv.id).subscribe({
      next: (full) => {
        this.selectedInvoice = full;
        this.loadingPrint = false;
      },
    });
  }

  createInvoice() {
    this.showCreateModal = true;
  }

  saveInvoice(items: CreateInvoiceItem[]) {
    const payload = {
      items: items.map((i) => ({
        productId: i.productId,
        code: i.code,
        description: i.description,
        quantity: i.quantity,
        unitPrice: i.unitPrice,
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
