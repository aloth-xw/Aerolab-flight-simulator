# Aerolab
Simulador de vuelo y lanzamiento de cohetes de código abierto pensado para estudiantes y apasionados de la aviación/aeroespacial como alternativa accesible a simuladores comerciales de alto coste (X-Plane, Microsoft Flight Simulator, Kerbal Space Program)

## Índice
 
- [Objetivo](#-objetivo)
- [Estado actual](#-estado-actual)
- [Características](#-características)
- [Arquitectura y stack tecnológico](#-arquitectura-y-stack-tecnológico)
- [Estructura del repositorio](#-estructura-del-repositorio)
- [Instalación y ejecución](#-instalación-y-ejecución)
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
| Diseño de interfaz | ✅ Bocetado |
| Documento de diseño (GDD) | ✅ En progreso avanzado |
| Matriz de requisitos | ✅ Definida (v1.1) |
| Motor de físicas básico | 🔲 Por empezar |
| Vehículo controlable (v1) | 🔲 Por empezar |
| Interfaz gráfica (HUD) implementada | 🔲 Por empezar |
| Escenario/planeta base | 🔲 Por empezar |
| Asistente IA | 🔲 Por empezar |
| Múltiples motores/aviones | 🔲 Futuro (post-MVP) |
| Múltiples planetas | 🔲 Futuro (post-MVP) |

Consulta el [Game Design Document completo](docs/GDD.md) para el detalle de arquitectura, requisitos y diseño de interfaz.


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


## Arquitectura y stack tecnológico
 
- **Motor:** Unity (versión LTS más reciente, ej. Unity 6 LTS / 2022 LTS)
- **Lenguaje:** C#
- **Física:** Unity Physics (Rigidbody) + modelo simplificado propio de aerodinámica/cohetería (empuje, arrastre, sustentación) sobre `FixedUpdate`
- **Render Pipeline:** URP (Universal Render Pipeline), para mantener buen rendimiento en equipos modestos
- **UI:** UI Toolkit o Canvas/uGUI para el HUD
- **IA / Asistente:** sistema basado en reglas (máquina de estados / árbol de decisión en C#) para el MVP; posible integración con API de LLM en fases posteriores
- **Control de versiones:** Git + GitHub (con `.gitignore` específico de Unity)
- **Gestión de tareas:** GitHub Projects / Issues


  ## Estructura del repositorio
 
```
/
├── README.md
├── .gitignore               # .gitignore estándar de Unity
├── docs/                    # Documentación, memoria del proyecto, diseño
│   ├── GDD.md               # Game Design Document completo
│   ├── media/                # Capturas/mockups referenciados en el GDD
│   └── memoria-certamen.md
├── media/                   # Capturas y vídeo de demo para el certamen
├── Assets/                  # Proyecto Unity
│   ├── Scripts/
│   │   ├── Vehicles/        # Control y física de aviones/cohetes
│   │   ├── Physics/         # Modelo de físicas de vuelo (empuje, arrastre...)
│   │   ├── AI/               # Lógica del asistente IA
│   │   ├── UI/                # Interfaz gráfica y HUD
│   │   └── Core/              # GameManager, input, utilidades generales
│   ├── Scenes/               # Escenarios/planetas y menús
│   ├── Prefabs/               # Vehículos, elementos de UI, efectos
│   ├── Models/                # Modelos 3D
│   ├── Materials/             # Materiales y shaders
│   ├── Textures/
│   ├── Audio/
│   └── Settings/              # Perfiles de URP, input actions
├── Packages/                  # Dependencias del Package Manager (manifest.json)
└── ProjectSettings/           # Configuración del proyecto Unity
```

## Instalación y ejecución
 
**Requisitos:**
- [Unity Hub](https://unity.com/download)
- Unity Editor (versión LTS recomendada — anotar aquí la versión exacta usada en el proyecto, ej. `6000.x LTS`)
- Git
 
**Pasos:**
 
```bash
# Clonar el repositorio
git clone https://github.com/usuario/nombre-repo.git
```
 
1. Abrir Unity Hub → **Add project from disk** → seleccionar la carpeta clonada.
2. Unity Hub detectará automáticamente la versión requerida (o pedirá instalarla).
3. Abrir el proyecto y cargar la escena principal en `Assets/Scenes/`.
4. Pulsar Play para probar en el editor, o **File → Build Settings** para generar un ejecutable.

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

