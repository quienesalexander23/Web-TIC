import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

// Protege rutas admin-only por rol (Home, Users, Roles, Audit, Settings): antes,
// solo el nav *appHasRole ocultaba el enlace, pero navegar directo por URL a
// /admin/users, /admin/roles, etc. no tenía ningún control a nivel de ruta.
export function roleGuard(allowedRoles: string[]): CanActivateFn {
  return () => {
    const router = inject(Router);
    const token = sessionStorage.getItem('token');

    if (!token) {
      router.navigate(['/login']);
      return false;
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/')));
      let userRoles = payload['role'] || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || [];
      if (!Array.isArray(userRoles)) userRoles = [userRoles];

      if (allowedRoles.some(role => userRoles.includes(role))) {
        return true;
      }
    } catch {
      // token ilegible: cae al bloqueo de abajo
    }

    router.navigate(['/admin/profile']);
    return false;
  };
}
