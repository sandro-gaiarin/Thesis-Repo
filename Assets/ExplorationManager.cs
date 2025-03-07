using UnityEngine;

public class ExplorationManager : MonoBehaviour
{
    public GameObject combatManager; // Reference to CombatManager

    void Start()
    {
        // Ensure ExplorationManager is ON at start if combat is active
        if (combatManager != null && combatManager.activeSelf)
        {
            gameObject.SetActive(false); // Disable Exploration Mode if Combat is running
        }
    }

    void Awake()
    {
        CombatManager foundManager = FindObjectOfType<CombatManager>();
        if (foundManager != null)
        {
            combatManager = foundManager.gameObject;
        }
        else
        {
            Debug.LogError("CombatManager not found in the scene!");
        }
    }

    void OnEnable()
    {
        CombatManager.OnCombatTriggered += DisableExplorationMode;
        CombatManager.OnCombatEnded += EnableExplorationMode;
    }

    void OnDisable()
    {
        CombatManager.OnCombatTriggered -= DisableExplorationMode;
        CombatManager.OnCombatEnded -= EnableExplorationMode;
    }

    public void DisableExplorationMode()
    {
        Debug.Log("Exploration Mode Disabled!");
        gameObject.SetActive(false); // Disable ExplorationManager

        if (combatManager)
        {
            foreach (Transform child in combatManager.transform) // Fixed reference
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void EnableExplorationMode()
    {
        Debug.Log("Exploration Mode Enabled!");
        gameObject.SetActive(true); // Enable ExplorationManager

        if (combatManager)
        {
            foreach (Transform child in combatManager.transform) // Fixed reference
            {
                child.gameObject.SetActive(false); // Now properly disables CombatManager children
            }
        }
    }
}
