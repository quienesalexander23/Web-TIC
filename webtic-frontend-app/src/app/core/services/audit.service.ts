import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedResult } from './user.service';

export interface AuditLogDto {
  id: string;
  actionType: string;
  userId: string;
  timestamp: string;
  ipAddress: string;
  details: string;
}

export interface AuditFilters {
  actionType?: string;
  userId?: string;
  fromDate?: string;
  toDate?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuditService {
  private apiUrl = '/api/audit';

  constructor(private http: HttpClient) { }

  private buildFilterParams(filters?: AuditFilters): HttpParams {
    let params = new HttpParams();
    if (filters?.actionType) params = params.set('actionType', filters.actionType);
    if (filters?.userId) params = params.set('userId', filters.userId);
    if (filters?.fromDate) params = params.set('fromDate', filters.fromDate);
    if (filters?.toDate) params = params.set('toDate', filters.toDate);
    return params;
  }

  getAuditLogs(page: number = 1, pageSize: number = 10, filters?: AuditFilters): Observable<PaginatedResult<AuditLogDto>> {
    let params = this.buildFilterParams(filters)
      .set('pageNumber', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<PaginatedResult<AuditLogDto>>(this.apiUrl, { params });
  }

  getActionTypes(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/action-types`);
  }

  exportCsv(filters?: AuditFilters): Observable<Blob> {
    const params = this.buildFilterParams(filters);
    return this.http.get(`${this.apiUrl}/export`, { params, responseType: 'blob' });
  }
}
