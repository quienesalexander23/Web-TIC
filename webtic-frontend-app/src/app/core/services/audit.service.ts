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

@Injectable({
  providedIn: 'root'
})
export class AuditService {
  private apiUrl = 'http://localhost:5080/api/audit';

  constructor(private http: HttpClient) { }

  getAuditLogs(page: number = 1, pageSize: number = 10): Observable<PaginatedResult<AuditLogDto>> {
    let params = new HttpParams()
      .set('pageNumber', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<PaginatedResult<AuditLogDto>>(this.apiUrl, { params });
  }
}
