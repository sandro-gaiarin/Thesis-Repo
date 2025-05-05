using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PickupItem : MonoBehaviour
{
    public ItemData itemData; // Assign in Inspector (e.g., Memo 1)
    private bool openedInventoryBefore = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerUnit")) return;

        Debug.Log($"Player picked up: {itemData.itemName}");

        string message = itemData.itemName + " added to inventory.";
        if (!openedInventoryBefore)
        {
            openedInventoryBefore = true;
            message += " Press I to open it.";
        }

        TooltipUI.ShowMessage(message);

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(itemData, 1);

            if (itemData.itemType == ItemData.ItemType.Quest)
            {
                GameManager.Instance?.AddDocument();
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("InventoryManager.Instance not found!");
        }
    }
}
