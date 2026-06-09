import { Component, OnInit } from '@angular/core';
import { CommonModule, KeyValuePipe } from '@angular/common';
import { DashboardService, DashboardStats } from '../../../core/services/dashboard.service';

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [CommonModule, KeyValuePipe],
  templateUrl: './dashboard-home.component.html',
  styleUrls: ['./dashboard-home.component.css']
})
export class DashboardHomeComponent implements OnInit {
  stats: DashboardStats | null = null;
  isLoading = true;
  errorMessage = '';

  constructor(private dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.loadStats();
  }

  loadStats() {
    this.isLoading = true;
    this.dashboardService.getStats().subscribe({
      next: (data) => {
        this.stats = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'No se pudieron cargar las estadísticas del dashboard.';
        this.isLoading = false;
      }
    });
  }
}
