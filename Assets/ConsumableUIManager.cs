using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsumableUIManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;           // The ConsumableButton prefab
    [SerializeField] private Transform buttonContainer;         // The Content area under the scroll panel or layout group
    [SerializeField] private GameObject panel;                  // The actual UI panel to toggle
    [SerializeField] private UnitController unitController;     // Reference to UnitController to get selected unit

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void TogglePanel()
    {
        if (panel == null) return;

        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            RefreshConsumableButtons();
            panel.SetActive(true);
        }
    }

    private void RefreshConsumableButtons()
    {
        // Clear existing buttons
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // Get all consumables
        List<NewInventoryItem> items = InventoryManager.Instance.items;

        foreach (NewInventoryItem item in items)
        {
            if (item.itemData.itemType == ItemData.ItemType.Consumable)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
                TMP_Text label = buttonObj.GetComponentInChildren<TMP_Text>();
                if (label != null)
                {
                    label.text = $"{item.itemData.itemName} x{item.quantity}";
                }

                Button button = buttonObj.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => UseConsumable(item));
                }
            }
        }
    }

    private void UseConsumable(NewInventoryItem item)
    {
        GameObject selected = unitController.selectedUnit;
        if (selected == null) return;

        Unit unit = selected.GetComponent<Unit>();
        if (unit == null) return;

        if (item.itemData.itemName == "First Aid Kit")
        {
            unit.health += item.itemData.healAmount;
            if (unit.health > 100) unit.health = 100;

            InventoryManager.Instance.RemoveItem(item.itemData, 1);
            TooltipUI.ShowMessage($"{item.itemData.itemName} used! {unit.unitName} healed for {item.itemData.healAmount} HP.");
        }

        TogglePanel(); // Close the panel after using
    }
}
