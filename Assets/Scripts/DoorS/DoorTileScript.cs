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

        float distance = Vector3.Distance(GetColliderCenter(gameObject), GetColliderCenter(player));
        Debug.Log($"Distance to door: {distance}");

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Interacting with door tile.");
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

                gameObject.tag = "Tile"; // Unlock the tile
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
            float distance = Vector3.Distance(GetColliderCenter(gameObject), GetColliderCenter(door));
            if (distance <= doorDestroyRadius)
            {
                Debug.Log("WarehouseDoor found and destroyed.");
                Destroy(door);
            }
        }

        Debug.LogWarning("No WarehouseDoor found within range.");
    }

    Vector3 GetColliderCenter(GameObject obj)
    {
        Collider col = obj.GetComponentInChildren<Collider>();
        return col != null ? col.bounds.center : obj.transform.position;
    }
}
