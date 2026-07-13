# Project Architecture

## Philosophy

Puzzle Escape is built around a modular architecture.

Each script has one clearly defined responsibility. Systems communicate with one another instead of one script handling every part of the game.

This makes the project easier to understand, test, and extend with new mechanics.

---

# Core Gameplay Loop

Player Input

↓

PlayerMovement

↓

GridManager

↓

InteractionManager

↓

ActionManager

↓

LevelManager

↓

Next Level

---

# Project Structure

```
Assets
│
├── Prefabs
│   ├── Environment
│   ├── Interactables
│   ├── Player
│   └── UI
│
├── Scenes
│   ├── LevelTemplate
│   ├── MainMenu
│   ├── Level1
│   └── ...
│
├── Scripts
│   ├── Managers
│   ├── Player
│   ├── Interactables
│   ├── UI
│   └── Mechanics
│
└── Sprites
```

---

# Manager Responsibilities

## GridManager

Purpose

- Converts between world coordinates and grid coordinates.
- Determines if a tile is walkable.
- Checks tilemaps.
- Prevents walking through walls and closed doors.

Should Know About

- Floor Tilemap
- Wall Tilemap
- Grid

Should NOT

- Read player input.
- Move the player.
- Load scenes.
- Handle UI.

---

## PartyManager

Purpose

- Spawns players.
- Tracks all party members.
- Changes the active player.
- Selects the next player when one escapes.

Should Know About

- Player prefabs
- Spawn points
- Party members

Should NOT

- Handle movement.
- Handle UI.
- Handle interactions.

---

## ActionManager

Purpose

- Stores remaining Action Points.
- Updates the Action Bar.
- Determines whether an action can be performed.

Should NOT

- Move players.
- Open doors.
- Load scenes.

---

## InteractionManager

Purpose

Coordinates interactions between game objects.

Examples

- Buttons opening doors
- Pressure plates
- Future mechanics

Goal

Adding new interactions should usually happen here instead of inside PlayerMovement.

---

## LevelManager

Purpose

- Tracks escaped players.
- Restarts the current level.
- Loads the next level.
- Shows the level complete UI.

Should NOT

- Move players.
- Handle buttons.
- Manage keys.

---

# Player System

PlayerMovement is responsible for

- Reading movement input.
- Moving between grid cells.
- Smooth movement.
- Carrying keys.
- Using keys.

PlayerMovement should never directly

- Load scenes.
- Open doors.
- Modify the Action Bar.

---

# Interactable System

Every placeable object that players interact with should inherit from

```
Interactable
```

Current interactables

- Exit
- Button
- Door
- LockedDoor
- Key

Future interactables

- Teleporter
- Ice Tile
- Pressure Plate
- Laser
- Push Block

Using inheritance keeps all interactables consistent.

---

# Level Structure

Every level should contain

```
Main Camera

Grid
    Floor Tilemap
    Wall Tilemap

Managers
    GridManager
    PartyManager
    ActionManager
    InteractionManager
    LevelManager

Canvas

Spawn Points

Gameplay
```

Everything puzzle-related belongs under the Gameplay object.

---

# Level Template

Never create a scene from scratch.

Duplicate

```
LevelTemplate
```

Rename it.

Build the puzzle.

Assign the next level.

Done.

---

# Communication Between Systems

Movement

PlayerMovement

↓

GridManager

↓

Move Player

---

Buttons

Button

↓

InteractionManager

↓

Door

---

Keys

Player

↓

Key

↓

Locked Door

---

Level Completion

Exit

↓

LevelManager

↓

Hide Player

↓

PartyManager selects next player

↓

All Players Escaped?

↓

Next Level

---

# Adding New Mechanics

When adding a mechanic, ask:

Does this mechanic already fit into an existing system?

Examples

Teleporters

- New Teleporter.cs
- Uses GridManager

Pressure Plate

- New PressurePlate.cs
- Uses InteractionManager

Enemy

- New Enemy.cs
- Uses GridManager

Try to avoid modifying unrelated scripts.

---

# Design Principles

✔ One responsibility per script

✔ Keep mechanics modular

✔ Use prefabs

✔ Duplicate the LevelTemplate

✔ Never duplicate code

✔ Favor extending existing systems instead of replacing them

✔ Document significant changes