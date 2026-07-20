import { Injectable, signal } from '@angular/core';

export interface ConfirmRequest {
  title: string;
  message: string;
  confirmText: string;
  cancelText: string;
  variant: 'default' | 'danger';
}

// Reemplaza el confirm() nativo del navegador por un modal propio,
// consistente con el resto de la UI (mismo estilo que el modal de logout).
@Injectable({
  providedIn: 'root'
})
export class ConfirmDialogService {
  readonly request = signal<ConfirmRequest | null>(null);
  private resolver: ((value: boolean) => void) | null = null;

  confirm(
    message: string,
    options?: Partial<Pick<ConfirmRequest, 'title' | 'confirmText' | 'cancelText' | 'variant'>>
  ): Promise<boolean> {
    this.request.set({
      title: options?.title ?? 'Confirmar acción',
      message,
      confirmText: options?.confirmText ?? 'Confirmar',
      cancelText: options?.cancelText ?? 'Cancelar',
      variant: options?.variant ?? 'default'
    });

    return new Promise<boolean>(resolve => {
      this.resolver = resolve;
    });
  }

  respond(value: boolean): void {
    this.resolver?.(value);
    this.resolver = null;
    this.request.set(null);
  }
}
