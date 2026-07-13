# Contributing Guide

Thank you for contributing to Puzzle Escape!

This document explains the workflow, coding standards, and best practices for working on the project.

Following these guidelines helps keep the codebase clean, organized, and easy for everyone to understand.

---

# Development Workflow

Before starting work:

1. Pull the latest version of the repository.
2. Make sure the project compiles.
3. Read the relevant documentation.
4. Create a new branch for your feature.

Example

```
main
    ↓
feature/teleporters
```

Never develop directly on main unless the team has agreed to do so.

---

# Before Writing Code

Ask yourself:

- Does this mechanic already exist?
- Can I reuse an existing system?
- Which script should own this feature?

If you're unsure, check:

SCRIPT_REFERENCE.md

or

ARCHITECTURE.md

---

# Coding Standards

## One Responsibility Per Script

Every script should have a single purpose.

Good

```
PlayerMovement
Door
Teleporter
```

Avoid

```
GameManager
```

that controls everything.

---

## Keep Mechanics Modular

Whenever possible

Create

```
Teleporter.cs
```

instead of adding hundreds of lines to

```
PlayerMovement.cs
```

---

## Reuse Existing Systems

Before writing code ask

Can this use:

- GridManager?
- InteractionManager?
- ActionManager?
- PartyManager?

Avoid duplicating functionality.

---

## Use Prefabs

If an object appears in more than one level

Make it a prefab.

Examples

✔ Door

✔ Exit

✔ Button

✔ Key

✔ Player Spawn

---

## Keep the Scene Organized

Puzzle objects belong inside

```
Gameplay
```

Managers belong inside

```
Managers
```

Spawn points belong inside

```
Spawn Points
```

---

# Naming Conventions

Scripts

```
PlayerMovement.cs

GridManager.cs

Teleporter.cs
```

Methods

```
TryMove()

Open()

Close()

UseKey()
```

Variables

```
currentCell

isMoving

activePlayer
```

Avoid names like

```
x

temp

thing
```

---

# Comments

Comment code that is not immediately obvious.

Good

```csharp
// Check if the player has enough Action Points before moving.
```

Avoid commenting every line.

Bad

```csharp
// Increment i
i++;
```

---

# Git Workflow

Before starting

```
Pull
```

↓

```
Create Branch
```

↓

```
Develop
```

↓

```
Test
```

↓

```
Commit
```

↓

```
Merge
```

---

# Commit Messages

Good examples

```
Add teleporter mechanic

Fix locked door collision

Implement restart system

Improve level completion UI
```

Avoid

```
stuff

update

fix

asdf
```

---

# Testing Checklist

Before committing

□ Project compiles

□ No Console errors

□ Mechanic works

□ Existing mechanics still work

□ Restart works

□ Level completion works

□ Documentation updated if needed

---

# Adding a New Mechanic

Example

Adding Teleporters

1. Read ARCHITECTURE.md

2. Read SCRIPT_REFERENCE.md

3. Decide which scripts need changes.

4. Create Teleporter.cs.

5. Test.

6. Update documentation.

7. Commit.

---

# Pull Requests

Before merging

Ask yourself

- Is my code readable?
- Did I break another mechanic?
- Can this be reused later?
- Did I remove debug code?
- Did I test everything?

---

# When to Modify Existing Scripts

Modify an existing script only if the mechanic naturally belongs there.

Example

Dash ability

Modify

```
PlayerMovement.cs
```

Teleporters

Create

```
Teleporter.cs
```

Pressure Plates

Create

```
PressurePlate.cs
```

Avoid putting unrelated code into existing scripts.

---

# Documentation

Whenever a new mechanic is added

Update

- MECHANICS.md
- SCRIPT_REFERENCE.md

If the architecture changes

Update

- ARCHITECTURE.md

If a new workflow is introduced

Update

- CONTRIBUTING.md

---

# Using ChatGPT

Before asking for code

Explain

- What mechanic you're adding.
- What scripts you've modified.
- Which documentation you've read.

Good example

"I'm adding teleporters.

Based on SCRIPT_REFERENCE.md, I think I'll need Teleporter.cs and GridManager.cs.

Is that correct?"

This helps keep the project architecture consistent.

---

# Team Goals

As contributors, we aim to:

- Write clean, readable code.
- Keep mechanics modular.
- Reuse existing systems.
- Avoid unnecessary complexity.
- Document significant changes.
- Help each other maintain a consistent architecture.

The goal is not just to make the game work, but to build a codebase that any teammate can understand and extend.