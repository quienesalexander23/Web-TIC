import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MockModulesService, PropuestaConsulta } from '../../../core/services/mock-modules.service';

@Component({
  selector: 'app-consultas-reportes',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './consultas-reportes.component.html',
  styleUrl: './consultas-reportes.component.css'
})
export class ConsultasReportesComponent implements OnInit {
  items: PropuestaConsulta[] = [];
  isLoading = true;
  accessDenied = false;

  constructor(private mockModulesService: MockModulesService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.isLoading = true;
    this.accessDenied = false;
    this.mockModulesService.getConsultasReportes().subscribe({
      next: (data) => {
        this.items = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        this.accessDenied = err.status === 403;
      }
    });
  }
}
