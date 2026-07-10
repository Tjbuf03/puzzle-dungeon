using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    private readonly Dictionary<string, List<Door>> doors = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RegisterDoors();
    }

    private void RegisterDoors()
    {
        Door[] sceneDoors = Object.FindObjectsByType<Door>(FindObjectsSortMode.None);

        foreach (Door door in sceneDoors)
        {
            if (!doors.ContainsKey(door.DoorID))
                doors.Add(door.DoorID, new List<Door>());

            doors[door.DoorID].Add(door);
        }
    }

    public void ActivateDoor(string id)
    {
        if (!doors.ContainsKey(id))
            return;

        foreach (Door door in doors[id])
        {
            door.SetOpen(true);
        }
    }
}