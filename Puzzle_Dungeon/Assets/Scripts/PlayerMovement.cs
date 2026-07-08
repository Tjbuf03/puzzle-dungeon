using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly List<PlayerMovement> PartyMembers = new List<PlayerMovement>();
    private static readonly Color[] PartyColors =
    {
        new Color(0.20f, 0.45f, 1.00f, 1f),
        new Color(0.95f, 0.25f, 0.25f, 1f),
        new Color(0.25f, 0.80f, 0.35f, 1f)
    };

    private static bool partyInitialized;
    private static int activeMemberIndex;

    public GridManager grid;

    [SerializeField] private Vector2Int currentCell = new Vector2Int(0, 0);

    [Header("Spawn Cells")] // Serialized start cells for each player
    [SerializeField] private Vector2Int currentCellP1 = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int currentCellP2 = new Vector2Int(1, 0);
    [SerializeField] private Vector2Int currentCellP3 = new Vector2Int(0, 1);

    [Header("Player Interaction")]
    [SerializeField] private bool playerCollisionsEnabled;
    [SerializeField] private bool playerPushEnabled;
    [SerializeField] private bool allowSharedSpawnCells;

    private SpriteRenderer spriteRenderer;
    private bool moving;
    private bool isRegistered;  // Booleans default to false in C#
    private int partyIndex = -1;

    // Cache the renderer and add this player to the party list.
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RegisterMember();
    }

    // Build the full 3-player party and assign their starting cells.
    private void Start()
    {
        if (!partyInitialized)
        {
            partyInitialized = true;
            EnsurePartySize();
            AssignPartyDefaults();
            activeMemberIndex = 0;
        }

        ApplyCurrentState();
    }

    // Remove this player from the party list when it is destroyed.
    private void OnDestroy()
    {
        if (!isRegistered)
            return;

        PartyMembers.Remove(this);

        if (PartyMembers.Count == 0)
        {
            partyInitialized = false;
            activeMemberIndex = 0;
        }
    }

    // Read input only for the currently selected party member.
    private void Update()
    {
        if (!IsActiveMember())
            return;

        HandleSelectionInput();

        if (moving)
            return;

        Vector2Int direction = GetMoveDirection();
        if (direction != Vector2Int.zero)
            TryMove(direction);
    }

    // Register this instance so the party controller can track it.
    private void RegisterMember()
    {
        if (isRegistered) 
            return;

        PartyMembers.Add(this);
        isRegistered = true;
        partyIndex = PartyMembers.Count - 1;
    }

    private static void EnsurePartySize()
    {
        if (PartyMembers.Count == 0)
            return;

        PlayerMovement template = PartyMembers[0];

        while (PartyMembers.Count < 3)
        {
            Instantiate(template.gameObject);
        }
    }

    // Assign each party member its configured start cell and snap it to the nearest free tile.
    private static void AssignPartyDefaults()
    {
        for (int i = 0; i < PartyMembers.Count && i < 3; i++)
        {
            PlayerMovement member = PartyMembers[i];
            member.partyIndex = i;
            Vector2Int configuredStart = member.GetConfiguredStartCell(i);
            member.currentCell = member.ResolveSpawnCell(configuredStart, i);
            member.ApplyCurrentState();
        }
    }

    // Pick the starting cell configured for this player slot.
    private Vector2Int GetConfiguredStartCell(int index)
    {
        switch (index)
        {
            case 0:
                return currentCellP1;
            case 1:
                return currentCellP2;
            case 2:
                return currentCellP3;
            default:
                return currentCell;
        }
    }

    // Choose between shared spawns and nearest-free placement for this player slot.
    private Vector2Int ResolveSpawnCell(Vector2Int desiredCell, int memberIndex)
    {
        Vector2Int clampedCell = ClampCellToGrid(desiredCell);

        if (!playerCollisionsEnabled && allowSharedSpawnCells)
            return clampedCell;

        return FindNearestFreeStartCell(clampedCell, memberIndex);
    }

    // Snap the player to the scene and tint it by party slot.
    private void ApplyCurrentState()
    {
        if (grid != null)
            transform.position = grid.CellToWorld(currentCell);

        if (spriteRenderer != null && partyIndex >= 0 && partyIndex < PartyColors.Length)
            spriteRenderer.color = PartyColors[partyIndex];
    }

    // Check whether this instance is the currently active party member.
    private bool IsActiveMember()
    {
        return PartyMembers.Count > 0 && activeMemberIndex >= 0 && activeMemberIndex < PartyMembers.Count && PartyMembers[activeMemberIndex] == this;
    }

    // Handle 1/2/3 selection for the active player slot.
    private void HandleSelectionInput()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame)
            SetActiveMember(0);
        else if (Keyboard.current.digit2Key.wasPressedThisFrame || Keyboard.current.numpad2Key.wasPressedThisFrame)
            SetActiveMember(1);
        else if (Keyboard.current.digit3Key.wasPressedThisFrame || Keyboard.current.numpad3Key.wasPressedThisFrame)
            SetActiveMember(2);
    }

    // Switch which party member responds to movement input.
    private static void SetActiveMember(int index)
    {
        if (PartyMembers.Count == 0)
            return;

        activeMemberIndex = Mathf.Clamp(index, 0, PartyMembers.Count - 1);
        RefreshAllPartyVisuals();
    }

    // Re-apply state to every party member after the selected player changes.
    private static void RefreshAllPartyVisuals()
    {
        for (int i = 0; i < PartyMembers.Count; i++)
        {
            PartyMembers[i].ApplyCurrentState();
        }
    }

    // Read WASD or arrow-key movement for the active party member.
    private Vector2Int GetMoveDirection()
    {
        if (Keyboard.current == null)
            return Vector2Int.zero;

        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            return Vector2Int.up;

        if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
            return Vector2Int.down;

        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
            return Vector2Int.left;

        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            return Vector2Int.right;

        return Vector2Int.zero;
    }

    // Move one cell if the target stays in bounds and the team still has actions.
    private void TryMove(Vector2Int direction)
    {
        if (grid == null)
            return;

        Vector2Int target = currentCell + direction;

        if (!grid.IsInsideGrid(target))
            return;

        List<PlayerMovement> pushChain;
        if (!TryResolvePlayerCollision(target, direction, out pushChain))
            return;

        if (ActionManager.Instance == null || !ActionManager.Instance.SpendActions(1))
            return;

        Vector3 targetWorldPosition = grid.CellToWorld(target);

        Door door = GetObjectAtCell<Door>(targetWorldPosition);
        if (door != null && !door.isOpen)
        {
            Debug.Log("The door is closed.");
            return;
        }

        if (pushChain != null)
            ApplyPushChain(pushChain, direction);

        moving = true;
        MoveToCell(target);
        moving = false;

        ButtonTile button = GetObjectAtCell<ButtonTile>(targetWorldPosition);
        if (button != null)
            button.PressButton();
    }

    // Handle player blocking or pushing before movement is applied.
    private bool TryResolvePlayerCollision(Vector2Int targetCell, Vector2Int direction, out List<PlayerMovement> pushChain)
    {
        pushChain = null;

        if (!playerCollisionsEnabled)
            return true;

        PlayerMovement blockingPlayer = GetPlayerAtCell(targetCell);
        if (blockingPlayer == null)
            return true;

        if (!playerPushEnabled)
            return false;

        if (!TryBuildPushChain(targetCell, direction, out pushChain))
            return false;

        return true;
    }

    // Move this player to a new grid cell.
    private void MoveToCell(Vector2Int targetCell)
    {
        currentCell = targetCell;

        if (grid != null)
            transform.position = grid.CellToWorld(currentCell);
    }

    // Search outward from the configured start cell until a free tile is found.
    private Vector2Int FindNearestFreeStartCell(Vector2Int desiredCell, int memberIndex)
    {
        if (grid == null)
            return desiredCell;

        Vector2Int clampedStart = ClampCellToGrid(desiredCell);
        if (IsCellFree(clampedStart, memberIndex))
            return clampedStart;

        Queue<Vector2Int> searchQueue = new Queue<Vector2Int>();
        HashSet<Vector2Int> visitedCells = new HashSet<Vector2Int>();
        searchQueue.Enqueue(clampedStart);
        visitedCells.Add(clampedStart);

        Vector2Int[] searchDirections =
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

        while (searchQueue.Count > 0)
        {
            Vector2Int searchCell = searchQueue.Dequeue();

            for (int i = 0; i < searchDirections.Length; i++)
            {
                Vector2Int nextCell = searchCell + searchDirections[i];
                if (!grid.IsInsideGrid(nextCell) || visitedCells.Contains(nextCell))
                    continue;

                if (IsCellFree(nextCell, memberIndex))
                    return nextCell;

                visitedCells.Add(nextCell);
                searchQueue.Enqueue(nextCell);
            }
        }

        return clampedStart;
    }

    // Check whether a cell is already occupied by an earlier party member.
    private static bool IsCellFree(Vector2Int candidateCell, int memberIndex)
    {
        for (int i = 0; i < PartyMembers.Count; i++)
        {
            PlayerMovement otherMember = PartyMembers[i];
            if (otherMember == null || otherMember == PartyMembers[memberIndex])
                continue;

            if (otherMember.partyIndex >= memberIndex)
                continue;

            if (otherMember.currentCell == candidateCell)
                return false;
        }

        return true;
    }

    // Build a list of players that must slide forward together for a push.
    private bool TryBuildPushChain(Vector2Int occupiedCell, Vector2Int direction, out List<PlayerMovement> pushChain)
    {
        pushChain = new List<PlayerMovement>();

        Vector2Int searchCell = occupiedCell;
        while (true)
        {
            PlayerMovement occupant = GetPlayerAtCell(searchCell);
            if (occupant == null)
                return false;

            pushChain.Add(occupant);

            Vector2Int nextCell = searchCell + direction;
            if (!grid.IsInsideGrid(nextCell))
                return false;

            if (GetPlayerAtCell(nextCell) == null)
                return true;

            searchCell = nextCell;
        }
    }

    // Slide pushed players forward from farthest to nearest so they do not overlap.
    private void ApplyPushChain(List<PlayerMovement> pushChain, Vector2Int direction)
    {
        for (int i = pushChain.Count - 1; i >= 0; i--)
        {
            PlayerMovement player = pushChain[i];
            player.MoveToCell(player.currentCell + direction);
        }
    }

    // Find the player currently standing on a given cell.
    private static PlayerMovement GetPlayerAtCell(Vector2Int cell)
    {
        for (int i = 0; i < PartyMembers.Count; i++)
        {
            PlayerMovement member = PartyMembers[i];
            if (member != null && member.currentCell == cell)
                return member;
        }

        return null;
    }

    // Keep a candidate cell inside the current grid bounds.
    private Vector2Int ClampCellToGrid(Vector2Int cell)
    {
        if (grid == null)
            return cell;

        return new Vector2Int(
            Mathf.Clamp(cell.x, 0, grid.width - 1),
            Mathf.Clamp(cell.y, 0, grid.height - 1));
    }

    private T GetObjectAtCell<T>(Vector3 worldPosition) where T : Component
    {
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit == null)
            return null;

        return hit.GetComponent<T>();
    }
}