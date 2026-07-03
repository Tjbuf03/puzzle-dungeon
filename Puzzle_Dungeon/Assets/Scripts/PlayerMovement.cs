using Unity.VisualScripting;
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

    //trying my way of detecting stuff
    
    public LayerMask hitLayers;

    void TryMove(Vector2Int direction)
    {

        Vector2Int target = currentCell + direction;

        Vector2 targetPosition = grid.CellToWorld(currentCell + direction);

        Collider2D hit = Physics2D.OverlapBox(targetPosition, Vector2.one * 0.9f, 0f, hitLayers);

        if (hit != null)
        {
            TileProperties tile = hit.GetComponent<TileProperties>();

            if (tile != null && !tile.Walkable)
            {
                Debug.Log("Wall!");
                return;
            }
        }
        //// Stay inside the grid
        if (!grid.IsInsideGrid(target))
            return;

        // Spend one action to move
        if (!ActionManager.Instance.SpendActions(1))
            return;

        currentCell = target;
        transform.position = grid.CellToWorld(currentCell);
    }
}