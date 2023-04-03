using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRadius : PlayerUpgradePower
{
    public float pickupRadius;
    [SerializeField] SphereCollider pickupCollider;

    private void Awake()
    {
        //playerController = GetComponentInParent<PlayerController>();
        if(pickupCollider != null)
        {
            pickupCollider = GetComponent<SphereCollider>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();
        //weaponId = 10;

        pickupCollider.radius = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ExpGem>() != null)
        {
            if(playerController == null) { return; }
            //ExpGem expGem = other.GetComponent<ExpGem>();
            //playerController.playerExp += other.GetComponent<ExpGem>().exp;
            playerController.GetExp(other.GetComponent<ExpGem>().exp);
            ObjectPool.Instance.ReturnObjectToPool("ExpGem", other.gameObject);
            //other.gameObject.SetActive(false);
        }
    }

    public void SetWeaponHandler(PlayerController player)
    {
        Debug.Log("Pickup Setup Hanler");
        playerController = player;
        playerController.pickupRadius = pickupCollider.radius;
    }

   

    //public void AddToUpgradeButton()
    //{
    //    upgradeButtonManager = FindObjectOfType<UpgradeManager>();
    //    if (weaponCureentLv <= maxUpgradeLevel)
    //    {
    //        upgradeButtonManager.currentUpgradePartList.Add(PlayerController.UpgradePart.PickUp);
    //    }

    //}

    public override void Upgrade()
    {
        Debug.Log("upgrade Pickup");
        weaponCurrentLv++;
        switch (weaponCurrentLv)
        {
            case 1:
                pickupCollider.radius += 1;
                
                break;
            case 2:
                pickupCollider.radius += 1;


                break;
            case 3:
                pickupCollider.radius += 1;


                break;
            case 4:
                pickupCollider.radius += 1;

                break;
            case 5:
                pickupCollider.radius += 1;


                break;
            case 6:
                pickupCollider.radius += 1;


                break;
            case 7:
                pickupCollider.radius += 1;

                break;

        }
        
        playerController.pickupRadius = pickupCollider.radius;
    }
}
