using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;      // Reference to the Inventory script
    public GameObject slotPrefab;    // Prefab for the inventory slot
    public Transform itemContainer;  // Parent object holding slots

    private List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        RefreshUI(); // Populate the inventory on start
    }

    // Refresh the inventory UI
    public void RefreshUI()
    {
        // Clear existing slots
        foreach (GameObject slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();

        // Create a new slot for each item in the inventory
        foreach (NewInventoryItem item in inventory.items)
        {
            GameObject slot = Instantiate(slotPrefab, itemContainer);
            slots.Add(slot);

            // Set the icon to the item's sprite
            Image icon = slot.GetComponentInChildren<Image>();
            if (icon) icon.sprite = item.itemData.icon;

            // Add button interaction for item use
            Button button = slot.GetComponent<Button>();
            if (button) button.onClick.AddListener(() => UseItem(item));
        }
    }

    // Example method for using an item
    void UseItem(NewInventoryItem item)
    {
        Debug.Log($"Used {item.itemData.itemName}");
    }
}
