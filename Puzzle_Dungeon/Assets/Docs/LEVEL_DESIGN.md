# Level Design Guide

This document explains how to create, organize, and test levels in Puzzle Escape.

---

# Creating a New Level

Never create a scene from scratch.

Instead:

1. Duplicate `LevelTemplate`
2. Rename it
3. Build the puzzle
4. Assign the next level in the LevelManager
5. Add the scene to Build Settings
6. Test
7. Commit

---

# Scene Hierarchy

Every level should follow this hierarchy.

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
    Action Bar
    Level Complete Panel

Spawn Points
    Blue Spawn
    Purple Spawn
    Orange Spawn

Gameplay
```

Puzzle objects belong inside the Gameplay object.

---

# Tilemaps

Floor Tilemap

- Paint all walkable tiles.

Wall Tilemap

- Paint all blocking tiles.

Do not place gameplay objects directly onto tilemaps.

---

# Spawn Points

Every level must contain:

- Blue Spawn
- Purple Spawn
- Orange Spawn

These use the PlayerSpawn prefab.

---

# Gameplay Objects

Always use prefabs.

Current gameplay prefabs

- Exit
- Button
- Door
- Locked Door
- Key

Future gameplay prefabs

- Teleporter
- Push Block
- Ice Tile
- Enemy
- Laser
- Pressure Plate

Never build gameplay objects directly in the scene.

---

# LevelManager

Each level must have a LevelManager.

Set

Next Level

inside the Inspector.

Example

```
Level1

Next Level
↓

Level2
```

---

# Level Completion

A level is completed when:

- Blue reaches Exit
- Purple reaches Exit
- Orange reaches Exit

When a player reaches the exit

- Player disappears
- Next player becomes active

When every player escapes

- Level Complete UI appears
- Press Enter to continue

---

# Restarting

Press

R

to restart the current level.

Every level should verify this works before committing.

---

# Action Points

Every movement costs one Action Point.

When Action Points reach zero

The player cannot move.

Every level should be beatable within the available Action Points.

---

# Level Design Principles

A good puzzle should

✔ Teach one mechanic at a time

✔ Introduce mechanics gradually

✔ Encourage planning

✔ Reward efficiency

✔ Avoid trial-and-error

---

# Puzzle Progression

Recommended progression

Level 1

Movement

↓

Level 2

Buttons

↓

Level 3

Doors

↓

Level 4

Keys

↓

Level 5

Locked Doors

↓

Level 6+

Combine mechanics

---

# Testing Checklist

Before committing a level

□ All three players spawn correctly

□ Players cannot leave the map

□ Walls block movement

□ Buttons work

□ Doors work

□ Locked Doors work

□ Keys can be picked up

□ Exit works

□ All players must escape

□ Action Bar updates

□ R restarts level

□ Enter loads next level

□ No Console errors

---

# Performance

Avoid

- Hundreds of unnecessary GameObjects
- Duplicate prefabs
- Large empty areas

Keep puzzles compact.

---

# Naming Convention

Levels

```
Level1
Level2
Level3
```

Gameplay Objects

```
Exit

Door

LockedDoor

Button

Key
```

Spawn Points

```
Blue Spawn

Purple Spawn

Orange Spawn
```

---

# Best Practices

✔ Keep puzzles readable.

✔ Leave enough room for players to move.

✔ Use prefabs.

✔ Keep the hierarchy organized.

✔ Test before pushing.

✔ Keep mechanics consistent between levels.

---

# Common Mistakes

❌ Forgetting to assign the Next Level.

❌ Forgetting a spawn point.

❌ Creating gameplay objects instead of prefabs.

❌ Leaving Console errors.

❌ Forgetting to add the scene to Build Settings.

❌ Editing LevelTemplate directly.
