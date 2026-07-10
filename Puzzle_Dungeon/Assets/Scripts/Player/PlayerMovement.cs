using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveDuration = 0.12f;

    private Vector3Int currentCell;
    private bool isMoving;

    private bool canControl = false;

    private SpriteRenderer spriteRenderer;
    private PartyMember partyMember;

    private Key carriedKey;

    public bool HasKey => carriedKey != null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentCell = GridManager.Instance.WorldToCell(transform.position);
        transform.position = GridManager.Instance.CellToWorld(currentCell);
    }

    private void Update()
    {
        if (!canControl || isMoving)
            return;

        Vector3Int direction = GetInputDirection();

        if (direction != Vector3Int.zero)
            TryMove(direction);
    }

    private Vector3Int GetInputDirection()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return Vector3Int.zero;

        if (keyboard.wKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame)
            return Vector3Int.up;

        if (keyboard.sKey.wasPressedThisFrame || keyboard.downArrowKey.wasPressedThisFrame)
            return Vector3Int.down;

        if (keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame)
            return Vector3Int.left;

        if (keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)
            return Vector3Int.right;

        return Vector3Int.zero;
    }

    private void TryMove(Vector3Int direction)
    {
        Vector3Int targetCell = currentCell + direction;

        if (!GridManager.Instance.IsWalkable(targetCell, this))
            return;

        if (!ActionManager.Instance.SpendActions(1))
            return;

        currentCell = targetCell;

        StartCoroutine(SmoothMove(GridManager.Instance.CellToWorld(currentCell)));
    }

    private IEnumerator SmoothMove(Vector3 targetPosition)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                elapsed / moveDuration);

            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false;
    }

    public void SetControl(bool enabled)
    {
        canControl = enabled;

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = enabled ? 1f : 0.5f;
            spriteRenderer.color = color;
        }
    }

    public void Initialize(PartyMember member)
    {
        partyMember = member;

        switch (partyMember)
        {
            case PartyMember.Blue:
                spriteRenderer.color = new Color(0.2f, 0.45f, 1f);
                break;

            case PartyMember.Purple:
                spriteRenderer.color = new Color(0.65f, 0.3f, 0.9f);
                break;

            case PartyMember.Orange:
                spriteRenderer.color = new Color(1f, 0.55f, 0.1f);
                break;
        }
    }

    public bool PickUpKey(Key key)
    {
        if (carriedKey != null)
            return false;

        carriedKey = key;

        key.AttachToPlayer(this);

        return true;
    }

    public bool UseKey()
    {
        if (carriedKey == null)
            return false;

        Destroy(carriedKey.gameObject);
        carriedKey = null;

        return true;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        currentCell = GridManager.Instance.WorldToCell(position);
    }
}