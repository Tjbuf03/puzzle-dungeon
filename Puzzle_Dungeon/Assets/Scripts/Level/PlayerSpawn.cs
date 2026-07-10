using UnityEngine;

public class PlayerSpawn : Interactable
{
    [Header("Spawn Settings")]
    [SerializeField] private PartyMember player;

    public PartyMember Player => player;

    protected override void Start()
    {
        base.Start();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }
}