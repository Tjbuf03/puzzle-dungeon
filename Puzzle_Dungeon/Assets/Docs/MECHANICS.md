# Game Mechanics

This document describes every gameplay mechanic currently implemented in Puzzle Escape.

Whenever a new mechanic is added, document it here.

---

# Core Gameplay Loop

1. Players spawn.
2. One player is active.
3. Move around the grid.
4. Spend Action Points.
5. Solve puzzles.
6. Guide all three players to the exit.
7. Press Enter to load the next level.

---

# Grid Movement

## Description

Players move one tile at a time on a grid.

Movement is smooth instead of instant.

Every move costs one Action Point.

## Scripts

- PlayerMovement
- GridManager
- ActionManager

---

# Party System

## Description

The player controls three characters.

- Blue
- Purple
- Orange

Only one player can be controlled at a time.

When a player reaches the exit, the next available player automatically becomes active.

## Scripts

- PartyManager
- PlayerMovement
- LevelManager

---

# Action Point System

## Description

Every movement costs one Action Point.

If no Action Points remain, movement is disabled.

## Scripts

- ActionManager
- PlayerMovement

---

# Buttons

## Description

Buttons activate connected objects.

Currently they open doors.

## Scripts

- Button
- InteractionManager
- Door

---

# Doors

## Description

Doors block movement.

Buttons can open them.

Open doors no longer block the player.

## Scripts

- Door
- GridManager

---

# Keys

## Description

Players can pick up keys.

The key is displayed above the player's sprite while being carried.

A player can only carry one key.

## Scripts

- Key
- PlayerMovement

---

# Locked Doors

## Description

Locked doors require a key.

Walking into a locked door while carrying a key unlocks it.

Unlocking removes:

- Door sprite
- Keyhole visual

The player's key is consumed.

## Scripts

- LockedDoor
- PlayerMovement
- GridManager

---

# Exit

## Description

A player reaching the exit disappears.

Control automatically switches to another player.

When every player has escaped, the level is complete.

## Scripts

- Exit
- PartyManager
- LevelManager

---

# Restart Level

## Description

Press

R

to restart the current level.

## Scripts

- LevelManager

---

# Level Completion

## Description

After all three players escape:

- Level Complete UI appears.
- Press Enter to continue.
- The next scene loads.

The next scene is assigned in the LevelManager Inspector.

## Scripts

- LevelManager

---

# Current Mechanics Summary

| Mechanic | Status |
|----------|--------|
| Grid Movement | ✅ Complete |
| Party System | ✅ Complete |
| Action Points | ✅ Complete |
| Buttons | ✅ Complete |
| Doors | ✅ Complete |
| Keys | ✅ Complete |
| Locked Doors | ✅ Complete |
| Exit | ✅ Complete |
| Restart Level | ✅ Complete |
| Scene Progression | ✅ Complete |

---

# Planned Mechanics

These mechanics are planned but not yet implemented.

## Puzzle Mechanics

- Pushable Blocks
- Ice Tiles
- Teleporters
- Pressure Plates
- Lasers
- Moving Platforms
- One-Way Doors
- Conveyor Belts
- Timed Switches

---

## Player Mechanics

- Character abilities
- Inventory expansion
- Carry multiple items

---

## Level Features

- Level Select
- Checkpoints
- Save System
- Tutorial Levels

---

## User Interface

- Pause Menu
- Settings
- Audio Controls
- Win Statistics
- Best Score Tracking

---

# Adding New Mechanics

Whenever a mechanic is added:

1. Add it to this document.
2. List every script involved.
3. Explain how it interacts with existing mechanics.
4. Update SCRIPT_REFERENCE.md if new scripts were created.
5. Update ARCHITECTURE.md if the system architecture changed.

---

# Design Principles

Every mechanic should:

- Be easy to understand.
- Build on previously introduced mechanics.
- Encourage strategic thinking.
- Reuse existing systems whenever possible.
- Be modular and reusable.

Complexity should come from combining simple mechanics, not from making individual mechanics difficult to understand.