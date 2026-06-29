using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    [Header("Action Points")]
    public int maxActions = 30;
    public int currentActions;

    [Header("UI")]
    public Slider actionBar;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentActions = maxActions;
        UpdateUI();
    }

    public bool SpendActions(int amount)
    {
        if (currentActions < amount)
            return false;

        currentActions -= amount;
        UpdateUI();

        Debug.Log($"Actions Remaining: {currentActions}");

        return true;
    }

    public void ResetActions()
    {
        currentActions = maxActions;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (actionBar == null)
            return;

        actionBar.maxValue = maxActions;
        actionBar.value = currentActions;
    }
}