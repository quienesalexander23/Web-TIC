import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService, UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto } from '../../../../core/services/user.service';
import { RoleService, RoleDto } from '../../../../core/services/role.service';
import { HasRoleDirective } from '../../../../shared/directives/has-role.directive';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HasRoleDirective],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {

  users: UsuarioDto[] = [];
  roles: RoleDto[] = [];
  loading = false;
  totalItems = 0;

  // Modal State
  isModalOpen = false;
  isEditing = false;
  currentUserId: string | null = null;
  userForm: FormGroup;
  isSaving = false;

  constructor(
    private userService: UserService,
    private roleService: RoleService,
    private fb: FormBuilder
  ) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^[a-zA-Z0-9._%+-]+@epn\.edu\.ec$/)]],
      role: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.loadUsers();
    this.loadRoles();
  }

  loadUsers() {
    this.loading = true;
    this.userService.getUsers(1, 50).subscribe({
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
          alert('Usuario actualizado exitosamente');
        },
        error: (err) => {
          console.error(err);
          this.isSaving = false;
          alert('Error al actualizar usuario');
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
          alert('Usuario creado exitosamente. Se ha enviado un correo con las credenciales temporales.');
        },
        error: (err) => {
          console.error(err);
          this.isSaving = false;
          const errorMsg = err.error?.message || 'Error al crear usuario';
          alert(errorMsg);
        }
      });
    }
  }

  toggleStatus(user: UsuarioDto) {
    if (confirm(`¿Estás seguro de ${user.isActive ? 'desactivar' : 'activar'} a ${user.firstName} ${user.lastName}?`)) {
      this.userService.toggleStatus(user.id).subscribe({
        next: () => this.loadUsers(),
        error: (err) => console.error(err)
      });
    }
  }

  unlockUser(user: UsuarioDto) {
    if (confirm(`¿Estás seguro de desbloquear a ${user.firstName} ${user.lastName}?`)) {
      this.userService.unlockUser(user.id).subscribe({
        next: () => {
          alert('Usuario desbloqueado exitosamente. Se ha enviado un correo con instrucciones para restablecer su contraseña.');
          this.loadUsers();
        },
        error: (err) => {
          console.error(err);
          alert('Error al desbloquear usuario');
        }
      });
    }
  }
}
