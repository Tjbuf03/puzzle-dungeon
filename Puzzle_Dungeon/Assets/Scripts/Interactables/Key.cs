using UnityEngine;

public class Key : Interactable
{
    private PlayerMovement holder;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (holder != null)
            return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        player.PickUpKey(this);
    }

    public void AttachToPlayer(PlayerMovement player)
    {
        holder = player;

        transform.SetParent(player.transform);

        transform.localPosition = new Vector3(0f, 0.75f, 0f);
    }
}