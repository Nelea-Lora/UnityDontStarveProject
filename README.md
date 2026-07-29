# Unity Don't Starve Project

A 2D survival game developed in Unity and inspired by the gameplay mechanics of **Don't Starve**.

The player explores a procedurally generated world, gathers resources, crafts useful items, manages survival indicators and builds objects required to survive dangerous nights.

> This is an unofficial educational project created for learning and demonstrating Unity game development techniques. It is not affiliated with or endorsed by Klei Entertainment.

## Features

* Procedurally generated terrain
* Multiple biomes
* Random generation of plants, animals and resources
* Day and night cycle
* Health, hunger and sanity systems
* Inventory management
* Drag-and-drop item interactions
* Item durability and wear
* Resource gathering
* Crafting system
* Crafting recipes
* Object placement in the game world
* Campfire construction
* Campfire fuel and lifecycle mechanics
* Food preparation and cooking
* Night damage and light-based survival mechanics
* Player equipment and items held in hand

## Technologies

* Unity 2022.3.7f1
* C#
* Unity UI
* TextMeshPro
* Unity 2D Physics
* Unity Tilemap
* Unity Animation System
* Unity Particle System

## Requirements

Before opening the project, install the following software

* Unity Hub
* Unity Editor 2022.3.7f1
* An IDE with C# support such as Visual Studio, JetBrains Rider or Visual Studio Code
* Git

Using the same Unity version is recommended to avoid compatibility problems.

## Installation

Clone the repository

```bash
git clone https://github.com/Nelea-Lora/UnityDontStarveProject.git
```

Open Unity Hub and select **Add project from disk**.

Choose the cloned project directory.

Make sure Unity Editor version **2022.3.7f1** is installed and selected.

Wait until Unity imports all assets and restores the required packages.

Open the main game scene and press the **Play** button.

## Gameplay

The main objective is to survive for as long as possible.

Explore the generated world, collect available resources and use them to craft tools, equipment and structures. Keep track of the character's health, hunger and sanity while preparing for nighttime.

During the night, the player must stay close to a light source. A campfire can provide protection, but it requires fuel and eventually burns out.

Food and other resources can be placed into the inventory, used in crafting recipes or cooked using the campfire.

## Main Systems

### World Generation

The world is generated using terrain weights and biome configuration. Plants, animals and other environmental objects are distributed across the generated areas.

### Survival Indicators

The player must manage three primary indicators

* Health
* Hunger
* Sanity

Hunger can affect health, while darkness and other environmental conditions can reduce sanity or damage the player.

### Inventory

The inventory allows the player to collect, store, move and use items. Items support drag-and-drop interactions and can have limited durability.

### Crafting

Collected resources can be combined through predefined recipes. Crafted objects may be added to the inventory or placed directly into the game world.

### Campfire

The campfire provides light and protection during the night. It has its own lifecycle and fuel level. The player can add suitable items to extend its burning time and use it for cooking.

### Day and Night Cycle

The game world changes between daytime and nighttime. Darkness introduces additional dangers and requires the player to use torches, campfires or other light sources.


