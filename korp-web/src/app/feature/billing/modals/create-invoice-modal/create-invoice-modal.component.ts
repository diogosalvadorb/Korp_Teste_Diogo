import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CreateInvoiceItem } from '../../models/create-invoice-item.model';
import { Product } from '../../../product/models/product.model';
import { ProductService } from '../../../product/services/product.service';

@Component({
  selector: 'app-create-invoice-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-invoice-modal.component.html',
  styleUrls: ['./create-invoice-modal.component.scss'],
})
export class CreateInvoiceModalComponent implements OnInit {
  @Output() onConfirm = new EventEmitter<CreateInvoiceItem[]>();
  @Output() onCancel = new EventEmitter<void>();

  products: Product[] = [];

  items: CreateInvoiceItem[] = [
    {
      productId: 0,
      code: '',
      description: '',
      quantity: 1,
      unitPrice: 0
    }
  ];

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.productService.getAll().subscribe(res => this.products = res);
  }

  addItem() {
    this.items.push({
      productId: 0,
      code: '',
      description: '',
      quantity: 1,
      unitPrice: 0
    });
  }

  removeItem(index: number) {
    this.items.splice(index, 1);
  }

  updateUnitPrice(i: number) {
    const selected = this.products.find(
      p => p.id === Number(this.items[i].productId)
    );

    if (selected) {
      this.items[i].unitPrice = selected.price;
      this.items[i].code = selected.code;
      this.items[i].description = selected.description;
    }
  }

  confirm() {
    this.onConfirm.emit(this.items);
  }

  cancel() {
    this.onCancel.emit();
  }
}
