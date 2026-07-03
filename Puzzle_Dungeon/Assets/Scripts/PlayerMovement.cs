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

    private static readonly string[] PartyNames = { "Blue", "Red", "Green" };
    private static bool partyInitialized;
    private static int activeMemberIndex;

    public GridManager grid;

    public Vector2Int currentCell = new Vector2Int(0, 0);

    private SpriteRenderer spriteRenderer;
    private bool moving;
    private bool isRegistered;
    private int partyIndex = -1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RegisterMember();
    }

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

    private static void AssignPartyDefaults()
    {
        for (int i = 0; i < PartyMembers.Count && i < 3; i++)
        {
            PlayerMovement member = PartyMembers[i];
            member.partyIndex = i;
            member.currentCell = GetDefaultStartCell(i, member.currentCell);
            member.ApplyCurrentState();
        }
    }

    private static Vector2Int GetDefaultStartCell(int index, Vector2Int fallbackCell)
    {
        switch (index)
        {
            case 0:
                return fallbackCell;
            case 1:
                return fallbackCell + Vector2Int.right;
            case 2:
                return fallbackCell + Vector2Int.up;
            default:
                return fallbackCell;
        }
    }

    private void ApplyCurrentState()
    {
        if (grid != null)
            transform.position = grid.CellToWorld(currentCell);

        if (spriteRenderer != null && partyIndex >= 0 && partyIndex < PartyColors.Length)
            spriteRenderer.color = PartyColors[partyIndex];
    }

    private bool IsActiveMember()
    {
        return PartyMembers.Count > 0 && activeMemberIndex >= 0 && activeMemberIndex < PartyMembers.Count && PartyMembers[activeMemberIndex] == this;
    }

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

    private static void SetActiveMember(int index)
    {
        if (PartyMembers.Count == 0)
            return;

        activeMemberIndex = Mathf.Clamp(index, 0, PartyMembers.Count - 1);
        RefreshAllPartyVisuals();
    }

    private static void RefreshAllPartyVisuals()
    {
        for (int i = 0; i < PartyMembers.Count; i++)
        {
            PartyMembers[i].ApplyCurrentState();
        }
    }

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

    private void TryMove(Vector2Int direction)
    {
        if (grid == null)
            return;

        Vector2Int target = currentCell + direction;

        if (!grid.IsInsideGrid(target))
            return;

        if (ActionManager.Instance == null || !ActionManager.Instance.SpendActions(1))
            return;

        moving = true;
        currentCell = target;
        transform.position = grid.CellToWorld(currentCell);
        moving = false;
    }
}