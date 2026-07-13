import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token?: string;
  expiration?: string;
  requires2FA?: boolean;
  email?: string;
  message?: string;
}

export interface Verify2FARequest {
  email: string;
  code: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = '/api/auth';

  constructor(private http: HttpClient) { }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, request);
  }

  verify2FA(request: Verify2FARequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/verify-2fa`, request);
  }

  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/forgot-password`, { email });
  }

  resetPassword(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/reset-password`, data);
  }

  logout(): void {
    // Revocar el token en el backend (agrega su jti a la lista negra) antes de
    // borrarlo de sessionStorage — el interceptor de auth necesita el token
    // todavía presente para adjuntar el header Authorization a esta petición.
    // Es "best-effort": si la llamada falla, igual limpiamos la sesión local.
    this.http.post(`${this.apiUrl}/logout`, {}).subscribe({ error: () => {} });
    sessionStorage.removeItem('token');
  }
}
