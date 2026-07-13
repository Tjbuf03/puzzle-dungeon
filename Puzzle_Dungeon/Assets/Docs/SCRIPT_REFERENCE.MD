---

# System Ownership

The project is divided into systems. Before modifying a script, identify which system your feature belongs to.

| System | Purpose | Scripts |
|---------|---------|---------|
| **Core Gameplay** | Grid logic and shared functionality | GridManager, Interactable |
| **Player System** | Player movement, spawning, switching | PlayerMovement, PartyManager, PlayerSpawn |
| **Interaction System** | Objects interacting with each other | InteractionManager, Button, Door, LockedDoor, Key, Exit |
| **Level System** | Level progression and scene management | LevelManager |
| **Action System** | Action Points and Action Bar | ActionManager |
| **UI System** | User Interface | Action Bar, Level Complete Panel, future menus |

---

# Before You Modify a Script

Ask yourself these questions:

### 1. Which system does my feature belong to?

Examples

Teleporters → Interaction System

Dash Ability → Player System

Ice Tiles → Core Gameplay

Pause Menu → UI System

Save System → Level System

---

### 2. Does a script already exist for this?

If yes, extend it.

If no, create a new script.

---

### 3. Can I inherit from an existing class?

Example

```
Interactable
    ↑
Teleporter
```

instead of creating a completely unrelated object.

---

### 4. Will changing this script affect unrelated mechanics?

If yes...

Stop.

There is probably a better place for the code.

---

# Common Examples

## I want to add teleporters

✔ Create Teleporter.cs

✔ Inherit from Interactable

✔ Use GridManager to determine destination

✖ Do NOT modify PlayerMovement unless absolutely necessary

---

## I want to add moving enemies

✔ Create Enemy.cs

✔ Use GridManager for movement

✔ Detect players with triggers

✖ Do NOT modify ActionManager

---

## I want to add Ice Tiles

✔ Create IceTile.cs

✔ Extend GridManager movement rules

✖ Do NOT put sliding code inside Door.cs

---

## I want to add Pushable Blocks

✔ Create PushableBlock.cs

✔ Inherit from Interactable

✔ Use GridManager

✖ Do NOT modify PartyManager

---

## I want to add a new UI element

✔ Create a UI script

✔ Update the Canvas

✖ Do NOT add UI code to PlayerMovement

---

# Golden Rule

When adding a new mechanic:

1. Create a new script first.
2. Only modify existing scripts if the mechanic truly depends on them.
3. Keep each script focused on one responsibility.
4. If you're adding hundreds of lines to an existing script, consider creating another class instead.

---

# Architecture Checklist

Before committing code, ask yourself:

□ Does this mechanic belong in the script I edited?

□ Could this have been its own script?

□ Am I breaking another system?

□ Does another teammate's mechanic depend on this?

□ Is this reusable for future levels?

□ Should I update the documentation?

If every answer looks good, the feature is probably in the right place.

# Script Reference

This document describes every script in the project, its purpose, responsibilities, dependencies, and when it should (or should not) be modified.

---

# Managers

---

## GridManager.cs

Location

Scripts/Managers

Purpose

- Converts between world and grid coordinates.
- Determines if a tile is walkable.
- Provides helper methods for interacting with the grid.
- Prevents movement through walls and closed doors.

Responsibilities

- CellToWorld()
- WorldToCell()
- IsWalkable()
- Grid coordinate conversions

Used By

- PlayerMovement
- Interactable
- Door
- LockedDoor
- Future mechanics

Modify When

- Adding new terrain types
- Adding movement restrictions
- Adding new tile interactions

Do NOT Modify When

- Adding UI
- Loading levels
- Reading keyboard input
- Spending Action Points

---

## PartyManager.cs

Location

Scripts/Managers

Purpose

Controls the player's party.

Responsibilities

- Spawn players
- Track all party members
- Switch active player
- Select next available player

Used By

- PlayerMovement
- LevelManager

Modify When

- Adding new party members
- Changing switching behavior

Do NOT Modify When

- Creating doors
- Buttons
- Keys
- UI

---

## ActionManager.cs

Location

Scripts/Managers

Purpose

Tracks remaining Action Points.

Responsibilities

