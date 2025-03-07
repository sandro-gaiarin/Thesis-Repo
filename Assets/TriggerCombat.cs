using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCombat : MonoBehaviour
{
    public GameObject CombatManagerParent;
    public GameObject ExplorationManager;
    // Start is called before the first frame update
    void Start()
    {
        CombatManagerParent = GameObject.Find("Combat Manager Parent");
        ExplorationManager = GameObject.Find("Exploration Manager");
    }

    public void TriggerCombatStart()
    {
        foreach (Transform child in CombatManagerParent.transform)
        {
            child.gameObject.SetActive(true);
        }

        ExplorationManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
