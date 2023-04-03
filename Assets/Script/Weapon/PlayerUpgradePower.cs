using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradePower : MonoBehaviour ,IWeapon
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected UpgradeManager upgradeButtonManager;

    [SerializeField] protected WeaponData weaponData;

    [SerializeField] protected int weaponId;
    [SerializeField] protected string weaponName;
    [SerializeField] protected int weaponMaxLv;
    [SerializeField] public int weaponCurrentLv;

    [SerializeField] protected int weaponBaseDamage;
    [SerializeField] protected float baseAttackSpeed;


    public WeaponData WeaponData { get => weaponData; set { weaponData = value; } }
    public int WeaponID { get => weaponId; set { weaponId = value; } }
    public string WeaponName { get => weaponName; set { weaponName = value; } }
    public int WeaponLv { get => weaponMaxLv; set { weaponMaxLv = value; } }
    public Sprite sprite { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }





    public virtual void Upgrade()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Chay ham Upgrade Trong playerUpgradePower");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public PlayerUpgradePower()
    //{
    //    Debug.Log("Chay ham dung PlayerUpgradePower");
    //    //SetUpWeapon();
    //}


    public void SetWeaponHandler(PlayerController player)
    {
        playerController = player;
        //throw new System.NotImplementedException();
    }

    public void AddToUpgradeButton(UpgradeManager upgradeManager)
    {
        //upgradeButtonManager = FindObjectOfType<UpgradeManager>();
        if (weaponCurrentLv <= weaponMaxLv)
        {
            upgradeManager.currentUpgradePartList.Add(weaponData.weaponType);
        }

    }

    public virtual void Attack()
    {
        Debug.Log("playerUpgrade Tang cong ");
        Debug.Log("playerUpgrade Tang cong 2");
    }

    public void SetUpWeapon()
    {
        weaponName = weaponData.weaponName;
        weaponMaxLv = weaponData.weaponMaxLv;
        weaponBaseDamage = weaponData.attackDamage;
        baseAttackSpeed = weaponData.baseAttackSpeed;
        //Debug.Log("Da SEtupWeapon" + weaponName);
    }

    public string GetUpgradeInformation(int weaponCurrentLv)
    {
        
        return weaponData.weaponUpgradeInformation[weaponCurrentLv];
    }
}
