import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UsuarioDto {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  isActive: boolean;
  roles: string[];
}

export interface PaginatedResult<T> {
  items: T[];
  totalItems: number;
  page: number;
  pageSize: number;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7220/api/usuarios';

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

  toggleStatus(userId: string): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${userId}/estado`, {});
  }
}
