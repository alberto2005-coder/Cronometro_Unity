# Cronómetro Unity

[![Unity](https://img.shields.io/badge/Unity-2021.3%20LTS%2B-000000?logo=unity&logoColor=white)](https://unity.com/)
[![URP](https://img.shields.io/badge/Render-URP-2C8EBB)](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)
[![MIT](https://img.shields.io/badge/Licencia-MIT-green)](LICENSE)

[Español](#español) · [English](#english)

---

## Español

### Índice

- [Descripción](#descripción)
- [Características](#características)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Requisitos](#requisitos)
- [Instalación](#instalación)
- [Uso](#uso)
- [Licencia](#licencia)

### Descripción

Cronómetro funcional para **Unity** con soporte de **vueltas (laps)**, detección de la vuelta más rápida y la más lenta, y una interfaz limpia pensada para leerse de un vistazo. Sirve como herramienta independiente o como base para juegos y aplicaciones que necesiten medir tiempos.

### Características

- **Cronometraje preciso** con minutos, segundos y milisegundos.
- **Sistema de vueltas**: registra tantas vueltas como quieras con un clic.
- **Resaltado automático**: la vuelta más rápida se muestra en lila y la más lenta en rojo.
- **Interfaz configurable** desde el Inspector: colores, grosor de borde y tipografías.
- **Diseño responsivo**: el canvas se adapta a distintas resoluciones y relaciones de aspecto.
- **TextMesh Pro** para texto nítido a cualquier escala.

### Estructura del proyecto

```text
Cronometro_Unity/
├── cronómetro/
│   ├── Assets/
│   │   ├── Scripts/
│   │   │   ├── Cronometro.cs          # Lógica del cronómetro
│   │   │   ├── ControladorVueltaUI.cs # Prefab y presentación de cada vuelta
│   │   │   └── Readme.cs
│   │   ├── Editor/                    # Utilidades de editor (anclajes, readme)
│   │   ├── Scenes/SampleScene.unity   # Escena de demostración
│   │   ├── Settings/                  # Perfiles URP para PC y móvil
│   │   ├── TextMesh Pro/               # Fuentes y materiales
│   │   └── art/                        # Sprites de botones y fondos
│   └── ProjectSettings/
├── LICENSE
└── README.md
```

### Requisitos

- **Unity 2021.3 LTS** o superior (recomendado).
- **Universal Render Pipeline (URP)**, ya configurado en `Assets/Settings`.
- Paquete **TextMesh Pro** (incluido en las versiones modernas; el editor lo pedirá si falta).
- No requiere paquetes de terceros ni conexión a red.

### Instalación

1. Clona el repositorio:
   ```bash
   git clone https://github.com/alberto2005-coder/Cronometro_Unity.git
   ```
2. Abre **Unity Hub → Add → Add project from disk** y selecciona la carpeta `cronómetro`.
3. Abre la escena `Assets/Scenes/SampleScene.unity`.
4. Si Unity avisa de que faltan recursos de TextMesh Pro, elige **Window → TextMeshPro → Import TMP Essential Resources**.
5. Pulsa **Play**.

### Uso

| Acción | Resultado |
|---|---|
| **Start** | Arranca el cronómetro |
| **Lap** | Registra la vuelta actual y la añade a la lista |
| **Stop** | Detiene la cuenta sin perder las vueltas |
| **Reset** | Vuelve a cero y limpia la lista de vueltas |

Los colores de cada vuelta se recalculan al añadir una nueva: la más rápida y la más lenta se recolorean automáticamente.

### Licencia

**MIT** — consulta [LICENSE](LICENSE).

---

## English

### Table of contents

- [Overview](#overview)
- [Features](#features)
- [Project structure](#project-structure)
- [Requirements](#requirements)
- [Installation](#installation)
- [Usage](#usage)
- [License](#license)

### Overview

A working **Unity** stopwatch with **lap support**, fastest and slowest lap detection, and a clean interface designed to be read at a glance. It works as a standalone tool or as a base for games and apps that need to measure time.

### Features

- **Precise timing** down to minutes, seconds and milliseconds.
- **Lap system**: record as many laps as you need with one click.
- **Automatic highlighting**: the fastest lap turns lilac and the slowest turns red.
- **Configurable interface** from the Inspector: colours, outline thickness and fonts.
- **Responsive layout**: the canvas adapts to different resolutions and aspect ratios.
- **TextMesh Pro** for crisp text at any scale.

### Project structure

```text
Cronometro_Unity/
├── cronómetro/
│   ├── Assets/
│   │   ├── Scripts/
│   │   │   ├── Cronometro.cs          # Stopwatch logic
│   │   │   ├── ControladorVueltaUI.cs # Lap prefab and presentation
│   │   │   └── Readme.cs
│   │   ├── Editor/                    # Editor helpers (anchors, readme)
│   │   ├── Scenes/SampleScene.unity   # Demo scene
│   │   ├── Settings/                  # URP profiles for desktop and mobile
│   │   ├── TextMesh Pro/               # Fonts and materials
│   │   └── art/                        # Button and background sprites
│   └── ProjectSettings/
├── LICENSE
└── README.md
```

### Requirements

- **Unity 2021.3 LTS** or later (recommended).
- **Universal Render Pipeline (URP)**, already configured under `Assets/Settings`.
- **TextMesh Pro** package (bundled with modern versions; the editor prompts you if it is missing).
- No third-party packages and no network access required.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/alberto2005-coder/Cronometro_Unity.git
   ```
2. Open **Unity Hub → Add → Add project from disk** and pick the `cronómetro` folder.
3. Open `Assets/Scenes/SampleScene.unity`.
4. If Unity warns about missing TextMesh Pro resources, choose **Window → TextMeshPro → Import TMP Essential Resources**.
5. Press **Play**.

### Usage

| Action | Result |
|---|---|
| **Start** | Starts the stopwatch |
| **Lap** | Records the current lap and appends it to the list |
| **Stop** | Pauses the count without losing laps |
| **Reset** | Returns to zero and clears the lap list |

Lap colours are recalculated whenever a new one is added: the fastest and slowest rows recolour themselves automatically.

### License

**MIT** — see [LICENSE](LICENSE).
