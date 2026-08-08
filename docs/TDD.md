# Technical Design Document

Aerolab utiliza un modelo físico simplificado para representar el comportamiento de aeronaves atmosféricas y espaciales. El objetivo no es reproducir un simulador profesional, sino proporcionar un modelo coherente para que el usuario pueda experimentar y comprender conceptos fundamentales de física del vuelo. Este proyecto está abierto a ampliaciones.

**Objetivos técnicos: ** 
-Separar la simulación física de la presentación visual
-Utilizar un modelo aerodinámico comprensible y modificable
-Permitir diferentes tipos de vehículos en el futuro con posibilidad de experimentar libremente con los modelos
-Mantener una arquitectura modular (MUY modular)
-Utilizar Unity para integrar, renderizar y simulación dinámica con Rigidbody

## 1. Arquitectura

## 2. Sistemas de coordenadas

## 3. Modelo físico

### 3.1 Atmósfera

 Empezamos con ρ=cte
 Calculamos viento relativo con Vrel​=Vaircraft​−Vwind​

 El viento relativo se calcula inicialmente en coordenadas globales como la diferencia entre la velocidad del vehículo y la velocidad del viento. Posteriormente, el vector se transforma al sistema local de cada superficie aerodinámica para calcular el ángulo de ataque y el sideslip.

 **Ángulo de ataque: **α será positivo cuando el flujo relativo incida por debajo del eje longitudinal del vehículo y produzca una sustentación positiva.

 β representará la componente lateral del flujo relativo. El modelo inicial utilizará principalmente α para el cálculo de sustentación, mientras que β podrá utilizarse más adelante para fuerzas y momentos laterales.

 
### 3.5 Resistencia
 D=0.5​ρV^2SCD con dirección opuesta al movimiento relativo del aire


### 3.6 Sustentación
**Calcular magnitud**
 L=0.5​ρV^2SCL
 
 **Calcular vector**
 La geometría del flujo y la orientación del vehículo determinan hacia dónde aplicamos la fuerza.​

 
### 3.7 Empuje
 T=Tf donde f es el vector forward de la aeronave​

### 3.7 Gravedad​
 g=(0,−9.81,0) en el sistema global
 Fg=mg

## 4. Modelo de clases
World
  │-Gravity
  │-atmosphere
      │-wind
      │-density
      
PhysicsEngine
  │-Update()
  
Flightcontrols(throttle, elevator, aileron, rudder, flaps, geardown)

Aircraft
  │-PhysicsBody
      │-position
      │-velocity
      │-orientation
      │-angularVel
      |-getforwardVector()
      |-getupVector()
      |-getrightVector()
  │-Aircraftconfig
      │-emptymass
      │-wing
         │-area
         │-airfoil
            │-getCL(AoA)
            │-getCD(AoA)
            |-tabla de equivalencias
       │-engine
         |-MaxThrust
         |-GetThrust()
  |-getMass()


## 5. Bucle de simulación
FixedUpdate
     │
     ▼
Leer controles
     │
     ▼
Actualizar estado del vehículo
     │
     ▼
Obtener velocidad relativa
     │
     ▼
Transformar a coordenadas locales
     │
     ▼
Calcular AoA / sideslip
     │
     ▼
Calcular CL / CD
     │
     ▼
Calcular fuerzas
     │
     ▼
Transformar fuerzas a global
     │
     ▼
Sumar fuerzas
     │
     ▼
Rigidbody.AddForce(...)


## 6. Controles de vuelo
  Throttle → potencia/empuje
  Elevator → pitch
  Aileron  → roll
  Rudder   → yaw
  
## 7. Decisiones de diseño
 El proyecto utilizará Rigidbody como representación física del vehículo. Aerolab calculará las fuerzas aerodinámicas y de propulsión mediante un modelo propio y las aplicará al Rigidbody.

 **HUD**
    Altitude
    Airspeed
    Vertical speed
    AoA
    Throttle
    Fuel

  **Asistente IA educativa**
   Estado del avión
       ↓
     Reglas
       ↓
   Detectar situación
       ↓
  Mensaje educativo

  
## 8. Futuras ampliaciones
helicópteros
cohetes
diferentes planetas
modelos atmosféricos
diferentes perfiles alares
momentos aerodinámicos
varios vehículos
misiones educativas
IA conversacional
colaboración con artistas
escenarios locales
