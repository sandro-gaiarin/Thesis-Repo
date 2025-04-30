using UnityEngine;

public class DoorTile : MonoBehaviour
{
    public string requiredItemName = "KeyCard1"; // The name of the required item
    public float interactionDistance = 3f;       // Distance to player to allow interaction
    public float doorDestroyRadius = 5f;         // Radius to search for WarehouseDoor prefab

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerUnit");

        if (player == null)
        {
            Debug.LogError("Player with tag 'PlayerUnit' not found in the scene!");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            TryUnlockDoor();
        }
    }

    void TryUnlockDoor()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager instance not found!");
            return;
        }

        foreach (var item in InventoryManager.Instance.items)
        {
            if (item.itemData.itemName == requiredItemName && item.quantity > 0)
            {
                Debug.Log("Correct keycard found! Unlocking door.");

                // Unlock the tile
                gameObject.tag = "Tile";

                // Search for nearby WarehouseDoor and destroy it
                DestroyNearbyWarehouseDoor();

                return;
            }
        }

        Debug.Log("Missing required item: " + requiredItemName);
    }

    void DestroyNearbyWarehouseDoor()
    {
        GameObject[] allDoors = GameObject.FindGameObjectsWithTag("WarehouseDoor");

        foreach (GameObject door in allDoors)
        {
            float distance = Vector3.Distance(transform.position, door.transform.position);
            if (distance <= doorDestroyRadius)
            {
                Debug.Log("WarehouseDoor found and destroyed.");
                Destroy(door);
                //return; // Exit after destroying one (if you only want one removed)
            }
        }

        Debug.LogWarning("No WarehouseDoor found within range.");
    }
}
