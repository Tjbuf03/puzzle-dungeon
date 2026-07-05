using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GridManager grid;

    public Vector2Int currentCell = new Vector2Int(0, 0);

    private bool moving = false;

    void Start()
    {
        transform.position = grid.CellToWorld(currentCell);
    }

    void Update()
    {
        if (moving)
            return;

        Vector2Int direction = Vector2Int.zero;

        if (Keyboard.current.wKey.wasPressedThisFrame)
            direction = Vector2Int.up;
        else if (Keyboard.current.sKey.wasPressedThisFrame)
            direction = Vector2Int.down;
        else if (Keyboard.current.aKey.wasPressedThisFrame)
            direction = Vector2Int.left;
        else if (Keyboard.current.dKey.wasPressedThisFrame)
            direction = Vector2Int.right;

        if (direction != Vector2Int.zero)
        {
            TryMove(direction);
        }
    }

    void TryMove(Vector2Int direction)
    {
        Vector2Int target = currentCell + direction;

        // Stay inside the grid
        if (!grid.IsInsideGrid(target))
            return;

        Vector3 targetWorldPosition = grid.CellToWorld(target);

        // Check if there is a closed door on the target tile
        Door door = GetObjectAtCell<Door>(targetWorldPosition);

        if (door != null && !door.isOpen)
        {
            Debug.Log("The door is closed.");
            return;
        }

        // Spend one action to move
        if (!ActionManager.Instance.SpendActions(1))
            return;

        currentCell = target;
        transform.position = targetWorldPosition;

        // Check if player stepped on a button
        ButtonTile button = GetObjectAtCell<ButtonTile>(targetWorldPosition);

        if (button != null)
        {
            button.PressButton();
        }
    }

    private T GetObjectAtCell<T>(Vector3 worldPosition) where T : Component
    {
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit == null)
            return null;

        return hit.GetComponent<T>();
    }
}