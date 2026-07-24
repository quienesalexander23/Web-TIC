import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface PropuestaConsulta {
  titulo: string;
  proponente: string;
  estudianteAsignado: string | null;
  estado: string;
  fechaUltimoCambio: string;
}

export interface PropuestaTic {
  titulo: string;
  docenteProponente: string;
  estado: string;
  fechaEnvio: string;
}

@Injectable({
  providedIn: 'root'
})
export class MockModulesService {
  private apiUrl = `${environment.apiUrl}/api/mock`;

  constructor(private http: HttpClient) { }

  getConsultasReportes(): Observable<PropuestaConsulta[]> {
    return this.http.get<PropuestaConsulta[]>(`${this.apiUrl}/consultas-reportes`);
  }

  getPropuestasTic(): Observable<PropuestaTic[]> {
    return this.http.get<PropuestaTic[]>(`${this.apiUrl}/propuestas-tic`);
  }
}
