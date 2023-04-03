using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon2 : PlayerUpgradePower
{
    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        Debug.Log("TestWEapon2 tang cong");
    }
}
