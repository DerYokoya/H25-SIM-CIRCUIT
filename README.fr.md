[English](README.md) | **Français**

# H25-SIM-CIRCUIT

Un projet de simulation de circuits basé sur Unity, développé en C#, ShaderLab et HLSL.

---

## Images

<div align="center">
  <table>
    <tr>
      <td align="center">
        <img width="250" alt="Composantes" src="https://github.com/user-attachments/assets/8f2b82c8-2c6d-42c1-80a9-560455fcfec8" /><br />
        <sub><b>Toutes les composantes</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="Circuit basique" src="https://github.com/user-attachments/assets/1b222124-cbfb-48a6-a5f7-91239883c285" /><br />
        <sub><b>Circuit basique</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="Court-circuit" src="https://github.com/user-attachments/assets/e3c5b8af-a1f2-4b89-b677-31a28f51cebe" /><br />
        <sub><b>Court-circuit</b></sub>
      </td>
    </tr>
    <tr>
      <td align="center">
        <img width="250" alt="Résistance avec bandes de couleurs" src="https://github.com/user-attachments/assets/b79a465f-2cd0-4012-a5ca-5f844a828728" /><br />
        <sub><b>Résistance avec bandes de couleurs</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="Ampoule" src="https://github.com/user-attachments/assets/100c1f25-fea5-4090-b7c8-146e3ff18c16" /><br />
        <sub><b>Ampoule</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="Interrupteur (Le courant passe)" src="https://github.com/user-attachments/assets/bddff83a-23eb-426b-821a-b5f0d8e4903b" /><br />
        <sub><b>Interrupteur (Le courant passe)</b></sub>
      </td>
    </tr>
    <tr>
      <td align="center">
        <img width="250" alt="Fusible" src="https://github.com/user-attachments/assets/5d1c7f9f-27d4-468b-9283-cc014542ab38" /><br />
        <sub><b>Fusible</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="Fusible brisé" src="https://github.com/user-attachments/assets/804c813c-ec8a-475b-9876-642e39a22e6f" /><br />
        <sub><b>Fusible brisé</b></sub>
      </td>
      <td align="center">
        <img width="250" alt="Le courant ne passe pas" src="https://github.com/user-attachments/assets/e0067497-efb6-467e-8a1c-3ba334bc6cd9" /><br />
        <sub><b>Interrupteur (Le courant passe)</b></sub>
      </td>
    </tr>
  </table>
</div>

---

## 📋 Table des matières

- [À propos](#à-propos)
- [Technologies utilisées](#technologies-utilisées)
- [Démarrage](#démarrage)
  - [Prérequis](#prérequis)
  - [Installation](#installation)
- [Structure du projet](#structure-du-projet)
- [Utilisation](#utilisation)
- [Licence](#licence)

---

## À propos

**H25-SIM-CIRCUIT** est un projet de simulation Unity axé sur le comportement interactif des circuits. Le projet utilise des shaders personnalisés (ShaderLab/HLSL) combinés à de la logique de jeu en C# pour créer une expérience de simulation de circuits visuellement riche et fonctionnelle. Il permet de construire, tester et analyser des circuits électriques dans un environnement virtuel, reproduisant le comportement de composantes réelles, c'est-à-dire que les composantes réagissent aux changements de tension ou de résistance comme ils le feraient dans un laboratoire physique.

---

## Technologies utilisées

| Technologie | Utilisation |
|---|---|
| **Unity** | Moteur de jeu / framework de simulation |
| **C#** (~75%) | Logique de simulation principale, scripts de jeu |
| **ShaderLab** (~20%) | Shaders visuels personnalisés |
| **HLSL** (~5%) | Programmation de shaders pour effets GPU |

---

## Démarrage

### Prérequis

- [Unity Hub](https://unity.com/download) installé
- Unity Editor (vérifier `ProjectSettings/ProjectVersion.txt` pour la version requise)
- Visual Studio ou tout IDE C# compatible (voir `.vsconfig` pour les extensions recommandées)

### Installation

1. **Cloner le dépôt**
   ```bash
   git clone https://github.com/DerYokoya/H25-SIM-CIRCUIT.git
   cd H25-SIM-CIRCUIT
   ```

2. **Ouvrir dans Unity Hub**
   - Lancer Unity Hub
   - Cliquer sur **Ajouter** → **Ajouter un projet depuis le disque**
   - Sélectionner le dossier `H25-SIM-CIRCUIT` cloné

3. **Ouvrir le projet**
   - Unity importera automatiquement tous les assets et packages
   - Attendre la fin de la compilation avant de lancer

4. **Lancer la simulation**
   - Ouvrir la scène principale depuis le dossier `Assets/`
   - Appuyer sur **Jouer** dans l'éditeur Unity

---

## Structure du projet

```
H25-SIM-CIRCUIT/
├── Assets/             # Assets, scripts, scènes et shaders
├── Packages/           # Dépendances Unity Package Manager
├── ProjectSettings/    # Fichiers de configuration du projet Unity
├── .gitattributes      # Paramètres Git de fin de ligne et de diff
├── .gitignore          # Fichiers exclus du contrôle de version
├── .vsconfig           # Extensions recommandées pour Visual Studio
└── LICENSE             # Licence MIT
```

---

## Utilisation

Une fois le projet lancé dans l'éditeur Unity :

- Charger la scène souhaitée depuis le dossier `Assets/`
- Interagir avec les éléments du circuit tels qu'implémentés dans la simulation
- Consulter les scripts individuels dans `Assets/` pour les détails sur le comportement de chaque composante

---

## Licence

Ce projet est sous licence **MIT** — voir le fichier [LICENSE](LICENSE) pour plus de détails.

---

## Auteurs

**Kiisteric** — [Profil GitHub](https://github.com/Kiisteric)<br>
**DerYokoya** — [Profil GitHub](https://github.com/DerYokoya)<br>
**1826** — [Profil GitHub](https://github.com/theArabeMonkey1826)<br>
**Taha863** — [Profil GitHub](https://github.com/taha863)
