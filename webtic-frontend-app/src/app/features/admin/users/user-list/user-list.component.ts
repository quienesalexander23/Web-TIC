import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { UserService, UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto } from '../../../../core/services/user.service';
import { RoleService, RoleDto } from '../../../../core/services/role.service';
import { HasRoleDirective } from '../../../../shared/directives/has-role.directive';
import { ToastService } from '../../../../core/services/toast.service';
import { ConfirmDialogService } from '../../../../core/services/confirm-dialog.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, HasRoleDirective],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit, OnDestroy {

  users: UsuarioDto[] = [];
  roles: RoleDto[] = [];
  loading = false;
  totalItems = 0;

  // Búsqueda, filtros y paginación (CP2-04/05/06)
  searchTerm = '';
  selectedRole = '';
  selectedStatus = ''; // '', 'true' (Activo), 'false' (Inactivo)
  currentPage = 1;
  pageSize = 10;
  private searchSubject = new Subject<string>();

  // Modal State
  isModalOpen = false;
  isEditing = false;
  currentUserId: string | null = null;
  userForm: FormGroup;
  isSaving = false;

  constructor(
    private userService: UserService,
    private roleService: RoleService,
    private fb: FormBuilder,
    private toastService: ToastService,
    private confirmDialogService: ConfirmDialogService
  ) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^[a-zA-Z0-9._%+-]+@epn\.edu\.ec$/)]],
      role: ['', Validators.required]
    });

    this.searchSubject.pipe(
      debounceTime(350),
      distinctUntilChanged()
    ).subscribe(() => {
      this.currentPage = 1;
      this.loadUsers();
    });
  }

  ngOnInit() {
    this.loadUsers();
    this.loadRoles();
  }

  ngOnDestroy() {
    this.searchSubject.complete();
  }

  loadUsers() {
    this.loading = true;
    const isActive = this.selectedStatus === '' ? undefined : this.selectedStatus === 'true';

    this.userService.getUsers(this.currentPage, this.pageSize, this.searchTerm || undefined, this.selectedRole || undefined, isActive).subscribe({
      next: (res) => {
        this.users = res.items;
        this.totalItems = res.totalItems;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching users', err);
        this.loading = false;
      }
    });
  }

  loadRoles() {
    this.roleService.getRoles().subscribe({
      next: (res) => this.roles = res,
      error: (err) => console.error('Error fetching roles', err)
    });
  }

  onSearchInput(value: string) {
    this.searchTerm = value;
    this.searchSubject.next(value);
  }

  onRoleFilterChange() {
    this.currentPage = 1;
    this.loadUsers();
  }

  onStatusFilterChange() {
    this.currentPage = 1;
    this.loadUsers();
  }

  onPageChange(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.loadUsers();
  }

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.totalItems / this.pageSize));
  }

  get lastItemOnPage(): number {
    return Math.min(this.currentPage * this.pageSize, this.totalItems);
  }

  getPageArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  openUserModal(user?: UsuarioDto) {
    this.isModalOpen = true;
    if (user) {
      this.isEditing = true;
      this.currentUserId = user.id;
      this.userForm.patchValue({
        firstName: user.firstName,
        lastName: user.lastName,
        email: user.email,
        role: user.roles.length > 0 ? user.roles[0] : ''
      });
      // Email cannot be changed during edit
      this.userForm.get('email')?.disable();
    } else {
      this.isEditing = false;
      this.currentUserId = null;
      this.userForm.reset();
      this.userForm.get('email')?.enable();
    }
  }

  closeModal() {
    this.isModalOpen = false;
    this.userForm.reset();
  }

  get emailControl() { return this.userForm.get('email'); }
  get firstNameControl() { return this.userForm.get('firstName'); }
  get lastNameControl() { return this.userForm.get('lastName'); }
  get roleControl() { return this.userForm.get('role'); }

  onSubmit() {
    if (this.userForm.invalid) {
      this.userForm.markAllAsTouched();
      return;
    }

    this.isSaving = true;
    const formValue = this.userForm.getRawValue();

    if (this.isEditing && this.currentUserId) {
      const updateDto: UpdateUsuarioDto = {
        firstName: formValue.firstName,
        lastName: formValue.lastName,
        role: formValue.role
      };
      this.userService.updateUser(this.currentUserId, updateDto).subscribe({
        next: () => {
          this.loadUsers();
          this.closeModal();
          this.isSaving = false;
          this.toastService.success('Usuario actualizado exitosamente.');
        },
        error: (err) => {
          console.error(err);
          this.isSaving = false;
          this.toastService.error(err.error?.message || 'Error al actualizar usuario.');
        }
      });
    } else {
      const createDto: CreateUsuarioDto = {
        email: formValue.email,
        firstName: formValue.firstName,
        lastName: formValue.lastName,
        role: formValue.role
      };
      this.userService.createUser(createDto).subscribe({
        next: () => {
          this.loadUsers();
          this.closeModal();
          this.isSaving = false;
          this.toastService.success('Usuario creado exitosamente. Se ha enviado un correo con las credenciales temporales.');
        },
        error: (err) => {
          console.error(err);
          this.isSaving = false;
          this.toastService.error(err.error?.message || 'Error al crear usuario.');
        }
      });
    }
  }

  async toggleStatus(user: UsuarioDto) {
    const confirmed = await this.confirmDialogService.confirm(
      `¿Estás seguro de ${user.isActive ? 'desactivar' : 'activar'} a ${user.firstName} ${user.lastName}?`,
      { title: user.isActive ? 'Desactivar usuario' : 'Activar usuario', confirmText: user.isActive ? 'Desactivar' : 'Activar', variant: user.isActive ? 'danger' : 'default' }
    );
    if (!confirmed) return;

    this.userService.toggleStatus(user.id).subscribe({
      next: () => {
        this.loadUsers();
        this.toastService.success(`Usuario ${user.isActive ? 'desactivado' : 'activado'} exitosamente.`);
      },
      error: (err) => {
        console.error(err);
        this.toastService.error('Error al cambiar el estado del usuario.');
      }
    });
  }

  async unlockUser(user: UsuarioDto) {
    const confirmed = await this.confirmDialogService.confirm(
      `¿Estás seguro de desbloquear a ${user.firstName} ${user.lastName}?`,
      { title: 'Desbloquear cuenta', confirmText: 'Desbloquear' }
    );
    if (!confirmed) return;

    this.userService.unlockUser(user.id).subscribe({
      next: () => {
        this.toastService.success('Usuario desbloqueado exitosamente. Se ha enviado un correo con instrucciones para restablecer su contraseña.');
        this.loadUsers();
      },
      error: (err) => {
        console.error(err);
        this.toastService.error('Error al desbloquear usuario.');
      }
    });
  }
}
