using System;
using UnityEngine;
using System.Collections; // <-- Needed for IEnumerator


public class CombatManager : MonoBehaviour
{
    public static event Action OnCombatTriggered;
    public static event Action OnCombatEnded;
    public bool combatActive = false;

    public ExplorationManager explorationManager;
    public ExplorationUnitController explorationUnitController;

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

        else
        {
            EnableCombatMode(); // Ensures combat mode starts enabled
            if (explorationManager) explorationManager.DisableExplorationMode();
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
            explorationManager.gameObject.SetActive(true);
            foreach (Transform child in explorationManager.transform)
            {
                child.gameObject.SetActive(true);
            }

            ExplorationUnitController explorationUnitController = explorationManager.GetComponent<ExplorationUnitController>();
            if (explorationUnitController != null)
            {
                GameObject hacker = GameObject.Find("Hacker");
                if (hacker != null)
                {
                    explorationUnitController.selectedUnit = hacker;
                    Debug.Log("Hacker has been set as the selected unit.");
                }
                else
                {
                    Debug.LogWarning("Could not find GameObject named 'Hacker'.");
                }
            }
            else
            {
                Debug.LogWarning("ExplorationUnitController not found on ExplorationManager.");
            }
        }
    }


    public void RequestCombatEnd()
    {
        StartCoroutine(DelayedEndCombat());
    }

    private IEnumerator DelayedEndCombat()
    {
        yield return new WaitForSeconds(0.1f); // Short delay to finish any pending coroutines
        EndCombat();
    }

}
