import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MockModulesService, PropuestaTic } from '../../../core/services/mock-modules.service';

@Component({
  selector: 'app-propuestas-tic',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './propuestas-tic.component.html',
  styleUrl: './propuestas-tic.component.css'
})
export class PropuestasTicComponent implements OnInit {
  items: PropuestaTic[] = [];
  isLoading = true;
  accessDenied = false;

  constructor(private mockModulesService: MockModulesService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.isLoading = true;
    this.accessDenied = false;
    this.mockModulesService.getPropuestasTic().subscribe({
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
