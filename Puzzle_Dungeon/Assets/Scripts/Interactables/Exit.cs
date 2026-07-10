using UnityEngine;

public class Exit : Interactable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        LevelManager.Instance.CompleteLevel();
    }
}