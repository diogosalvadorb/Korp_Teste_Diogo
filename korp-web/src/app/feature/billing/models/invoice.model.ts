import { InvoiceItem } from './invoice-item.model';

export interface Invoice {
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
