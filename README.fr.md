[English](README.md) | **Français**

# H25-SIM-CIRCUIT

Un projet de simulation de circuits basé sur Unity, développé en C#, ShaderLab et HLSL.

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

**H25-SIM-CIRCUIT** est un projet de simulation Unity axé sur le comportement interactif des circuits. Le projet utilise des shaders personnalisés (ShaderLab/HLSL) combinés à de la logique de jeu en C# pour créer une expérience de simulation de circuits visuellement riche et fonctionnelle. Il permet de construire, tester et analyser des circuits électriques dans un environnement virtuel, reproduisant le comportement de composants réels — c'est-à-dire que les composants réagissent aux changements de tension ou de résistance comme ils le feraient dans un laboratoire physique.

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
- Consulter les scripts individuels dans `Assets/` pour les détails sur le comportement de chaque composant

---

## Licence

Ce projet est sous licence **MIT** — voir le fichier [LICENSE](LICENSE) pour plus de détails.

---

## Auteurs

**Kiisteric** — [Profil GitHub](https://github.com/Kiisteric)<br>
**DerYokoya** — [Profil GitHub](https://github.com/DerYokoya)<br>
**1826** — [Profil GitHub](https://github.com/theArabeMonkey1826)<br>
**Taha863** — [Profil GitHub](https://github.com/taha863)
