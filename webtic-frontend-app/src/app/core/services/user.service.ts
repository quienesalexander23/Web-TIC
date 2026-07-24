import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface UsuarioDto {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  isActive: boolean;
  isLockedOut?: boolean;
  roles: string[];
}

export interface PaginatedResult<T> {
  items: T[];
  totalItems: number;
  page: number;
  pageSize: number;
}

export interface CreateUsuarioDto {
  email: string;
  firstName: string;
  lastName: string;
  role: string;
}

export interface UpdateUsuarioDto {
  firstName: string;
  lastName: string;
  role: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/api/usuarios`;

  constructor(private http: HttpClient) { }

  getUsers(page: number = 1, pageSize: number = 10, search?: string, role?: string, isActive?: boolean): Observable<PaginatedResult<UsuarioDto>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (search) params = params.set('search', search);
    if (role) params = params.set('role', role);
    if (isActive !== undefined) params = params.set('isActive', isActive.toString());

    return this.http.get<PaginatedResult<UsuarioDto>>(this.apiUrl, { params });
  }

  createUser(dto: CreateUsuarioDto): Observable<any> {
    return this.http.post(this.apiUrl, dto);
  }

  updateUser(id: string, dto: UpdateUsuarioDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  toggleStatus(userId: string): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${userId}/estado`, {});
  }

  unlockUser(userId: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${userId}/unlock`, {});
  }

  getMyProfile(): Observable<UsuarioDto> {
    return this.http.get<UsuarioDto>('/api/profile/me');
  }

  updateMyProfile(dto: { firstName: string, lastName: string }): Observable<any> {
    return this.http.put('/api/profile/me', dto);
  }
}
