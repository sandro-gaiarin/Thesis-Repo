using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        CombatManager.OnCombatTriggered += DisableExplorationMode;
    }

    void OnDisable()
    {
        CombatManager.OnCombatTriggered -= DisableExplorationMode;
    }

    void DisableExplorationMode()
    {
        Debug.Log("Exploration Mode Disabled!");
       foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
