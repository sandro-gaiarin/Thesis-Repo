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
        GameObject closestPlayer = FindClosestPlayerUnit();
        if (closestPlayer == null) return;

        Vector3 doorPoint = GetClosestEdge(gameObject, closestPlayer);
        Vector3 playerPoint = GetClosestEdge(closestPlayer, gameObject);

        float distance = Vector3.Distance(doorPoint, playerPoint);
        Debug.Log($"Distance to closest player: {distance}");

        Debug.DrawLine(playerPoint, doorPoint, Color.blue);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Interacting with door tile.");
            TryUnlockDoor();
        }
    }

    GameObject FindClosestPlayerUnit()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerUnit");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject p in players)
        {
            float dist = Vector3.Distance(p.transform.position, transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = p;
            }
        }

        return closest;
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
            float distance = Vector3.Distance(
                GetClosestEdge(gameObject, door),
                GetClosestEdge(door, gameObject)
            );

            if (distance <= doorDestroyRadius)
            {
                Debug.Log("WarehouseDoor found and destroyed.");
                Destroy(door);
            }
        }

        Debug.LogWarning("No WarehouseDoor found within range.");
    }

    Vector3 GetClosestEdge(GameObject source, GameObject target)
    {
        Collider sourceCollider = source.GetComponentInChildren<Collider>();
        if (sourceCollider != null)
        {
            return sourceCollider.ClosestPoint(target.transform.position);
        }
        return source.transform.position;
    }


}
