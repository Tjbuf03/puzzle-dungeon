using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Vector3 CellToWorld(Vector3Int cell)
    {
        return grid.GetCellCenterWorld(cell);
    }

    public Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return grid.WorldToCell(worldPosition);
    }

    public bool IsWalkable(Vector3Int cell)
    {
        // Must have floor
        if (!floorTilemap.HasTile(cell))
            return false;

        // Cannot have a wall
        if (wallTilemap.HasTile(cell))
            return false;

        return true;
    }

    public BoundsInt GetBounds()
    {
        return floorTilemap.cellBounds;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            world.z = 0;

            Vector3Int cell = WorldToCell(world);

            Debug.Log(cell + " Walkable: " + IsWalkable(cell));
        }
    }
}