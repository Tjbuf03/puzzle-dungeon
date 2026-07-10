using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }

    [Header("Player Prefab")]
    [SerializeField] private PlayerMovement playerPrefab;

    private readonly Dictionary<PartyMember, PlayerMovement> players = new();

    private PartyMember activePlayer = PartyMember.Blue;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Debug.Log("PartyManager Start");

        SpawnPlayers();
        SetActivePlayer(0);

    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.digit1Key.wasPressedThisFrame)
            SetActivePlayer(0);

        if (keyboard.digit2Key.wasPressedThisFrame)
            SetActivePlayer(1);

        if (keyboard.digit3Key.wasPressedThisFrame)
            SetActivePlayer(2);
    }

    private void SpawnPlayers()
    {
        PlayerSpawn[] spawns = Object.FindObjectsByType<PlayerSpawn>(FindObjectsSortMode.None);

        foreach (PlayerSpawn spawn in spawns)
        {
            PlayerMovement player = Instantiate(playerPrefab);

            player.SetPosition(spawn.transform.position);

            players.Add(spawn.Player, player);
        }
    }

    private void SetActivePlayer(int index)
    {
        if (index < 0 || index >= players.Count)
            return;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetControl(i == index);
        }

        activePlayer = index;
    }

    public PlayerMovement ActivePlayer => players[activePlayer];
}