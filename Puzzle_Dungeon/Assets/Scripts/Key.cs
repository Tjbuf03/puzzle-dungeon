using UnityEngine;

public class Key : MonoBehaviour
{
   [SerializeField] private string keyID = "MainDoorKey";

    public string GetKeyID()
    {
        return keyID;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            // Give the key to the player
            playerInventory.PickUpKey(this);
            
            // Deactivate the key object so it disappears from the ground
            gameObject.SetActive(false);
        }
    }
}
