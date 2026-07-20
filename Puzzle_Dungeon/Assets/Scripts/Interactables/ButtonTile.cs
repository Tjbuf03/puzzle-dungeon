using UnityEngine;

public class ButtonTile : Interactable
{
    [Header("Door Target")]
    [SerializeField] private string targetDoorID;

    [Header("Player Requirement")]
    [SerializeField] private PartyMember requiredPlayer;

    private bool pressed;

    public void TryPressButton(PlayerMovement player)
    {
        if (pressed)
            return;

        if (player.Member != requiredPlayer)
        {
            Debug.Log($"Wrong player. This button requires {requiredPlayer}, but {player.Member} stepped on it.");
            return;
        }

        pressed = true;

        InteractionManager.Instance.ActivateDoor(targetDoorID);

        Debug.Log($"{requiredPlayer} pressed button for {targetDoorID}");
    }
}