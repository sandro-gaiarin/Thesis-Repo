using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Inventory inventory; // Reference to player's inventory
    public List<ItemData> shopItems = new List<ItemData>(); // List of available shop items
    public GameObject shopItemPrefab; // Prefab for shop items
    public Transform shopItemContainer; // Parent object holding shop item buttons
    public int playerCoins = 50; // Starting money
    public TMP_Text coinText; // UI text for coins
    public ShopTooltip shopTooltip;
    public ShopInventoryUI shopInventoryUI; // Reference to the shop's inventory panel UI

    private List<GameObject> shopButtons = new List<GameObject>();

    void Start()
    {
        RefreshShop();
        UpdateCoinText();
        shopInventoryUI.RefreshInventory(); // Ensure the shop inventory starts fresh
    }

    void RefreshShop()
    {
        foreach (GameObject button in shopButtons)
        {
            Destroy(button);
        }
        shopButtons.Clear();

        foreach (ItemData item in shopItems)
        {
            GameObject shopButton = Instantiate(shopItemPrefab, shopItemContainer);
            shopButtons.Add(shopButton);

            // Set button text
            TMP_Text buttonText = shopButton.GetComponentInChildren<TMP_Text>();
            if (buttonText)
                buttonText.text = $"{item.itemName} ${item.price}";

            // Set item icon
            Image itemIcon = shopButton.GetComponentInChildren<Image>();
            if (itemIcon)
                itemIcon.sprite = item.icon;

            // Add button click event for purchasing
            Button button = shopButton.GetComponent<Button>();
            if (button)
                button.onClick.AddListener(() => BuyItem(item));

            // Tooltip Events
            ShopItemHover hoverHandler = shopButton.AddComponent<ShopItemHover>();
            hoverHandler.shopTooltip = shopTooltip;
            hoverHandler.itemDescription = item.description;
        }
    }

    void BuyItem(ItemData item)
    {
        if (playerCoins >= item.price)
        {
            playerCoins -= item.price;
            inventory.AddItem(item, 1);
            UpdateCoinText();
            shopInventoryUI.RefreshInventory(); // Update the player's inventory panel
            Debug.Log($"Bought {item.itemName}!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    void UpdateCoinText()
    {
        coinText.text = "Dollars: " + playerCoins;
    }
}
