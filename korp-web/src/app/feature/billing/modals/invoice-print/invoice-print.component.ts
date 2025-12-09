import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Invoice } from '../../models/invoice.model';
import { BillingService } from '../../services/billing.service';

@Component({
  selector: 'app-invoice-print',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invoice-print.component.html',
  styleUrls: ['./invoice-print.component.scss']
})
export class InvoicePrintComponent {

  @Input() invoice: Invoice | null = null;
  @Input() showPrintButton: boolean = false;
  @Output() close = new EventEmitter<void>();
  @Output() printed = new EventEmitter<void>();

  printing = false;
  message = "";

  constructor(private billingService: BillingService) {}

  get canPrint(): boolean {
    return this.invoice?.status === 1 && this.showPrintButton;
  }

  printNow() {
    if (!this.invoice || !this.canPrint) return;

    this.printing = true;
    this.message = "";

    this.billingService.printInvoice(this.invoice.id).subscribe({
      next: (res: any) => {
        this.printing = false;
        this.message = res.message || "Nota gerada com sucesso!";

        setTimeout(() => {
          this.printed.emit();
          this.close.emit();
        }, 3200);
      },
      error: (err) => {
        this.printing = false;
        this.message = err.error?.message || "Erro ao gerar nota.";
      }
    });
  }
}
