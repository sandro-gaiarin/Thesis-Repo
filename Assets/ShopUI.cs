using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    public GameObject shopItemPrefab;
    public Transform shopItemContainer;
    public TMP_Text coinText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RefreshShopUI();
        UpdateCoinDisplay();
    }

    public void RefreshShopUI()
    {
        foreach (Transform child in shopItemContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemData item in ShopManager.Instance.availableItems)
        {
            GameObject slot = Instantiate(shopItemPrefab, shopItemContainer);
            ShopItemSlot shopItemSlot = slot.GetComponent<ShopItemSlot>();

            if (shopItemSlot != null)
            {
                shopItemSlot.Setup(item);
            }
        }

        coinText.text = $"Coins: {ShopManager.Instance.playerCoins}";
    }

    public void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {InventoryManager.Instance.playerCoins}";
        }
    }

}