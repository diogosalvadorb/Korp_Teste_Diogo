import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Invoice } from '../models/invoice.model';
import { CreateInvoiceModel } from '../models/create-invoice.model';

@Injectable({ providedIn: 'root' })
export class BillingService {
  private baseUrl = 'https://localhost:7200/api/invoices';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(this.baseUrl);
  }

  getById(id: number): Observable<Invoice> {
    return this.http.get<Invoice>(`${this.baseUrl}/${id}`);
  }

  create(model: CreateInvoiceModel): Observable<any> {
    return this.http.post(this.baseUrl, model);
  }

  printInvoice(id: number) {
    return this.http.post(`${this.baseUrl}/${id}/print`, {});
  }
}
