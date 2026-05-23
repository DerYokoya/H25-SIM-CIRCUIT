**English** | [Français](README.fr.md) 

# H25-SIM-CIRCUIT

A Unity-based circuit simulation project built with C#, ShaderLab, and HLSL.

---

## Images

<div align="center">
  <table>
    <tr>
      <td align="center">
        <img width="250" alt="components" src="https://github.com/user-attachments/assets/8f2b82c8-2c6d-42c1-80a9-560455fcfec8" /><br />
        <sub><b>All Components</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="circuit" src="https://github.com/user-attachments/assets/1b222124-cbfb-48a6-a5f7-91239883c285" /><br />
        <sub><b>Basic Circuit</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="short_circuit" src="https://github.com/user-attachments/assets/e3c5b8af-a1f2-4b89-b677-31a28f51cebe" /><br />
        <sub><b>Short Circuit</b></sub>
      </td>
    </tr>
    <tr>
      <td align="center">
        <img width="250" alt="resistor" src="https://github.com/user-attachments/assets/b79a465f-2cd0-4012-a5ca-5f844a828728" /><br />
        <sub><b>Resistor Color Bands</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="light_bulb" src="https://github.com/user-attachments/assets/100c1f25-fea5-4090-b7c8-146e3ff18c16" /><br />
        <sub><b>Light Bulb</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="switch_on" src="https://github.com/user-attachments/assets/bddff83a-23eb-426b-821a-b5f0d8e4903b" /><br />
        <sub><b>Switch On (Current Flows)</b></sub>
      </td>
    </tr>
    <tr>
      <td align="center">
        <img width="250" alt="fuse" src="https://github.com/user-attachments/assets/5d1c7f9f-27d4-468b-9283-cc014542ab38" /><br />
        <sub><b>Fuse</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="fuse_broken" src="https://github.com/user-attachments/assets/804c813c-ec8a-475b-9876-642e39a22e6f" /><br />
        <sub><b>Broken Fuse</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="switch_off" src="https://github.com/user-attachments/assets/e0067497-efb6-467e-8a1c-3ba334bc6cd9" /><br />
        <sub><b>Switch Off (No Current)</b></sub>
      </td>
    </tr>
  </table>
</div>

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

## What This Demonstrates
This project demonstrates the integration of real-time simulation logic with interactive visual rendering inside Unity. Through the use of C#, ShaderLab, and HLSL, the simulation models realistic electrical circuit behavior while providing immediate visual feedback to the user.

Key concepts demonstrated include:
* Real-time circuit simulation using programmable component behavior
* Interactive electrical systems with switches, fuses, resistors, and light bulbs
* Dynamic current flow visualization through custom shader effects
* Fault detection and response, including short circuits and fuse failure
* Modular component design allowing scalable circuit construction
* Combination of gameplay programming and GPU-based rendering
* Educational simulation techniques for understanding electrical engineering concepts in a virtual environment
* The project highlights how Unity can be used beyond traditional game development to create technical simulations, learning tools, and interactive engineering experiences.

---

## Future Prospects
* Add support for advanced electrical components such as capacitors, inductors, transistors, and integrated circuits
* Introduce oscilloscopes
* Add multiplayer or collaborative lab environments for educational use
* Create guided tutorials and challenge scenarios for students and beginners
* Export and import circuit configurations using external file formats
* Optimize performance for larger and more complex circuit networks

---

## Transferable Skills

Although this project focuses on circuit simulation, the technologies and design patterns used are applicable across many software and engineering domains.

This project demonstrates experience with:

* Object-oriented programming and modular software architecture using C#
* Real-time systems development and event-driven interactions
* GPU programming and shader development with ShaderLab and HLSL
* Simulation design and state management for interactive environments
* Debugging complex systems involving interconnected logic and visual feedback
* User interaction design within technical software tools
* Collaborative development workflows using Git and version control
* Optimization techniques for rendering and runtime performance

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

**Kiisteric** — [GitHub Profile](https://github.com/Kiisteric)<br>
**DerYokoya** — [GitHub Profile](https://github.com/DerYokoya)<br>
**1826** — [GitHub Profile](https://github.com/theArabeMonkey1826)<br>
**Taha863** — [GitHub Profile](https://github.com/taha863)

[English](README.md) | [Français](README.fr.md)
