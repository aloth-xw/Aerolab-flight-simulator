# Aerolab
Simulador de vuelo y lanzamiento de cohetes de código abierto

## Índice
 
- [Objetivo](#-objetivo)
- [Estado actual](#-estado-actual)
- [Características](#-características)
- [Roadmap](#-roadmap)
- [Contribuir](#-contribuir)

  ## Objetivo
 
Crear una aplicación de simulación que permita:
 
- Pilotar distintos tipos de aeronaves y cohetes con físicas simplificadas
- Aprender conceptos de vuelo (empuje, sustentación, arrastre, gravedad) de forma práctica
- Experimentar lanzamientos a órbita
- Implementar un asistente IA
**Público objetivo:** estudiantes de bachillerato/FP/universidad relacionados con aeronáutica o ingeniería, y aficionados a la aviación y el espacio sin acceso a simuladores profesionales
  

## Estado actual
 
| Módulo | Estado |
|---|---|
| Motor de físicas básico | Por empezar |
| Vehículo controlable (v1) | Por empezar |
| Interfaz gráfica (HUD) | Por empezar |
| Escenario/planeta base | Por empezar |
| Asistente IA | Por empezar |
| Múltiples motores/aviones | Futuro (post-MVP) |
| Múltiples planetas | Futuro (post-MVP) |


## Características
 
### Incluidas en el MVP (versión para el certamen)
- Un vehículo controlable (avión o cohete) con física simplificada de vuelo/lanzamiento.
- Un escenario/planeta jugable.
- HUD con telemetría en tiempo real (altitud, velocidad, combustible, ángulo).
- Asistente IA básico que interpreta el estado del vuelo y da avisos/consejos contextuales.
### Visión a futuro (roadmap, no incluidas en el MVP)
- Selección entre varios motores y modelos de aeronaves/cohetes, cada uno con curvas de rendimiento propias.
- Varios cuerpos celestes/planetas con distinta gravedad y atmósfera.
- Modo carrera o misiones guiadas para aprendizaje progresivo.
- Asistente IA conversacional (integración con LLM) capaz de responder preguntas libres sobre la física del vuelo.
- Multijugador/comparativa de resultados.


## Roadmap
 
- [x] Fase 1 — Definición y diseño (interfaz, modelo físico, alcance del MVP)
- [ ] Fase 2 — Motor de físicas y vehículo controlable
- [ ] Fase 3 — Interfaz gráfica y HUD
- [ ] Fase 4 — Asistente IA (v1, basado en reglas)
- [ ] Fase 5 — corrección de errores
- [ ] Fase 6 — Documentación y vídeo de presentación
- [ ] Fase 7 Ampliación: más motores, aviones y planetas; asistente IA conversacional


## Contribuir
 
Este proyecto está en fase inicial. Si quieres colaborar:
 
1. Haz un fork del repositorio.
2. Crea una rama descriptiva (`feature/motor-fisicas`, `fix/hud-altitud`...).
3. Haz commit de tus cambios con mensajes claros.
4. Abre un Pull Request describiendo qué añade/soluciona.
Cualquier sugerencia de diseño, física o IA es bienvenida a través de los Issues.
 
 
