using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Key currentKey = null;

    public bool HasKey()
    {
        return currentKey != null;
    }

    public string GetCurrentKeyID()
    {
        return currentKey != null ? currentKey.GetKeyID() : string.Empty;
    }

    public void PickUpKey(Key key)
    {
        currentKey = key;
        Debug.Log("Key picked up: " + key.GetKeyID());
    }

    public void UseKey()
    {
        if (currentKey != null)
        {
            Destroy(currentKey.gameObject);
            currentKey = null;
        }
    }

    public void DropKey()
    {
        if (currentKey != null)
        {
            currentKey.transform.position = transform.position;
            currentKey.gameObject.SetActive(true);
            currentKey = null;
            Debug.Log("Key dropped!");
        }
    }
}