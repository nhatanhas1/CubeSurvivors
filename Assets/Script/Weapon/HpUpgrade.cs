using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUpgrade : PlayerUpgradePower
{
    //[SerializeField] PlayerController playerController;

    //[SerializeField] UpgradeManager upgradeButtonManager;

    //[SerializeField] WeaponData weaponData;
    //[SerializeField] private int weaponId;
    //[SerializeField] string weaponName;
    //[SerializeField] int weaponCurrentLv;
    int maxUpgradeLevel = 7;

    //public WeaponData WeaponData { get => weaponData; set { weaponData = value; } }
    //public int WeaponID { get => weaponId; set { weaponId = value; } }
    //public string WeaponName { get => weaponName; set { weaponName = value; } }
    //public int WeaponLv { get => weaponLv; set { weaponLv = value; } }
    //public Sprite sprite { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();
        weaponId = 4;
        //weaponName = "UpMaxHP";
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


    //public void AddToUpgradeButton()
    //{
    //    //Debug.Log("update Armor Btton");
    //    upgradeButtonManager = FindObjectOfType<UpgradeManager>();
    //    if (weaponCureentLv <= maxUpgradeLevel)
    //    {
    //        upgradeButtonManager.currentUpgradePartList.Add(PlayerController.UpgradePart.Hp);
    //    }

    //}

    public override void Upgrade()
    {
        weaponCurrentLv++;
        switch (weaponCurrentLv)
        {
            case 1:
                Debug.Log("upgrade MaxHp");
                playerController.hitPoint += 50;
                UIManager.Instance.SetUpHeathBar(playerController.hitPoint, playerController.currentHitPoint);
                break;
            case 2:
                playerController.hitPoint += 50;
                UIManager.Instance.SetUpHeathBar(playerController.hitPoint, playerController.currentHitPoint);
                break;
            case 3:
                playerController.hitPoint += 50;
                UIManager.Instance.SetUpHeathBar(playerController.hitPoint, playerController.currentHitPoint);

                break;
            case 4:
                playerController.hitPoint += 50;
                UIManager.Instance.SetUpHeathBar(playerController.hitPoint, playerController.currentHitPoint);
                break;
            case 5:
                playerController.hitPoint += 50;
                UIManager.Instance.SetUpHeathBar(playerController.hitPoint, playerController.currentHitPoint);

                break;
            case 6:
                playerController.hitPoint += 50;
                UIManager.Instance.SetUpHeathBar(playerController.hitPoint, playerController.currentHitPoint);

                break;
            case 7:
                playerController.hitPoint += 50;
                UIManager.Instance.SetUpHeathBar(playerController.hitPoint, playerController.currentHitPoint);
                break;

        }
        

    }
}
