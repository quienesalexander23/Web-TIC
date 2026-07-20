import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleService, RoleDto, SystemPermission } from '../../../../core/services/role.service';
import { FormsModule } from '@angular/forms';
import { ToastService } from '../../../../core/services/toast.service';

@Component({
  selector: 'app-role-management',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './role-management.component.html',
  styleUrl: './role-management.component.css'
})
export class RoleManagementComponent implements OnInit {
  roles: RoleDto[] = [];
  activeRole: RoleDto | null = null;
  permissions: SystemPermission[] = [];

  loadingRoles = false;
  loadingPermissions = false;
  saving = false;

  constructor(private roleService: RoleService, private toastService: ToastService) {}

  ngOnInit() {
    this.loadRoles();
  }

  loadRoles() {
    this.loadingRoles = true;
    this.roleService.getRoles().subscribe({
      next: (res) => {
        this.roles = res;
        this.loadingRoles = false;
        if (this.roles.length > 0) {
          this.selectRole(this.roles[0]);
        }
      },
      error: (err) => {
        console.error(err);
        this.loadingRoles = false;
      }
    });
  }

  selectRole(role: RoleDto) {
    this.activeRole = role;
    this.loadPermissions(role.name);
  }

  loadPermissions(roleName: string) {
    this.loadingPermissions = true;
    this.roleService.getPermissionsByRole(roleName).subscribe({
      next: (res) => {
        this.permissions = res;
        this.groupPermissions();
        this.loadingPermissions = false;
      },
      error: (err) => {
        console.error(err);
        this.loadingPermissions = false;
      }
    });
  }

  groupedPermissions: { moduleName: string, items: SystemPermission[] }[] = [];

  groupPermissions() {
    const map = new Map<string, SystemPermission[]>();
    for (const p of this.permissions) {
      if (!map.has(p.moduleName)) {
        map.set(p.moduleName, []);
      }
      map.get(p.moduleName)!.push(p);
    }
    this.groupedPermissions = Array.from(map.entries()).map(([moduleName, items]) => ({
      moduleName,
      items
    }));
  }

  savePermissions() {
    if (!this.activeRole) return;
    
    this.saving = true;
    this.roleService.savePermissions(this.activeRole.name, this.permissions).subscribe({
      next: () => {
        this.saving = false;
        this.toastService.success('Permisos guardados exitosamente.');
      },
      error: (err) => {
        console.error(err);
        this.saving = false;
        this.toastService.error('Error al guardar permisos.');
      }
    });
  }
}
