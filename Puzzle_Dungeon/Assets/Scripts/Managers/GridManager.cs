using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap actionpickupTilemap;

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

    public bool IsWalkable(Vector3Int cell, PlayerMovement player)
    {
        // Must have floor
        if (!floorTilemap.HasTile(cell))
            return false;

        // Cannot have a wall
        if (wallTilemap.HasTile(cell))
            return false;

        // Check normal doors
        Door[] doors = Object.FindObjectsByType<Door>(FindObjectsSortMode.None);

        foreach (Door door in doors)
        {
            Vector3Int doorCell = WorldToCell(door.transform.position);

            if (doorCell == cell && !door.IsOpen)
                return false;
        }

        // Check locked doors
        LockedDoor[] lockedDoors = Object.FindObjectsByType<LockedDoor>(FindObjectsSortMode.None);

        foreach (LockedDoor lockedDoor in lockedDoors)
        {
            Vector3Int doorCell = WorldToCell(lockedDoor.transform.position);

            if (doorCell != cell)
                continue;

            if (!lockedDoor.IsUnlocked)
            {
                lockedDoor.TryUnlock(player);

                // If it is still locked after trying, movement is blocked.
                if (!lockedDoor.IsUnlocked)
                    return false;
            }
        }

        return true;
    }

    public BoundsInt GetBounds()
    {
        return floorTilemap.cellBounds;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 world = Camera.main.ScreenToWorldPoint(mousePos);
            world.z = 0;

            Vector3Int cell = WorldToCell(world);

            Debug.Log($"{cell}");
        }
    }
}