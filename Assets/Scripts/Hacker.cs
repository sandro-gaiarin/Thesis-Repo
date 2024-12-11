using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : Unit
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DamageRoll()
    {
        Debug.Log("Rolling for damage...");
        this.attackDamage = Random.Range(1, 6);
    }
}
