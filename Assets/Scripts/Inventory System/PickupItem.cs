using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PickupItem : MonoBehaviour
{
    public ItemData itemData; // Assign Memo 1 in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            Debug.Log($"Player picked up: {itemData.itemName}");

            InventoryManager inventory = GameObject.Find("InventoryManager")?.GetComponent<InventoryManager>();
            if (inventory != null)
            {
                InventoryManager.Instance.AddItem(itemData, 1, itemData);

                // ✅ Update document counters
                if (itemData.itemType == ItemData.ItemType.Quest)
                {
                    GameManager.Instance?.AddDocument();
                }

                Destroy(gameObject);
            }
        }
    }
}
