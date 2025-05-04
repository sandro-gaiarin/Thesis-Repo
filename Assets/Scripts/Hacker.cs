using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : Unit
{

    // Start is called before the first frame update

    //Health bar addons

    [SerializeField] private float maxHealth = 100;
    private float _currentHealth;
    [SerializeField] private HealthBar _healthBar;
    void Start()
    {
        _currentHealth = maxHealth;
        _healthBar.UpdateHealthBar(maxHealth, _currentHealth);
        //End of health bar addons bc i didn't wanna mess with any current code - kayla
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DamageRoll()
    {
        Debug.Log("Rolling for damage...");
        this.attackDamage = Random.Range(1, 6);
        TooltipUI.ShowMessage("Quinn dealt" + this.attackDamage + "damage!");
    }
}
