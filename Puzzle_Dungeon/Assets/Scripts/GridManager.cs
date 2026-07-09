using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Size")]
    public int width = 5;
    public int height = 5;

    [Header("Tile")]
    public GameObject tilePrefab;
    public float tileSize = 1f;


    public Vector2Int GridSize => new Vector2Int(width, height);

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2Int cell = new Vector2Int(x, y);
                    Instantiate(
                        tilePrefab,
                        CellToWorld(cell),
                        Quaternion.identity,
                        transform);
            }
        }
    }

    public bool IsInsideGrid(Vector2Int cell)
    {
        return cell.x >= 0 &&
               cell.x < width &&
               cell.y >= 0 &&
               cell.y < height;
    }

    public Vector3 CellToWorld(Vector2Int cell)
    {
        float xOffset = (width - 1) * tileSize / 2f;
        float yOffset = (height - 1) * tileSize / 2f;

        return new Vector3(
            cell.x * tileSize - xOffset,
            cell.y * tileSize - yOffset,
            0);
    }
}