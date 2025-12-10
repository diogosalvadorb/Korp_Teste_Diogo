import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BillingService } from './services/billing.service';
import { Invoice } from './models/invoice.model';
import { CreateInvoiceModalComponent } from './modals/create-invoice-modal/create-invoice-modal.component';
import { CreateInvoiceItem } from './models/create-invoice-item.model';
import { InvoicePrintComponent } from './modals/invoice-print/invoice-print.component';
import { ToastService } from '../../core/service/toast.service';

@Component({
  selector: 'app-billing',
  standalone: true,
  imports: [CommonModule, CreateInvoiceModalComponent, InvoicePrintComponent],
  templateUrl: './billing.component.html',
  styleUrls: ['./billing.component.scss'],
})
export class BillingComponent implements OnInit {
  invoices: Invoice[] = [];
  selectedInvoice: Invoice | null = null;
  isLoading = true;
  isPrintModalOpen = false;
  showPrintButton = false;
  showCreateModal = false;

  constructor(private billingService: BillingService, private toastService: ToastService) {}

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices() {
    this.isLoading = true;

    this.billingService.getAll().subscribe({
      next: (data) => {
        this.invoices = data;
        this.isLoading = false;
      },
      error: () => {
        this.toastService.error("Erro ao carregar notas fiscais");
        this.isLoading = false;
      },
    });
  }

  getStatusLabel(status: number): string {
    return status === 1 ? 'Aberto' : 'Fechado';
  }

  getStatusClass(status: number): string {
    return status === 1 ? 'status-open' : 'status-closed';
  }

  viewInvoice(inv: Invoice) {
    this.openPrintModal(inv, false);
  }

  printInvoice(inv: Invoice) {
    this.openPrintModal(inv, true);
  }

  private openPrintModal(inv: Invoice, showPrintBtn: boolean) {
    this.selectedInvoice = null;
    this.showPrintButton = showPrintBtn;
    this.isPrintModalOpen = true;

    this.billingService.getById(inv.id).subscribe({
      next: (full) => {
        this.selectedInvoice = full;
      },
      error: () => {
        this.toastService.error("Erro ao carregar nota fiscal");
        this.isPrintModalOpen = false;
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
        this.toastService.success("Nota fiscal criada com sucesso!");
      },
      error: (err) => {
        const errorMessage = err.error?.message || "Erro ao criar nota fiscal";
        this.toastService.error(errorMessage);
      }
    });
  }
}
