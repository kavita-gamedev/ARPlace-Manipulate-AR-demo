# ARPlace-Manipulate-AR-demo

# ARPlace & Manipulate — Simple AR Demo

## Overview

This is a minimal Unity AR demo project that allows you to:

* Place a prefab onto detected planes using **ARRaycastManager** (tap to place).
* Rotate the placed object with a two-finger rotation gesture.
* Zoom (scale) the placed object with pinch gesture.
* Reset / remove the placed object via a UI button.

The project is intended as a starting point for experimenting with AR Foundation object placement and interaction.

---

## Requirements

* **Unity** 2020.3 LTS or newer (tested with Unity 2021/2022).
* **AR Foundation** (4.x or newer).
* Platform package depending on device:

  * **ARCore XR Plugin** (for Android).
  * **ARKit XR Plugin** (for iOS).
* A placeable prefab (pivot at base).

---

## Project Setup

1. Create a new Unity project using the **AR Template** or add AR Foundation manually.
2. Add the following objects in your Scene:

   * `AR Session`
   * `AR Session Origin` with:

     * `AR Camera`
     * `ARRaycastManager`
     * `ARPlaneManager`
3. Add an empty `ARManager` GameObject and attach the object placement script (from `Assets/Scripts/`).
4. Assign in the Inspector:

   * The `ARRaycastManager` reference.
   * The prefab you want to place.
5. (Optional) Add a `Canvas` with a Reset button linked to the UI script to reset/remove placed objects.

---

## Repository Structure

```
ProjectRoot/
├── Assets/
│   ├── Scripts/              # Contains AR interaction scripts
│   ├── Scenes/               # Example demo scene
│   └── Prefabs/              # Placeable prefab(s)
├── Packages/                 # Unity package manifest & lock files
├── ProjectSettings/          # Unity project settings
└── .gitignore                # Ignore build/temp files
```

---

## Git & Version Control

For your first commit, include only:

* `Assets/`
* `Packages/`
* `ProjectSettings/`

Exclude auto-generated folders like `Library/`, `Temp/`, `Obj/`, `Build/`, and IDE-specific files.

Use a Unity `.gitignore` template such as:

```
[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
[Bb]uilds/
[Ll]ogs/
.vs/
.idea/
*.csproj
*.sln
*.user
```

---

## How to Run

1. Clone this repository.
2. Open the project in Unity (version listed above).
3. Build and run on an ARCore- or ARKit-compatible device.
4. Point the device at a flat surface to detect planes and tap to place the prefab.

---

## License

This project is provided as a demo/template. You may use and modify it freely for your own projects.
