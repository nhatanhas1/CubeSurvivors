using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUpgrade : PlayerUpgradePower
{
    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();
        weaponId = 5;


        weaponCurrentLv = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Upgrade()
    {
        weaponCurrentLv++;
        switch (weaponCurrentLv)
        {
            case 1:
                playerController.attackSpeed += 10;
                playerController.SetUpActiveWeapon();
                break;
            case 2:
                playerController.attackSpeed += 10;
                playerController.SetUpActiveWeapon();
                break;
            case 3:
                playerController.attackSpeed += 10;
                playerController.SetUpActiveWeapon();
                break;
            case 4:
                playerController.attackSpeed += 10;
                playerController.SetUpActiveWeapon();
                break;
            case 5:
                playerController.attackSpeed += 10;
                playerController.SetUpActiveWeapon();
                break;
            case 6:
                playerController.attackSpeed += 10;
                playerController.SetUpActiveWeapon();
                break;
            case 7:
                playerController.attackSpeed += 10;
                playerController.SetUpActiveWeapon();
                break;

        }
        

    }
}
