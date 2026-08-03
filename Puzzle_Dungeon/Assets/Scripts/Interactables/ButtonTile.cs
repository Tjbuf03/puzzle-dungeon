using UnityEngine;

public class ButtonTile : Interactable
{
    [Header("Door Target")]
    [SerializeField] private string targetDoorID;

    [Header("Player Requirement")]
    [SerializeField] private ButtonRequirement requiredPlayer;

    private bool pressed;

    public void TryPressButton(PlayerMovement player)
    {
        if (pressed)
            return;

        if (!CanBePressedBy(player))
        {
            Debug.Log($"Wrong player. This button requires {requiredPlayer}, but {player.Member} stepped on it.");
            return;
        }

        pressed = true;

        InteractionManager.Instance.ActivateDoor(targetDoorID);

        Debug.Log($"{requiredPlayer} pressed button for {targetDoorID}");
    }

    private bool CanBePressedBy(PlayerMovement player)
    {
        if (requiredPlayer == ButtonRequirement.AnyPlayer)
            return true;

        return player.Member switch
        {
            PartyMember.Blue => requiredPlayer == ButtonRequirement.Blue,
            PartyMember.Purple => requiredPlayer == ButtonRequirement.Purple,
            PartyMember.Orange => requiredPlayer == ButtonRequirement.Orange,
            _ => false
        };
    }
}