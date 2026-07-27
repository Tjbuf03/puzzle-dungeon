using UnityEngine;

public class ActionPickUp : MonoBehaviour
{
    ActionManager actionManager;
    public GameObject ActionManagerObject;
    public int ActionsGiven;
    void Start()
    {
        actionManager = ActionManagerObject.GetComponent<ActionManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        actionManager.AddActions(ActionsGiven);
        gameObject.SetActive(false);
    }
}
