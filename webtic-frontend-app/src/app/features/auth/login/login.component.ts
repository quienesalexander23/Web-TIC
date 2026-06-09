import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  isLoading = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^[a-zA-Z0-9._%+-]+@epn\.edu\.ec$/)]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  get emailControl() {
    return this.loginForm.get('email');
  }

  get passwordControl() {
    return this.loginForm.get('password');
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const credentials = this.loginForm.value;

    this.authService.login(credentials).subscribe({
      next: (response) => {
        sessionStorage.setItem('token', response.token);
        this.isLoading = false;
        this.router.navigate(['/admin/users']);
      },
      error: (error) => {
        this.isLoading = false;
        console.error('Login error details:', error);
        
        if (error.status === 0) {
          this.errorMessage = 'Error de conexión con el servidor. Verifica que la API esté corriendo (dotnet run) y que hayas aceptado el certificado HTTPS.';
        } else if (error.status === 401 || error.status === 400 || error.status === 404) {
          this.errorMessage = error.error?.message || 'Credenciales incorrectas.';
        } else if (error.status === 423) {
          // HTTP 423 Locked
          this.router.navigate(['/account-locked']);
        } else {
          this.errorMessage = `Error interno del servidor (${error.status}). Inténtalo más tarde.`;
        }
      }
    });
  }
}
