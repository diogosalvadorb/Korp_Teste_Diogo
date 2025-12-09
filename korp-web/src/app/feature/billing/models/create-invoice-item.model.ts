export interface CreateInvoiceItem {
  productId: number;
  quantity: number;
  code: string;
  description: string;
  unitPrice: number;
}
