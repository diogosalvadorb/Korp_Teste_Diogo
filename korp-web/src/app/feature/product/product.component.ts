import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

interface Product {
  id: number;
  code: string;
  description: string;
  price: number;
  stockQuantity: number;
  formattedPrice: string;
  stockStatus: string;
}

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  products: Product[] = [];
  isLoading = true;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.http.get<Product[]>('https://localhost:7100/api/products')
      .subscribe({
        next: (data) => {
          this.products = data;
          this.isLoading = false;
        },
        error: () => this.isLoading = false
      });
  }

  createProduct() {
    console.log("Cadastrar produto");
  }

  editProduct(product: Product) {
    console.log("Editar produto:", product);
  }

  deleteProduct(product: Product) {
    console.log("Excluir produto:", product);
  }

  searchProduct(event: any) {
    const value = event.target.value;
    console.log("Pesquisar:", value);
  }
}
