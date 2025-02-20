using System;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static event Action OnCombatTriggered;
    public bool combatActive = false;

    public void TriggerCombat()
    {
        Debug.Log("Combat Triggered!");
        combatActive = true;
        OnCombatTriggered?.Invoke(); // Notify all listeners
    }
}
