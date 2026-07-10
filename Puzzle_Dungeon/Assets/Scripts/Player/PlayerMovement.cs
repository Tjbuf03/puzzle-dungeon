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

        if (!GridManager.Instance.IsWalkable(targetCell))
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
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        currentCell = GridManager.Instance.WorldToCell(position);
    }
}