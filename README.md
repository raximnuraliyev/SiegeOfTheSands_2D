# 🏰 Siege of the Sands

**Siege of the Sands** is a 2D grid-based tactical lane-defense game built in Unity. Set in a gritty, atmospheric medieval desert, players must strategically deploy Crusader units to defend their fortress against escalating waves of Saracen infantry, spearmen, and elite vanguard units.

![Main Menu](docs/main_menu.png)
*(Screenshot: A cinematic main menu featuring custom Gothic UI elements and immersive background art.)*

---

## 🎮 Gameplay Features

*   **Grid-Based Tactical Deployment:** Place defenders on a dynamic 2D orthographic grid, managing space and lane coverage.
*   **Dynamic Wave Spawning:** Custom wave management system that scales difficulty, mixing light infantry with heavy elite units across multiple levels.
*   **Resource Management:** Deploy "Water Bearer" economic units to generate gold over time, funding heavier front-line defenses like King Baldwin IV and Shield Bearers.
*   **Multi-Level Campaign:** A continuous level progression system that advances the player through increasingly difficult siege stages.
*   **Custom Audio Management:** A lightweight, centralized Singleton audio manager handling combat SFX, economy sounds, and victory horns.

![Gameplay Screen](docs/gameplay_grid.png)
*(Screenshot: Mid-battle tactical placement showing the UI gold counter, base health, and unit deployment.)*

---

## 🛠️ Technical Architecture

The project emphasizes clean code organization, maintainability, and the use of core software design patterns (such as Singleton for managers and component-based design for unit health/combat). 

### Folder Structure
The core application logic and assets are sandboxed within the `Assets/_Project/` directory to keep the repository clean from third-party library clutter:

*   📂 **`Animations/`** - State-driven animation controllers for combat units.
*   📂 **`Fonts/`** - Converted TextMeshPro SDF assets (Angel Wish, Old London).
*   📂 **`Prefabs/`** - Modular, pre-configured GameObjects for all defenders, enemies, and grid tiles.
*   📂 **`Scripts/`** - C# game logic divided into functional domains (Economy, Spawning, Combat, UI).
*   📂 **`Sprites/`** - High-resolution 2D UI elements and border-defined grid textures.

---

## 🚀 Installation & Setup

### Play the Game (Windows)
A compiled, standalone installer is available for Windows PCs.
1. Download `SiegeOfTheSands_Setup.exe` from the latest [Releases](#) tab.
2. Run the installer and follow the setup wizard.
3. Launch the game from your desktop shortcut!

### For Developers
To run this project locally in the Unity Editor:
1. Clone the repository:
   ```bash
   git clone [https://github.com/raximnuraliyev/SiegeOfTheSands_2D.git](https://github.com/raximnuraliyev/SiegeOfTheSands_2D.git)