using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected GridManager Grid => GridManager.Instance;

    protected Vector3Int CurrentCell;

    protected virtual void Start()
    {
        SnapToGrid();
    }

    protected void SnapToGrid()
    {
        if (Grid == null)
            return;

        CurrentCell = Grid.WorldToCell(transform.position);
        transform.position = Grid.CellToWorld(CurrentCell);
    }
}