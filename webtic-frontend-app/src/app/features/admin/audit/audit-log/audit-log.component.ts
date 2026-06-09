import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuditService, AuditLogDto } from '../../../../core/services/audit.service';
import { HasRoleDirective } from '../../../../shared/directives/has-role.directive';

@Component({
  selector: 'app-audit-log',
  standalone: true,
  imports: [CommonModule, HasRoleDirective],
  templateUrl: './audit-log.component.html',
  styleUrls: ['./audit-log.component.css']
})
export class AuditLogComponent implements OnInit {
  logs: AuditLogDto[] = [];
  totalItems = 0;
  currentPage = 1;
  pageSize = 10;
  isLoading = true;
  errorMessage = '';

  constructor(private auditService: AuditService) {}

  ngOnInit(): void {
    this.loadLogs();
  }

  loadLogs() {
    this.isLoading = true;
    this.auditService.getAuditLogs(this.currentPage, this.pageSize).subscribe({
      next: (data) => {
        this.logs = data.items;
        this.totalItems = data.totalItems;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Error al cargar los logs de auditoría';
        this.isLoading = false;
      }
    });
  }

  onPageChange(page: number) {
    this.currentPage = page;
    this.loadLogs();
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  getPageArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }
}
