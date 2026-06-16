import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  forgotForm: FormGroup;
  isLoading = false;
  successMessage = '';
  errorMessage = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.forgotForm = this.fb.group({
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^[a-zA-Z0-9._%+-]+@epn\.edu\.ec$/)]]
    });
  }

  get emailControl() {
    return this.forgotForm.get('email');
  }

  onSubmit() {
    if (this.forgotForm.invalid) {
      this.forgotForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.successMessage = '';
    this.errorMessage = '';

    const email = this.forgotForm.value.email;

    this.authService.forgotPassword(email).subscribe({
      next: (res: any) => {
        this.isLoading = false;
        this.successMessage = res.message || 'Se ha enviado un enlace a tu correo institucional si está registrado.';
      },
      error: (err: any) => {
        this.isLoading = false;
        this.errorMessage = 'Hubo un error al procesar tu solicitud. Inténtalo más tarde.';
      }
    });
  }
}
