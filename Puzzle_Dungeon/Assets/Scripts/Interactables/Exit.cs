using UnityEngine;

public class Exit : Interactable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        LevelManager.Instance.PlayerEscaped(player);
    }
}