
using UnityEngine;
using UnityEngine.InputSystem; 

public class DoorSensor : MonoBehaviour
{
    [SerializeField] private string requiredKeyID = "MainDoorKey";
    private bool isPlayerZone = false;
    private PlayerInventory playerInventory = null;
    private bool isUnlocked = false;

    // References to the parent door components
    private SpriteRenderer parentSprite;
    private BoxCollider2D parentCollider;

    private void Start()
    {
        // Grab the components from the main solid door parent
        parentSprite = transform.parent.GetComponent<SpriteRenderer>();
        parentCollider = transform.parent.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Only look for the E keypress if the door hasn't been unlocked yet
        if (!isUnlocked && Keyboard.current != null && isPlayerZone && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (playerInventory != null)
            {
                if (playerInventory.HasKey() && playerInventory.GetCurrentKeyID() == requiredKeyID)
                {
                    playerInventory.UseKey();
                    UnlockDoor(); 
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null && collision.attachedRigidbody.gameObject.name == "Player")
        {
            isPlayerZone = true;
            playerInventory = collision.attachedRigidbody.GetComponent<PlayerInventory>();

            // If the door was already unlocked, make it gray and passable when entering the box
            if (isUnlocked)
            {
                OpenPassage();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null && collision.attachedRigidbody.gameObject.name == "Player")
        {
            isPlayerZone = false;
            playerInventory = null;

            // When the player completely exits the sensor box, lock it back up to Red!
            if (isUnlocked)
            {
                ClosePassage();
            }
        }
    }

    private void UnlockDoor()
    {
        isUnlocked = true;
        Debug.Log("Door successfully unlocked! You can now pass through.");
        OpenPassage();
    }

    private void OpenPassage()
    {
        // Turn the door gray (with some transparency if you want)
        parentSprite.color = new Color(0.5f, 0.5f, 0.5f, 0.6f); 
        
        // Disable the solid collider so the player can walk through the wall
        parentCollider.enabled = false; 
    }

    private void ClosePassage()
    {
        // Turn the door back to solid, vibrant Red
        parentSprite.color = Color.red; 
        
        // Re-enable the solid collider so they can't walk backward without re-entering the zone
        parentCollider.enabled = true; 
    }
}