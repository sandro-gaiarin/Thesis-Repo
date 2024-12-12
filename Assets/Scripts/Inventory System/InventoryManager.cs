using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;  // Declare the static instance

    public GameObject Inventory;
    public bool menuActivated;
    public ItemSlot[] itemSlot;
    public GameObject actionBars;

    void Awake()
    {
       /* if (instance == null)
        {
            instance = this;  // Assign the current instance
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);  // Prevent duplicates
        }*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuActivated = !menuActivated;
            Inventory.SetActive(menuActivated);
            actionBars.SetActive(!menuActivated);
        }
    }

    public void AddItem(string itemName, int quantity, string itemDescription)
    {
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
