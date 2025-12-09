export interface InvoiceItem {
  id: number;
  productId: number;
  quantity: number;
  code: string;
  description: string;
  unitPrice: number;
  totalPrice: number;
  formattedUnitPrice: string;
  formattedTotalPrice: string;
}
