# Nordeus RPG Game

This project is a turn-based RPG game developed in C# as part of the Nordeus Full Stack Challenge.

## Overview

The game features a turn-based combat system where the player fights a sequence of enemies. Each turn, the player selects an ability and the enemy responds with its own move based on simple decision logic.

## Features

- Turn-based combat system
- Physical and magic attacks with stat-based scaling
- Healing and temporary buffs (attack and defense)
- Critical hit system
- Enemy AI with basic decision-making
- Experience gain and level progression
- Ability to learn and replace moves after battles

## Technical Details

The project is implemented as a console application in C#. All game logic is handled on the client side, including combat resolution, status effects, and progression.

In a full implementation, game configuration and enemy behavior would be handled by a backend service, allowing dynamic balancing and easier updates without modifying the client.

The architecture is intentionally designed to separate game logic from configuration, simulating a real-world full-stack setup.

## How to Run

1. Open the solution file in Visual Studio
2. Build the project
3. Run the application

## Demo

The demo video is included in this repository (video1.mp4).
