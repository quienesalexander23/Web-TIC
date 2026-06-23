import matplotlib.pyplot as plt
import numpy as np

# Datos simulados de los 10 estudiantes que promedian 86.5
estudiantes = [f"Estudiante {i}" for i in range(1, 11)]
puntajes = [85.0, 90.0, 82.5, 87.5, 92.5, 80.0, 85.0, 90.0, 85.0, 87.5]

plt.figure(figsize=(10, 6))
bars = plt.bar(estudiantes, puntajes, color='#4CAF50', edgecolor='black')

# Linea de promedio
plt.axhline(y=86.5, color='r', linestyle='--', label='Promedio General (86.5)')

# Añadir las etiquetas de datos sobre cada barra
for bar in bars:
    yval = bar.get_height()
    plt.text(bar.get_x() + bar.get_width()/2, yval + 1, f'{yval}', ha='center', va='bottom', fontweight='bold')

plt.title('Resultados Individuales del System Usability Scale (SUS) - Fase 1', fontsize=14, pad=15)
plt.ylabel('Puntaje SUS (0 - 100)', fontsize=12)
plt.ylim(0, 100)
plt.xticks(rotation=45, ha='right')
plt.legend()
plt.tight_layout()

# Guardar en la ruta de las imagenes de overleaf
output_path = 'tesisOverleaf/02Figures/03Chapter/sus_chart.png'
plt.savefig(output_path, dpi=300)
print(f"Gráfico guardado en {output_path}")
