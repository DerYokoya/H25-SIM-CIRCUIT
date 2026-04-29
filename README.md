# H25-SIM-CIRCUIT

A Unity-based circuit simulation project built with C#, ShaderLab, and HLSL.

---

## 📋 Table of Contents

- [About](#about)
- [Built With](#built-with)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Project Structure](#project-structure)
- [Usage](#usage)
- [License](#license)

---

## About

**H25-SIM-CIRCUIT** is a Unity simulation project focused on interactive circuit behavior. The project leverages custom shaders (ShaderLab/HLSL) alongside C# gameplay logic to create a visually rich and functional circuit simulation experience. It allows you to build, test, and analyze electric circuits in a virtual environment, replicating the behavior of real-world hardware, meaning the components will respond to changes in voltage or resistance as they would in a physical lab.

---

## Built With

| Technology | Purpose |
|---|---|
| **Unity** | Game engine / simulation framework |
| **C#** (~75%) | Core simulation logic, gameplay scripts |
| **ShaderLab** (~20%) | Custom visual shaders |
| **HLSL** (~5%) | Shader programming for GPU effects |

---

## Getting Started

### Prerequisites

- [Unity Hub](https://unity.com/download) installed
- Unity Editor (check `ProjectSettings/ProjectVersion.txt` for the required version)
- Visual Studio or any compatible C# IDE (see `.vsconfig` for recommended extensions)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/DerYokoya/H25-SIM-CIRCUIT.git
   cd H25-SIM-CIRCUIT
   ```

2. **Open in Unity Hub**
   - Launch Unity Hub
   - Click **Add** → **Add project from disk**
   - Select the cloned `H25-SIM-CIRCUIT` folder

3. **Open the project**
   - Unity will import all assets and packages automatically
   - Wait for the compilation to finish before playing

4. **Run the simulation**
   - Open the main scene from the `Assets/` folder
   - Press **Play** in the Unity Editor

---

## Project Structure

```
H25-SIM-CIRCUIT/
├── Assets/             # All game assets, scripts, scenes, and shaders
├── Packages/           # Unity Package Manager dependencies
├── ProjectSettings/    # Unity project configuration files
├── .gitattributes      # Git line-ending and diff settings
├── .gitignore          # Files excluded from version control
├── .vsconfig           # Visual Studio recommended extensions
└── LICENSE             # MIT License
```

---

## Usage

Once the project is running in the Unity Editor:

- Load the desired scene from the `Assets/` folder
- Interact with circuit elements as implemented in the simulation
- Refer to individual script files in `Assets/` for details on specific component behaviour

---

## License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

## Authors

**Kiisteric** — [GitHub Profile](https://github.com/Kiisteric)
**DerYokoya** — [GitHub Profile](https://github.com/DerYokoya)
**1826** — [GitHub Profile](https://github.com/theArabeMonkey1826)
**Taha863** — [GitHub Profile](https://github.com/taha863)
