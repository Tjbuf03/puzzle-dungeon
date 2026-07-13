# Puzzle Escape Architecture

## Managers

### GridManager

Responsibilities

- Converts world coordinates to grid coordinates.
- Determines if a tile is walkable.
- Handles tilemap queries.

Should NOT

- Read player input.
- Load scenes.
- Handle UI.