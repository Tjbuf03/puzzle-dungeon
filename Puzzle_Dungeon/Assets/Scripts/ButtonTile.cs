using UnityEngine;

public class ButtonTile : MonoBehaviour
{
    [Header("Grid Position")]
    public GridManager grid;
    public Vector2Int cell;

    [Header("Linked Door")]
    public Door linkedDoor;

    [Header("Button State")]
    public bool hasBeenPressed = false;

    [Header("Visuals")]
    public GameObject unpressedVisual;
    public GameObject pressedVisual;

    [Header("Rendering")]
    [SerializeField] private int sortingOrder = 10;

    private void Start()
    {
        if (grid != null)
            transform.position = grid.CellToWorld(cell);

        ApplySortingOrder();
        UpdateVisuals();
    }

    public void PressButton()
    {
        if (hasBeenPressed)
            return;

        hasBeenPressed = true;

        if (linkedDoor != null)
            linkedDoor.OpenDoor();

        UpdateVisuals();

        Debug.Log("Button pressed!");
    }

    private void UpdateVisuals()
    {
        if (unpressedVisual != null)
            unpressedVisual.SetActive(!hasBeenPressed);

        if (pressedVisual != null)
            pressedVisual.SetActive(hasBeenPressed);
    }

    private void ApplySortingOrder()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].sortingOrder = sortingOrder;
    }
}