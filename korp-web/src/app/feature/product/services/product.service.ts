import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { CreateProductModel } from '../models/create-product.model';
import { UpdateProductModel } from '../models/update-product.model';

@Injectable({ providedIn: 'root' })
export class ProductService {

  private readonly baseUrl = "https://localhost:7100/api/products";

  constructor(private http: HttpClient) {}

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  create(data: CreateProductModel): Observable<any> {
    return this.http.post(this.baseUrl, data);
  }

  update(data: UpdateProductModel): Observable<any> {
    return this.http.put(`${this.baseUrl}/${data.id}`, data);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
