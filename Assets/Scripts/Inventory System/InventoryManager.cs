using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; } // Singleton

    [SerializeField] public List<NewInventoryItem> items = new List<NewInventoryItem>(); // Inventory storage
    public GameObject inventoryCanvas; // UI Panel for inventory
    public Transform itemContainer; // Parent object that holds item slots
    public GameObject itemSlotPrefab; // Prefab for inventory slot

    private bool menuActivated = false; // Inventory toggle state

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //ToggleInventory();
        }
    }

  /*  public void ToggleInventory()
    {
        menuActivated = !menuActivated;
        //inventoryCanvas.SetActive(menuActivated);

        if (menuActivated)
        {
            RefreshUI();
        }
    }*/

    public void AddItem(ItemData itemData, int quantity, ItemData itemDescription)
    {
        Debug.Log($"Adding {quantity}x {itemData.itemName} to inventory");

        // Check if item already exists in the inventory
        NewInventoryItem existingItem = items.Find(i => i.itemData == itemData);
        if (existingItem != null)
        {
            existingItem.AddQuantity(quantity);
        }
        else
        {
            items.Add(new NewInventoryItem(itemData, quantity));
        }

        RefreshUI(); // Update the UI
    }

    public void RemoveItem(ItemData itemData, int amount)
    {
        NewInventoryItem existingItem = items.Find(i => i.itemData == itemData);

        if (existingItem != null)
        {
            existingItem.RemoveQuantity(amount);
            Debug.Log($" Removed {amount} {itemData.itemName}, Remaining: {existingItem.quantity}");

            if (existingItem.quantity <= 0)
            {
                Debug.Log($" {itemData.itemName} fully removed from inventory.");
                items.Remove(existingItem); // ✅ Removes the item if quantity is 0
            }
        }
        else
        {
            Debug.LogWarning($" Tried to remove {itemData.itemName}, but it wasn't found in inventory.");
        }

        InventoryUI.Instance?.RefreshUI();
    }


    private void RefreshUI()
    {
        // Clear existing slots
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // Create new item slots
        foreach (NewInventoryItem item in items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemContainer);
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();

            if (itemSlot != null)
            {
                itemSlot.SetItem(item.itemData.itemName, item.quantity, item.itemData.description);
            }
        }
    }
}
