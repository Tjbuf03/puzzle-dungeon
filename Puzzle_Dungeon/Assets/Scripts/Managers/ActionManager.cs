using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance { get; private set; }

    [SerializeField] private int maxActions = 30;
    [SerializeField] private Slider actionBar;

    public int CurrentActions { get; private set; }

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
        ResetActions();
    }

    public bool SpendActions(int amount)
    {
        if (CurrentActions < amount)
            return false;

        CurrentActions -= amount;
        UpdateUI();

        return true;
    }

    public void ResetActions()
    {
        CurrentActions = maxActions;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (actionBar == null)
            return;

        actionBar.maxValue = maxActions;
        actionBar.value = CurrentActions;
    }
    public void AddActions(int ActionsToAdd)
    {
        //Debug.Log("" + CurrentActions);
        CurrentActions += ActionsToAdd;
        UpdateUI();
       // Debug.Log("added actions");
        //Debug.Log("" + CurrentActions);

    }
}