using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : PlayerUpgradePower
{

    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();
        weaponId = 9;
        //weaponName = weaponData.name;

        weaponCurrentLv = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //public void SetWeaponHandler(PlayerController player)
    //{
    //    playerController = player;  
    //}


    public override void Upgrade()
    {
        weaponCurrentLv++;
        switch (weaponCurrentLv)
        {
            case 1:
                playerController.armor += 3;
                break;
            case 2:
                playerController.armor += 3;

                break;
            case 3:
                playerController.armor += 5;
 
                break;
            case 4:
                playerController.armor += 5;
                break;
            case 5:
                playerController.armor += 7;

                break;
            case 6:
                playerController.armor += 7;

                break;
            case 7:
                playerController.armor += 10;
                break;

        }
        
        
    }
}
