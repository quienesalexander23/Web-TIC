import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface SystemPermission {
  id: string;
  moduleName: string;
  resourceName: string;
  canRead: boolean;
  canWrite: boolean;
  canApprove: boolean;
  canDelete: boolean;
  roleId: string;
}

export interface RoleDto {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private apiUrl = 'http://localhost:5080/api/RolePermissions';

  constructor(private http: HttpClient) { }

  getRoles(): Observable<RoleDto[]> {
    return this.http.get<RoleDto[]>(`${this.apiUrl}/roles`);
  }

  getPermissionsByRole(roleName: string): Observable<SystemPermission[]> {
    return this.http.get<SystemPermission[]>(`${this.apiUrl}/${roleName}`);
  }

  savePermissions(roleName: string, permissions: SystemPermission[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/${roleName}`, permissions);
  }
}
