using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Grid Position")]
    public GridManager grid;
    public Vector2Int cell;

    [Header("Door State")]
    public bool isOpen = false;

    [Header("Visuals")]
    public GameObject closedVisual;
    public GameObject openVisual;

    [Header("Rendering")]
    [SerializeField] private int sortingOrder = 10;

    private void Start()
    {
        if (grid != null)
            transform.position = grid.CellToWorld(cell);

        ApplySortingOrder();
        UpdateVisuals();
    }

    public void OpenDoor()
    {
        isOpen = true;
        UpdateVisuals();

        Debug.Log("Door opened!");
    }

    public void CloseDoor()
    {
        isOpen = false;
        UpdateVisuals();

        Debug.Log("Door closed!");
    }

    private void UpdateVisuals()
    {
        if (closedVisual != null)
            closedVisual.SetActive(!isOpen);

        if (openVisual != null)
            openVisual.SetActive(isOpen);
    }

    private void ApplySortingOrder()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].sortingOrder = sortingOrder;
    }
}