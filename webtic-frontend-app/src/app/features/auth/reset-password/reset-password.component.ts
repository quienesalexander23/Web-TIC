import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
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

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    // Patrón para verificar mínimo 8 caracteres, mayúscula, minúscula y número
    const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\w\W]{8,}$/;
    
    this.resetForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.pattern(passwordPattern)]],
      confirmPassword: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator });
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.email = params['email'] || '';
      this.token = params['token'] || '';
      
      if (!this.email || !this.token) {
        this.errorMessage = 'Enlace de recuperación inválido o incompleto.';
      }
    });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('newPassword')?.value === g.get('confirmPassword')?.value
      ? null : { mismatch: true };
  }

  get newPasswordControl() { return this.resetForm.get('newPassword'); }
  get confirmPasswordControl() { return this.resetForm.get('confirmPassword'); }

  onSubmit() {
    if (this.resetForm.invalid || !this.email || !this.token) {
      this.resetForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.successMessage = '';
    this.errorMessage = '';

    const payload = {
      email: this.email,
      token: this.token,
      newPassword: this.resetForm.value.newPassword
    };

    this.authService.resetPassword(payload).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.successMessage = 'Tu contraseña ha sido restablecida exitosamente. Redirigiendo...';
        setTimeout(() => this.router.navigate(['/login']), 3000);
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Hubo un error al restablecer la contraseña. El token puede haber expirado.';
      }
    });
  }
}
