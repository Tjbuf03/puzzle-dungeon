using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    // Returns the destination portal's transform to the player
    public Transform GetDestination()
    {
        return destination;
    }
}