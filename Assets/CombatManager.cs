using System;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static event Action OnCombatTriggered;
    public static event Action OnCombatEnded;
    public bool combatActive = false;

    public ExplorationManager explorationManager;

    void Awake()
    {
        // Find ExplorationManager, even if inactive
        explorationManager = FindObjectOfType<ExplorationManager>(true);

        if (explorationManager == null)
        {
            Debug.LogError("ExplorationManager not found in the scene!");
        }
    }

    void Start()
    {
        if (!combatActive)
        {
            DisableCombatMode(); // Ensures combat mode starts disabled
            if (explorationManager) explorationManager.EnableExplorationMode();
        }
    }

    public void TriggerCombat()
    {
        if (!combatActive)
        {
            Debug.Log("Combat Triggered!");
            combatActive = true;
            OnCombatTriggered?.Invoke();

            EnableCombatMode();
            if (explorationManager) explorationManager.DisableExplorationMode();
        }
    }

    public void EndCombat()
    {
        if (combatActive)
        {
            Debug.Log("Combat Ended!");
            combatActive = false;
            OnCombatEnded?.Invoke();

            DisableCombatMode();
            if (explorationManager) explorationManager.EnableExplorationMode();
        }
    }

    void EnableCombatMode()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        if (explorationManager)
        {
            foreach (Transform child in explorationManager.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    void DisableCombatMode()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        if (explorationManager)
        {
            foreach (Transform child in explorationManager.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
