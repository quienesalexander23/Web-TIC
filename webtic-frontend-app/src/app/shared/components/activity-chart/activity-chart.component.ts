import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivityByDay } from '../../../core/services/dashboard.service';

// Gráfica SVG real (sin dependencias externas) de la actividad de auditoría de
// los últimos 7 días. Reemplaza las barras de progreso CSS estáticas que
// solo representaban distribución de roles, no una serie temporal real
// (cierra el hallazgo: la tesis, Fig. 2.14, describe "gráfica de actividad").
@Component({
  selector: 'app-activity-chart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './activity-chart.component.html',
  styleUrls: ['./activity-chart.component.css']
})
export class ActivityChartComponent {
  @Input() set data(value: ActivityByDay[] | null | undefined) {
    this._data = value ?? [];
    this.recompute();
  }
  get data(): ActivityByDay[] {
    return this._data;
  }

  private _data: ActivityByDay[] = [];

  readonly chartWidth = 400;
  readonly chartHeight = 160;
  readonly barGap = 12;

  bars: { x: number; y: number; width: number; height: number; label: string; count: number }[] = [];
  maxCount = 0;

  private recompute(): void {
    const n = this._data.length;
    if (n === 0) {
      this.bars = [];
      return;
    }
    this.maxCount = Math.max(1, ...this._data.map(d => d.count));
    const barWidth = (this.chartWidth - this.barGap * (n + 1)) / n;

    this.bars = this._data.map((d, i) => {
      const height = (d.count / this.maxCount) * (this.chartHeight - 30);
      return {
        x: this.barGap + i * (barWidth + this.barGap),
        y: this.chartHeight - 20 - height,
        width: barWidth,
        height,
        label: this.formatDay(d.date),
        count: d.count
      };
    });
  }

  private formatDay(iso: string): string {
    const date = new Date(iso + 'T00:00:00');
    return date.toLocaleDateString('es-EC', { weekday: 'short' }).replace('.', '');
  }
}
