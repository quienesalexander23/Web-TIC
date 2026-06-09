import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: 'login', loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent) },
  { path: 'account-locked', loadComponent: () => import('./features/auth/account-locked/account-locked.component').then(m => m.AccountLockedComponent) },
  { path: 'forgot-password', loadComponent: () => import('./features/auth/forgot-password/forgot-password.component').then(m => m.ForgotPasswordComponent) },
  { path: 'reset-password', loadComponent: () => import('./features/auth/reset-password/reset-password.component').then(m => m.ResetPasswordComponent) },
  { 
    path: 'admin', 
    loadComponent: () => import('./features/admin/dashboard/dashboard.component').then(m => m.DashboardComponent),
    children: [
      { path: 'home', loadComponent: () => import('./features/admin/dashboard-home/dashboard-home.component').then(m => m.DashboardHomeComponent) },
      { path: 'users', loadComponent: () => import('./features/admin/users/user-list/user-list.component').then(m => m.UserListComponent) },
      { path: 'roles', loadComponent: () => import('./features/admin/roles/role-management/role-management.component').then(m => m.RoleManagementComponent) },
      { path: 'profile', loadComponent: () => import('./features/admin/users/user-profile/user-profile.component').then(m => m.UserProfileComponent) },
      { path: 'audit', loadComponent: () => import('./features/admin/audit/audit-log/audit-log.component').then(m => m.AuditLogComponent) },
      { path: '', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];
