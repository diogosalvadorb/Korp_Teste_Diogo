import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface Toast {
  id: number;
  message: string;
  type: 'success' | 'error' | 'info';
}

@Injectable({ providedIn: 'root' })
export class ToastService {
  private toastSubject = new Subject<Toast>();
  toasts$ = this.toastSubject.asObservable();
  private counter = 0;

  show(message: string, type: "success" | "error" | "info" = "success") {
    const toast: Toast = {
      id: this.counter++,
      message,
      type
    };
    this.toastSubject.next(toast);
  }

  success(message: string) {
    this.show(message, "success");
  }

  error(message: string) {
    this.show(message, "error");
  }

  info(message: string) {
    this.show(message, "info");
  }
}
