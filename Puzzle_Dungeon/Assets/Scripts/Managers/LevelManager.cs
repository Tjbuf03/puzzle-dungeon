using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject levelCompletePanel;

    [Header("Level")]
    [SerializeField] private string nextSceneName;

    private int escapedPlayers;
    private int totalPlayers;

    private bool levelComplete;

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
        totalPlayers = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None).Length;

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }

    private void Update()
    {
        // Restart the current level
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
            return;
        }

        // Only allow continuing if the level has been completed
        if (!levelComplete)
            return;

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
    }

    public void PlayerEscaped(PlayerMovement player)
    {
        if (!player.gameObject.activeSelf)
            return;

        escapedPlayers++;

        player.gameObject.SetActive(false);

        PartyManager.Instance.SelectNextAvailablePlayer();

        if (escapedPlayers >= totalPlayers)
        {
            levelComplete = true;

            if (levelCompletePanel != null)
                levelCompletePanel.SetActive(true);
        }
    }

    private void LoadNextLevel()
    {
        if (string.IsNullOrWhiteSpace(nextSceneName))
        {
            Debug.LogWarning("Next Scene Name has not been assigned!");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}