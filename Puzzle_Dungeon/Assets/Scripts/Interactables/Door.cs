using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door : Interactable
{
    [Header("Door")]
    [SerializeField] private string doorID;


    [SerializeField] private bool startsOpen = false;

    private bool isOpen;

    private Collider2D doorCollider;
    private SpriteRenderer spriteRenderer;

    public string DoorID => doorID;

    public bool IsOpen => isOpen;

    protected override void Start()
    {
        base.Start();

        doorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetOpen(startsOpen);
    }

    public void SetOpen(bool open)
    {
        isOpen = open;

        if (doorCollider != null)
            doorCollider.enabled = !open;

        if (spriteRenderer != null)
            spriteRenderer.enabled = !open;
    }
}