using UnityEngine;

public class SecurityPadIndicator : MonoBehaviour
{
    [SerializeField] private Renderer padRenderer;
    [SerializeField] private Color safeColor = Color.green;
    [SerializeField] private Color combatColor = Color.red;

    private void Start()
    {
        if (padRenderer == null)
            padRenderer = GetComponent<Renderer>();

        padRenderer.material.color = FindObjectOfType<CombatManager>()?.combatActive == true
            ? combatColor
            : safeColor;
    }


    private void OnEnable()
    {
        CombatManager.OnCombatTriggered += SetColorCombat;
        CombatManager.OnCombatEnded += SetColorSafe;
    }

    private void OnDisable()
    {
        CombatManager.OnCombatTriggered -= SetColorCombat;
        CombatManager.OnCombatEnded -= SetColorSafe;
    }

    private void SetColorCombat()
    {
        padRenderer.material.color = combatColor;
    }

    private void SetColorSafe()
    {
        padRenderer.material.color = safeColor;
    }
}
