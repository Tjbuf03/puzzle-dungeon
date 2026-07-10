using UnityEngine;

public class LockedDoor : Interactable
{

    [SerializeField] private GameObject keyhole;

    private bool unlocked;

    public bool IsUnlocked => unlocked;

    public void TryUnlock(PlayerMovement player)
    {
        if (unlocked)
            return;

        if (!player.UseKey())
            return;

        unlocked = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        if (keyhole != null)
            keyhole.SetActive(false);
    }
}