- Store Action Points
- Update Action Bar UI
- Determine whether an action can be performed

Used By

- PlayerMovement

Modify When

- Adding scoring
- Adding bonus actions
- Changing action costs

Do NOT Modify When

- Moving players
- Loading scenes

---

## InteractionManager.cs

Location

Scripts/Managers

Purpose

Coordinates interactions between game objects.

Responsibilities

- Register interactions
- Connect buttons to doors
- Future interaction mechanics

Used By

- Button
- Door

Future

- Pressure Plates
- Teleporters
- Lasers

Modify When

Any mechanic interacts with another object.

---

## LevelManager.cs

Location

Scripts/Managers

Purpose

Controls level flow.

Responsibilities

- Restart level
- Load next level
- Track escaped players
- Show Level Complete UI

Used By

- Exit
- PartyManager

Modify When

- Checkpoints
- Save system
- Menus
- Scene transitions

Do NOT Modify When

- Player movement
- Buttons
- Keys

---

# Player

---

## PlayerMovement.cs

Location

Scripts/Player

Purpose

Controls player movement.

Responsibilities

- Read input
- Move between cells
- Smooth movement
- Carry key
- Use key

Uses

- GridManager
- ActionManager
- PartyManager

Modify When

- Dash
- Sprint
- New movement abilities
- Carrying additional items

Do NOT Modify When

- Buttons
- Doors
- UI
- Scene loading

---

## PlayerSpawn.cs

Purpose

Determines where each party member starts.

Responsibilities

- Spawn position
- Assigned PartyMember

Modify When

- Adding new party members

---

# Interactables

---

## Interactable.cs

Purpose

Base class for every interactable object.

Responsibilities

- Snap object to grid
- Store current grid position

Inherited By

- Exit
- Door
- LockedDoor
- Button
- Key

Every new interactable should inherit from this class whenever possible.

---

## Exit.cs

Purpose

Detects when a player reaches the exit.

Responsibilities

- Notify LevelManager

Modify When

- Changing exit behavior
- Adding exit animations

---

## Button.cs

Purpose

Activates connected doors.

Responsibilities

- Detect player entering trigger
- Notify InteractionManager

Modify When

- Adding timed buttons
- Pressure plates

---

## Door.cs

Purpose

Blocks movement until opened.

Responsibilities

- Open
- Close
- Report IsOpen state

Used By

- Button
- GridManager

---

## LockedDoor.cs

Purpose

Consumes a key before opening.

Responsibilities

- Check if player has key
- Unlock
- Remove keyhole visual

Uses

- PlayerMovement

Modify When

- Colored doors
- Multiple keys

---

## Key.cs

Purpose

Allows players to unlock LockedDoors.

Responsibilities

- Detect pickup
- Attach to player

Modify When

- Colored keys
- Stackable keys

---

# UI

---

## Action Bar

Purpose

Displays remaining Action Points.

Updated By

- ActionManager

---

## Level Complete Panel

Purpose

Displayed once every player reaches the exit.

Updated By

- LevelManager

---

# Scene Objects

---

Grid

Owns

- Floor Tilemap
- Wall Tilemap

---

Managers

Contains

- GridManager
- PartyManager
- ActionManager
- InteractionManager
- LevelManager

---

Spawn Points

Contains

- Blue Spawn
- Purple Spawn
- Orange Spawn

---

Gameplay

Contains all puzzle objects.

Examples

- Exit
- Button
- Door
- LockedDoor
- Key

Future

- Teleporters
- Ice Tiles
- Lasers
- Enemies
- Boxes

---

# Adding a New Mechanic

Before creating a script ask:

Does one already exist that should handle this?

Example

Teleporters

Create

Teleporter.cs

Uses

GridManager

Does NOT require changes to

ActionManager

---

Pressure Plate

Create

PressurePlate.cs

Uses

InteractionManager

Does NOT modify

PlayerMovement

---

Enemy

Create

Enemy.cs

Uses

GridManager

May notify

LevelManager

---

# General Rule

If you're unsure where code belongs:

- Movement → PlayerMovement
- Grid logic → GridManager
- Interactions → InteractionManager
- Party logic → PartyManager
- Action Points → ActionManager
- Level flow → LevelManager