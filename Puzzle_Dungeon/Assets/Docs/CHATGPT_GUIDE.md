# CHATGPT_GUIDE.md

# How Our Team Uses ChatGPT for This Project

## Purpose

This document explains how our team should use ChatGPT while developing this game.

Because the team shares one ChatGPT account, this guide ensures that everyone:

* Provides enough context before asking questions
* Follows the existing project architecture
* Avoids creating conflicting systems
* Gets consistent coding suggestions
* Keeps the game organized as it grows

ChatGPT should be treated as a development assistant, not as a replacement for understanding the code. Always review, test, and adapt suggestions before adding them to the project.

---

# Before Asking ChatGPT for Help

Always include:

## 1. What You Are Trying To Do

Explain the goal clearly.

Good example:

> "I want the player to disappear after reaching the exit, then allow control of the next player."

Bad example:

> "Fix my exit."

---

## 2. Relevant Context

Explain how the current system works.

Include:

* What scripts are involved
* What objects are involved
* What has already been implemented
* What behavior is expected

Example:

> "We have a PartyManager that spawns three players. Each player has PlayerMovement. Level completion requires all players to reach the exit tile."

---

## 3. Provide Errors Exactly

Always copy the full Unity error message.

Include:

* Error type
* Script name
* Line number
* Stack trace if available

Example:

```
NullReferenceException: Object reference not set to an instance of an object
Button.OnTriggerEnter2D() (at Assets/Scripts/Button.cs:24)
```

Do not summarize errors unless necessary.

---

## 4. Include Existing Code

Before asking ChatGPT to create or modify a script:

Provide:

* The current script
* Related scripts
* Any important inspector settings

Do not ask ChatGPT to rewrite an entire system without showing the current implementation first.

---

# Project Architecture Rules

ChatGPT suggestions should follow these existing systems.

## Player System

Players are controlled through:

* `PlayerMovement`
* `PartyManager`
* `PlayerSpawn`

Do not create a separate player controller unless the team agrees.

Player colors:

* Blue
* Purple
* Orange

---

## Grid System

The game uses Unity's Grid and Tilemap system.

Important scripts:

* `GridManager`
* Tilemaps for floor and walls
* Grid-based movement

Movement should remain:

* Tile based
* Smooth
* Compatible with existing grid calculations

Avoid replacing the grid system with:

* Free movement
* Rigidbody physics movement
* New pathfinding systems

unless discussed by the team.

---

## Interaction System

Interactable objects inherit from:

`Interactable`

Examples:

* Doors
* Buttons
* Keys
* Exit

New interactive objects should follow this pattern.

Do not create random trigger systems unless necessary.

---

## Level System

Levels are handled through:

* `LevelManager`

Current design:

* Players must complete objectives
* Exit conditions are checked
* Level transitions happen through LevelManager

---

# When Asking For New Features

Use this format:

```
Feature:
[What we want to add]

Current Systems:
[What scripts already exist]

Expected Behavior:
[Step-by-step explanation]

Restrictions:
[Things that should not change]

Files That May Need Changes:
[Scripts involved]
```

Example:

```
Feature:
Add keys that unlock colored doors.

Current Systems:
We already have LockedDoor.cs and Interactable.cs.

Expected Behavior:
Player touches key, key disappears, player can unlock matching door.

Restrictions:
Do not change PlayerMovement.

Files That May Need Changes:
Key.cs
LockedDoor.cs
Inventory system
```

---

# When Asking For Code

Always ask ChatGPT to:

1. Explain the change first
2. Identify which files are affected
3. Provide complete scripts only when needed
4. Explain where the script goes
5. Explain Unity inspector setup

Avoid requests like:

> "Make the whole game better."

Instead ask for specific changes.

---

# Code Review Checklist

Before adding ChatGPT code:

Check:

## Does it match our architecture?

Does it use:

* Existing managers?
* Existing systems?
* Existing naming conventions?

---

## Does it create duplicate functionality?

Examples:

Bad:

* Creating another GridManager
* Creating another PlayerController
* Creating another LevelManager

---

## Does it introduce unnecessary complexity?

Prefer:

* Simple scripts
* Reusable components
* Clear responsibilities

Avoid:

* Large scripts that control everything
* Systems that nobody understands

---

# Debugging Process

When something breaks:

Follow this order:

## 1. Read the Unity Console

Find:

* First error
* Script name
* Line number

The first error is usually the cause.

---

## 2. Check Recent Changes

Ask:

* What changed before it broke?
* Was something merged?
* Was a prefab modified?
* Was an inspector reference removed?

---

## 3. Ask ChatGPT With Context

Provide:

* Error
* Script
* Recent changes
* Expected behavior

---

# Git Workflow With ChatGPT

Before making major changes:

Explain:

> "I am modifying [feature]. Which files should I change and how can I avoid merge conflicts?"

Avoid having multiple people edit the same major scripts at the same time.

Especially coordinate changes to:

* PlayerMovement
* GridManager
* PartyManager
* LevelManager

---

# Important Rule: Do Not Blindly Copy Code

ChatGPT can make mistakes.

Before using generated code:

* Read it
* Understand it
* Test it
* Ask questions if something is unclear

The goal is to improve the project, not just make errors disappear.

---

# Useful ChatGPT Prompts

## Debugging

```
I have this Unity error:

[paste error]

Here is the script:

[paste script]

Explain the cause and how to fix it without changing our architecture.
```

---

## Adding Features

```
We want to add:

[feature]

Our current architecture is:

[description]

What scripts should change and why?
```

---

## Code Review

```
Review this script for our Unity project.

Check:
- Architecture
- Bugs
- Performance
- Maintainability

Do not rewrite unless necessary.
```

---

## Learning

```
Explain this script line by line.

Assume I understand basic Unity but not this system.
```

---

# Final Reminder

The goal is not to make the biggest or most complicated game.

The goal is to make a polished, understandable, and playable game that the whole team can maintain.

When using ChatGPT:

* Give context
* Protect the architecture
* Understand the code
* Communicate with teammates
* Test everything
