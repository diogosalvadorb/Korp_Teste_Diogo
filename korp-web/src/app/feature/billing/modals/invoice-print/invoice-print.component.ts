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
  successMessage = "";
  errorMessages: string[] = [];

  constructor(private billingService: BillingService) {}

  get canPrint(): boolean {
    return this.invoice?.status === 1 && this.showPrintButton;
  }

  printNow() {
    if (!this.invoice || !this.canPrint) return;

    this.printing = true;
    this.successMessage = "";
    this.errorMessages = [];

    this.billingService.printInvoice(this.invoice.id).subscribe({
      next: (res: any) => {
        this.printing = false;
        this.successMessage =
          res.message || "Nota fiscal impressa com sucesso.";

        setTimeout(() => {
          this.printed.emit();
          this.close.emit();
        }, 3200);
      },
      error: (err) => {
        this.printing = false;
        this.errorMessages = this.extractErrors(err);
      },
    });
  }

  private extractErrors(err: any): string[] {
    if (err.error?.message) {
      return [err.error.message];
    }

    if (typeof err.error === 'string') {
      try {
        const parsed = JSON.parse(err.error);
        if (parsed.message) return [parsed.message];
      } catch {}
    }

    if (err.error?.errors) {
      const validationErrors: string[] = [];
      for (const key of Object.keys(err.error.errors)) {
        validationErrors.push(...err.error.errors[key]);
      }
      if (validationErrors.length > 0) return validationErrors;
    }

    return ['Ocorreu um erro inesperado ao imprimir a nota fiscal.'];
  }
}
