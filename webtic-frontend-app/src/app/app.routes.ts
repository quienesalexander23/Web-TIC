import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

const adminOnly = roleGuard(['Administrador']);
// La auditoría permite Docente además de Administrador: el acceso real de lectura
// lo decide el permiso granular del backend (ver AuditController.HasAuditReadPermissionAsync),
// esta guarda solo evita que otros roles ni siquiera intenten cargar la pantalla.
const auditAccess = roleGuard(['Administrador', 'Docente']);

export const routes: Routes = [
  { path: 'login', loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent) },
  { path: 'account-locked', loadComponent: () => import('./features/auth/account-locked/account-locked.component').then(m => m.AccountLockedComponent) },
  { path: 'forgot-password', loadComponent: () => import('./features/auth/forgot-password/forgot-password.component').then(m => m.ForgotPasswordComponent) },
  { path: 'reset-password', loadComponent: () => import('./features/auth/reset-password/reset-password.component').then(m => m.ResetPasswordComponent) },
  {
    path: 'admin',
    canActivate: [authGuard],
    loadComponent: () => import('./features/admin/dashboard/dashboard.component').then(m => m.DashboardComponent),
    children: [
      { path: 'home', canActivate: [adminOnly], loadComponent: () => import('./features/admin/dashboard-home/dashboard-home.component').then(m => m.DashboardHomeComponent) },
      { path: 'users', canActivate: [adminOnly], loadComponent: () => import('./features/admin/users/user-list/user-list.component').then(m => m.UserListComponent) },
      { path: 'roles', canActivate: [adminOnly], loadComponent: () => import('./features/admin/roles/role-management/role-management.component').then(m => m.RoleManagementComponent) },
      { path: 'profile', loadComponent: () => import('./features/admin/users/user-profile/user-profile.component').then(m => m.UserProfileComponent) },
      { path: 'audit', canActivate: [auditAccess], loadComponent: () => import('./features/admin/audit/audit-log/audit-log.component').then(m => m.AuditLogComponent) },
      // Vistas de demostración de los otros 2 módulos del proyecto (datos estáticos).
      // Sin guarda de rol propia: el backend decide acceso real vía permiso granular.
      { path: 'consultas-reportes', loadComponent: () => import('./features/admin/consultas-reportes/consultas-reportes.component').then(m => m.ConsultasReportesComponent) },
      { path: 'propuestas-tic', loadComponent: () => import('./features/admin/propuestas-tic/propuestas-tic.component').then(m => m.PropuestasTicComponent) },
      { path: '', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];
