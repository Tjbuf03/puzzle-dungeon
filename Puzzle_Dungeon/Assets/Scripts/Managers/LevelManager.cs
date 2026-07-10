using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject levelCompletePanel;

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
        if (!levelComplete)
            return;

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
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
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Game Complete!");
            return;
        }

        SceneManager.LoadScene(nextScene);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}