using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public GridManager grid;

    public Vector2Int currentCell = new Vector2Int(0, 0);

    private bool moving = false;
    
    // ADDED LINE 1: A variable to hold the physics reference
    private Rigidbody2D rb;

    void Start()
    {
        transform.position = grid.CellToWorld(currentCell);
        
        // ADDED LINE 2: Link the variable to the component on startup
        rb = GetComponent<Rigidbody2D>();
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

        // Spend one action to move
        if (!ActionManager.Instance.SpendActions(1))
            return;

        currentCell = target;
        Vector2 targetPosition = grid.CellToWorld(currentCell);
        rb.MovePosition(targetPosition);
    }
}