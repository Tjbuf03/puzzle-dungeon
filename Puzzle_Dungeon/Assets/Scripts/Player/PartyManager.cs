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
        SetActivePlayer(PartyMember.Blue);

    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.digit1Key.wasPressedThisFrame)
            SetActivePlayer(PartyMember.Blue);

        if (keyboard.digit2Key.wasPressedThisFrame)
            SetActivePlayer(PartyMember.Purple);

        if (keyboard.digit3Key.wasPressedThisFrame)
            SetActivePlayer(PartyMember.Orange);
    }

    private void SpawnPlayers()
    {
        PlayerSpawn[] spawns = Object.FindObjectsByType<PlayerSpawn>(FindObjectsSortMode.None);

        foreach (PlayerSpawn spawn in spawns)
        {
            PlayerMovement player = Instantiate(playerPrefab);

            player.Initialize(spawn.Player);
            player.SetPosition(spawn.transform.position);

            if (players.ContainsKey(spawn.Player))
            {
                Debug.LogError($"Duplicate spawn for {spawn.Player}!");
                Destroy(player.gameObject);
                continue;
            }

            players.Add(spawn.Player, player);
        }
    }

    private void SetActivePlayer(PartyMember member)
    {
        if (!players.ContainsKey(member))
            return;

        foreach (var player in players)
        {
            player.Value.SetControl(player.Key == member);
        }

        activePlayer = member;
    }

    public void SelectNextAvailablePlayer()
    {
        foreach (PartyMember member in System.Enum.GetValues(typeof(PartyMember)))
        {
            if (!players.ContainsKey(member))
                continue;

            PlayerMovement player = players[member];

            if (player == null)
                continue;

            if (!player.gameObject.activeSelf)
                continue;

            SetActivePlayer(member);
            return;
        }
    }

    public PlayerMovement ActivePlayer => players[activePlayer];
}