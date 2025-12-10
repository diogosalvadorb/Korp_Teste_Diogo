import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from './models/product.model';
import { CreateProductModel } from './models/create-product.model';
import { UpdateProductModel } from './models/update-product.model';
import { ProductService } from './services/product.service';
import { ProductFormModalComponent } from './modals/product-form-modal/product-form-modal.component';
import { ProductDeleteModalComponent } from './modals/product-delete-modal/product-delete-modal.component';
import { ToastService } from '../../core/service/toast.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [
    CommonModule,
    ProductFormModalComponent,
    ProductDeleteModalComponent
  ],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  products: Product[] = [];
  isLoading = true;
  showFormModal = false;
  showDeleteModal = false;
  selectedProduct: Product | null = null;
  formData!: CreateProductModel | UpdateProductModel;
  formErrors: string[] = [];

  constructor(private productService: ProductService, private toastService: ToastService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.isLoading = true;

    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
        this.isLoading = false;
      },
      error: () => {
        this.toastService.error("Erro ao carregar produtos");
        this.isLoading = false;
      }
    });
  }

  createProduct() {
    this.selectedProduct = null;

    this.formData = {
      code: '',
      description: '',
      price: 0,
      stockQuantity: 0
    };

    this.showFormModal = true;
  }

  editProduct(product: Product) {
    this.selectedProduct = product;

    this.formData = {
      id: product.id,
      code: product.code,
      description: product.description,
      price: product.price,
      stockQuantity: product.stockQuantity
    };

    this.showFormModal = true;
  }

  saveProduct(data: any) {
    const isEdit = !!this.selectedProduct;
    const request = isEdit
      ? this.productService.update(data)
      : this.productService.create(data);

    request.subscribe({
      next: () => {
        this.loadProducts();
        this.showFormModal = false;
        this.formErrors = [];

        const message = isEdit
          ? "Produto atualizado com sucesso!"
          : "Produto cadastrado com sucesso!";
        this.toastService.success(message);
      },
      error: (err) => {
        this.formErrors = this.extractErrors(err);
      }
    });
  }

  extractErrors(err: any): string[] {
    const errors: string[] = [];

    if (err.error?.errors) {
      for (const key of Object.keys(err.error.errors)) {
        errors.push(...err.error.errors[key]);
      }
    }

    if (err.error?.message) {
      errors.push(err.error.message);
    }

    if (errors.length === 0) {
      errors.push("Ocorreu um erro inesperado ao salvar o produto.");
    }

    return errors;
  }

  deleteProduct(product: Product) {
    this.selectedProduct = product;
    this.showDeleteModal = true;
  }

  confirmDelete() {
    if (!this.selectedProduct) return;

    this.productService.delete(this.selectedProduct.id).subscribe({
      next: () => {
        this.loadProducts();
        this.showDeleteModal = false;
        this.toastService.success("Produto excluído com sucesso!");
      },
      error: () => {
        this.toastService.error("Erro ao excluir produto");
      }
    });
  }
}
