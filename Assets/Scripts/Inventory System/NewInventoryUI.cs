using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public Inventory inventory;      // Reference to the Inventory script (assigned via code)
    public GameObject slotPrefab;    // Prefab for the inventory slot
    public Transform itemContainer;  // Parent object holding slots
    public GameObject inventoryCanvas; // Assign the inventory Canvas in Inspector

    private List<GameObject> slots = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
            return;
        }
    }

    void Start()
    {
        // Automatically find InventoryManager and get its Inventory component
        GameObject inventoryManager = GameObject.Find("InventoryManager");
        if (inventoryManager != null)
        {
            inventory = inventoryManager.GetComponent<Inventory>();
        }
        else
        {
            Debug.LogError("InventoryManager not found in the scene!");
        }

        inventoryCanvas.SetActive(false); // Ensure it's off at start
        RefreshUI(); // Populate the inventory UI initially
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Check if 'I' is pressed
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        bool isActive = inventoryCanvas.activeSelf;
        inventoryCanvas.SetActive(!isActive);

        if (!isActive)
        {
            RefreshUI();
        }
    }

    public void RefreshUI()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory reference is missing!");
            return;
        }

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

    void UseItem(NewInventoryItem item)
    {
        Debug.Log($"Used {item.itemData.itemName}");
    }
}
