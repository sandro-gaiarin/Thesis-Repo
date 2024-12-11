using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA

    public string itemName;
    public int quantity;
    public string description;
    public bool isFull;
    public bool isSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Manager").GetComponent<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found.");
            return;
        }
    }
    
    [SerializeField] private TMP_Text itemNameText;
    public TMP_Text ItemDescriptionText;
   
        public void AddItem(string itemName,int quantity, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.description = itemDescription;
        isFull = true;

        itemNameText.text = itemName.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //OnRightClick();
        }
    }
    
    public void OnLeftClick()
    {
        isSelected = true;
        ItemDescriptionText.text = description;
        Debug.Log("Left Clicked");
    }

   


}
