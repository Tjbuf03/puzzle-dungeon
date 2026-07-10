using UnityEngine;

public class Button : Interactable
{
    [SerializeField] private string targetDoorID;

    private bool pressed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pressed)
            return;

        if (!other.CompareTag("Player"))
            return;

        pressed = true;

        InteractionManager.Instance.ActivateDoor(targetDoorID);

        Debug.Log($"Pressed button for {targetDoorID}");
    }
}