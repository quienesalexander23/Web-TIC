import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService, UsuarioDto } from '../../../../core/services/user.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  profileForm: FormGroup;
  profile: UsuarioDto | null = null;
  isEditing = false;
  isLoading = true;
  successMessage = '';
  errorMessage = '';

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.profileForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]]
    });
  }

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile() {
    this.isLoading = true;
    this.userService.getMyProfile().subscribe({
      next: (data) => {
        this.profile = data;
        this.profileForm.patchValue({
          firstName: data.firstName,
          lastName: data.lastName
        });
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Error al cargar el perfil';
        this.isLoading = false;
      }
    });
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
    this.successMessage = '';
    this.errorMessage = '';
    if (!this.isEditing && this.profile) {
      // Revertir cambios si se cancela
      this.profileForm.patchValue({
        firstName: this.profile.firstName,
        lastName: this.profile.lastName
      });
    }
  }

  saveProfile() {
    if (this.profileForm.invalid) return;

    const dto = {
      firstName: this.profileForm.value.firstName,
      lastName: this.profileForm.value.lastName
    };

    this.userService.updateMyProfile(dto).subscribe({
      next: (res) => {
        this.successMessage = res.message || 'Perfil actualizado exitosamente';
        this.isEditing = false;
        if (this.profile) {
          this.profile.firstName = dto.firstName;
          this.profile.lastName = dto.lastName;
        }
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Error al actualizar el perfil';
      }
    });
  }
}
