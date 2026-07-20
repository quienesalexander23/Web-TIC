import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuditService, AuditLogDto, AuditFilters } from '../../../../core/services/audit.service';
import { UserService, UsuarioDto } from '../../../../core/services/user.service';
import { HasRoleDirective } from '../../../../shared/directives/has-role.directive';

@Component({
  selector: 'app-audit-log',
  standalone: true,
  imports: [CommonModule, FormsModule, HasRoleDirective],
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

  actionTypes: string[] = [];
  users: UsuarioDto[] = [];
  selectedActionType = '';
  selectedUserId = '';
  fromDate = '';
  toDate = '';
  isExporting = false;

  constructor(private auditService: AuditService, private userService: UserService) {}

  ngOnInit(): void {
    this.loadActionTypes();
    this.loadUsers();
    this.loadLogs();
  }

  loadActionTypes() {
    this.auditService.getActionTypes().subscribe({
      next: (types) => (this.actionTypes = types),
      error: () => {}
    });
  }

  loadUsers() {
    this.userService.getUsers(1, 1000).subscribe({
      next: (res) => (this.users = res.items),
      error: () => {}
    });
  }

  private get activeFilters(): AuditFilters {
    return {
      actionType: this.selectedActionType || undefined,
      userId: this.selectedUserId || undefined,
      fromDate: this.fromDate || undefined,
      toDate: this.toDate || undefined
    };
  }

  loadLogs() {
    this.isLoading = true;
    this.auditService.getAuditLogs(this.currentPage, this.pageSize, this.activeFilters).subscribe({
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

  applyFilters() {
    this.currentPage = 1;
    this.loadLogs();
  }

  clearFilters() {
    this.selectedActionType = '';
    this.selectedUserId = '';
    this.fromDate = '';
    this.toDate = '';
    this.applyFilters();
  }

  exportToCsv() {
    this.isExporting = true;
    this.auditService.exportCsv(this.activeFilters).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `auditoria_${new Date().toISOString().slice(0, 10)}.csv`;
        a.click();
        window.URL.revokeObjectURL(url);
        this.isExporting = false;
      },
      error: () => {
        this.errorMessage = 'Error al exportar el CSV';
        this.isExporting = false;
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
