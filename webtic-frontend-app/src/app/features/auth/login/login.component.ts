import { Component, OnDestroy, ElementRef, ViewChildren, QueryList } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnDestroy {
  @ViewChildren('digitInput') digitInputs!: QueryList<ElementRef>;
  
  loginForm: FormGroup;
  verifyForm: FormGroup;
  isLoading = false;
  showPassword = false;
  errorMessage = '';
  successMessage = '';
  requires2FA = false;
  userEmail = '';
  canResend = false;
  
  errorTitle = '';
  errorDescription = '';
  
  timeLeft = 120;
  timerInterval: any;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^[a-zA-Z0-9._%+-]+@epn\.edu\.ec$/)]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });

    this.verifyForm = this.fb.group({
      digit0: ['', [Validators.required, Validators.pattern(/^\d$/)]],
      digit1: ['', [Validators.required, Validators.pattern(/^\d$/)]],
      digit2: ['', [Validators.required, Validators.pattern(/^\d$/)]],
      digit3: ['', [Validators.required, Validators.pattern(/^\d$/)]],
      digit4: ['', [Validators.required, Validators.pattern(/^\d$/)]],
      digit5: ['', [Validators.required, Validators.pattern(/^\d$/)]]
    });
  }

  ngOnDestroy() {
    this.clearTimer();
  }

  startTimer() {
    this.timeLeft = 120; // 2 minutes
    this.canResend = false;
    this.timerInterval = setInterval(() => {
      if (this.timeLeft > 0) {
        this.timeLeft--;
      } else {
        this.clearTimer();
        this.errorMessage = 'El código ha expirado. Por favor, solicita uno nuevo.';
        this.canResend = true;
      }
    }, 1000);
  }

  clearTimer() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }

  get formattedTime() {
    const m = Math.floor(this.timeLeft / 60);
    const s = this.timeLeft % 60;
    return `${m}:${s.toString().padStart(2, '0')}`;
  }

  get emailControl() {
    return this.loginForm.get('email');
  }

  get passwordControl() {
    return this.loginForm.get('password');
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  get digits() {
    return [0, 1, 2, 3, 4, 5].map(i => this.verifyForm.get(`digit${i}`));
  }

  onDigitInput(event: any, index: number) {
    const input = event.target;
    // Solo permitir números
    input.value = input.value.replace(/[^0-9]/g, '');
    this.verifyForm.get(`digit${index}`)?.setValue(input.value);

    // Mover foco al siguiente
    if (input.value && index < 5) {
      const nextInput = this.digitInputs.toArray()[index + 1].nativeElement;
      nextInput.focus();
    }
  }

  onDigitKeyDown(event: KeyboardEvent, index: number) {
    if (event.key === 'Backspace' && !this.verifyForm.get(`digit${index}`)?.value && index > 0) {
      const prevInput = this.digitInputs.toArray()[index - 1].nativeElement;
      prevInput.focus();
    }
  }

  onDigitPaste(event: ClipboardEvent) {
    event.preventDefault();
    const pastedData = event.clipboardData?.getData('text/plain');
    if (pastedData && /^\d{6}$/.test(pastedData)) {
      for (let i = 0; i < 6; i++) {
        this.verifyForm.get(`digit${i}`)?.setValue(pastedData[i]);
      }
      this.digitInputs.toArray()[5].nativeElement.focus();
    }
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.errorTitle = '';
    this.errorDescription = '';
    this.successMessage = '';

    const credentials = this.loginForm.value;

    this.authService.login(credentials).subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.requires2FA) {
          this.requires2FA = true;
          this.userEmail = credentials.email;
          this.successMessage = response.message || 'Se ha enviado un código de seguridad a su correo.';
          
          this.startTimer();
          
          // Enfocar el primer input después de un breve delay
          setTimeout(() => {
            if (this.digitInputs?.first?.nativeElement) {
              this.digitInputs.first.nativeElement.focus();
            }
          }, 100);
        } else if (response.token) {
          sessionStorage.setItem('token', response.token);
          this.router.navigate(['/admin/users']);
        }
      },
      error: (error) => {
        this.isLoading = false;
        if (error.error?.errorType) {
          this.errorTitle = error.error.message;
          this.errorDescription = error.error.description;
        } else {
          this.errorMessage = `Error interno del servidor (${error.status}). Inténtalo más tarde.`;
        }
      }
    });
  }

  onVerify2FA() {
    if (this.verifyForm.invalid) {
      this.verifyForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const code = [0, 1, 2, 3, 4, 5].map(i => this.verifyForm.get(`digit${i}`)?.value).join('');

    const request = {
      email: this.userEmail,
      code: code
    };

    this.authService.verify2FA(request).subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.token) {
          this.clearTimer();
          sessionStorage.setItem('token', response.token);
          this.router.navigate(['/admin/users']);
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.message || 'Código incorrecto o expirado.';
      }
    });
  }

  resendCode() {
    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';
    
    // Limpiar los inputs
    for (let i = 0; i < 6; i++) {
      this.verifyForm.get(`digit${i}`)?.setValue('');
    }

    const credentials = this.loginForm.value;

    this.authService.login(credentials).subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.requires2FA) {
          this.successMessage = 'Se ha reenviado un nuevo código de seguridad a su correo.';
          this.startTimer();
          setTimeout(() => {
            if (this.digitInputs?.first?.nativeElement) {
              this.digitInputs.first.nativeElement.focus();
            }
          }, 100);
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.message || 'Error al reenviar el código.';
      }
    });
  }
}
