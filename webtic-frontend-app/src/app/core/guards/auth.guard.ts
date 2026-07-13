import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

// Protege las rutas /admin/*: si no hay un token en sessionStorage, redirige a
// /login en vez de renderizar el shell de la página (cierra el hallazgo CP1-12).
export const authGuard: CanActivateFn = () => {
  const router = inject(Router);
  const token = sessionStorage.getItem('token');

  if (!token) {
    router.navigate(['/login']);
    return false;
  }

  return true;
};
