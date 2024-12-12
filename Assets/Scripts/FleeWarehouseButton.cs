using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FleeWarehouseButton : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void FleeWarehouse()
    {
        GameObject uiObject = GameObject.Find("UI");
        if (uiObject != null)
        {
            foreach (Transform child in uiObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        // Load the main menu scene
        SceneManager.LoadScene("ink2");
    }   
}
