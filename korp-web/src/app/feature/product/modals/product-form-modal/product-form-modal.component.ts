import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CreateProductModel } from '../../models/create-product.model';
import { UpdateProductModel } from '../../models/update-product.model';

@Component({
  selector: 'app-product-form-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-form-modal.component.html',
  styleUrls: ['./product-form-modal.component.scss']
})
export class ProductFormModalComponent {

  @Input() isEdit = false;
  @Input() formData!: CreateProductModel | UpdateProductModel;
  @Input() errors: string[] = [];

  @Output() onSave = new EventEmitter<any>();
  @Output() onCancel = new EventEmitter<void>();

  save() {
    this.onSave.emit(this.formData);
  }

  cancel() {
    this.onCancel.emit();
  }
}
