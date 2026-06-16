import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent implements OnInit {
  resetForm: FormGroup;
  isLoading = false;
  successMessage = '';
  errorMessage = '';
  
  email = '';
  token = '';
  showPassword = false;

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.resetForm = this.fb.group({
      newPassword: ['', [
        Validators.required, 
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
      ]],
      confirmPassword: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator });
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.email = params['email'] || '';
      this.token = params['token'] || '';
      
      if (!this.email || !this.token) {
        this.errorMessage = 'Enlace de recuperación inválido.';
      }
    });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('newPassword')?.value === g.get('confirmPassword')?.value
      ? null : { 'mismatch': true };
  }

  get newPasswordControl() {
    return this.resetForm.get('newPassword');
  }

  get confirmPasswordControl() {
    return this.resetForm.get('confirmPassword');
  }

  get hasMinLength() {
    return (this.newPasswordControl?.value || '').length >= 8;
  }

  get hasUpper() {
    return /[A-Z]/.test(this.newPasswordControl?.value || '');
  }

  get hasNumber() {
    return /\d/.test(this.newPasswordControl?.value || '');
  }

  get hasSpecial() {
    return /[@$!%*?&]/.test(this.newPasswordControl?.value || '');
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  onSubmit() {
    if (this.resetForm.invalid || !this.email || !this.token) {
      this.resetForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const data = {
      email: this.email,
      token: this.token,
      newPassword: this.resetForm.value.newPassword
    };

    this.authService.resetPassword(data).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.successMessage = response.message || 'Contraseña actualizada correctamente. Redirigiendo...';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 3000);
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.message || 'Error al restablecer contraseña.';
      }
    });
  }
}
