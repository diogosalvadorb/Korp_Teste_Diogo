import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

interface InvoiceItem {
  id: number;
  productId: number;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  formattedUnitPrice: string;
  formattedTotalPrice: string;
}

interface Invoice {
  id: number;
  number: number;
  status: number;
  createdAt: string;
  formattedDate: string;
  items: InvoiceItem[];
  totalAmount: number;
  formattedTotal: string;
  canPrint: boolean;
}

@Component({
  selector: 'app-billing',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './billing.component.html',
  styleUrls: ['./billing.component.scss'],
})
export class BillingComponent implements OnInit {
  invoices: Invoice[] = [];
  isLoading = true;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices() {
    this.http.get<Invoice[]>('https://localhost:7200/api/invoices').subscribe({
      next: (data) => {
        this.invoices = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Erro ao carregar notas fiscais:', err);
        this.isLoading = false;
      },
    });
  }

  getStatusLabel(status: number) {
    if (status === 2) return 'Aberto';
    if (status === 1) return 'Fechado';
    return 'Desconhecido';
  }

  getStatusClass(status: number) {
    if (status === 2) return 'status-open';
    if (status === 1) return 'status-closed';
    return '';
  }

  createInvoice() {
    console.log('Criar nova nota fiscal');
  }

  viewInvoice(invoice: Invoice) {
    console.log('Visualizar NF:', invoice);
  }

  printInvoice(invoice: Invoice) {
    console.log('Imprimir NF:', invoice);
  }
}
