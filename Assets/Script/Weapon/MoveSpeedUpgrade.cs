using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpgrade : PlayerUpgradePower
{
    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();
        weaponId = 6;
        //weaponName = "MoveSpeed";
        weaponCurrentLv = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetWeaponHandler(PlayerController player)
    {
        playerController = player;
    }

    public override void Upgrade()
    {
        weaponCurrentLv++;
        switch (weaponCurrentLv)
        {
            case 1:
                //playerController.armor += 3;
                playerController.moveSpeed += 1;
                break;
            case 2:
                playerController.moveSpeed += 1;

                break;
            case 3:
                playerController.moveSpeed += 1;

                break;
            case 4:
                playerController.moveSpeed += 1;
                break;
            case 5:
                playerController.moveSpeed += 1;

                break;

        }
       

    }
}
