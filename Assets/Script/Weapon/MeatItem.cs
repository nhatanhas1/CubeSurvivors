using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatItem : PlayerUpgradePower  
{
    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();
        weaponId = 7;
        //weaponName = WeaponData.name;
        //weaponMaxLv = 1;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetWeaponHandler(PlayerController player)
    {
        playerController = player;
    }


    public void AddToUpgradeButton()
    {
        //Debug.Log("add meat to upgradeList");
        upgradeButtonManager = FindObjectOfType<UpgradeManager>();
        upgradeButtonManager.currentUpgradePartList.Add(WeaponData.weaponType);

    }

    public override void Upgrade()
    {
        //Debug.Log("upgrade MaxHp");
        playerController.currentHitPoint = playerController.hitPoint;
        UIManager.Instance.SetUpHeathBar(playerController.hitPoint, playerController.currentHitPoint);

    }
}
