// InventoryManager.cs
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public bool menuActivated;
    public ItemSlot[] itemSlot;
    public GameObject actionBars;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && menuActivated)
        {
            
            Inventory.SetActive(false);
            actionBars.SetActive(true);
            menuActivated = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.I) && !menuActivated)
        {
            
            Inventory.SetActive(true);
            actionBars.SetActive(false);
            menuActivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, string itemDescription)
    {
        //Debug.Log("itemName =" + itemName);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemDescription);
                return;
            }
        }

    }
}
