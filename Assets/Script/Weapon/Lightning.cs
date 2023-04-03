using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : PlayerUpgradePower , IWeaponActive
{
    // Start is called before the first frame update

    //[SerializeField] PlayerController playerController;
    

    //[SerializeField] UpgradeManager upgradeButtonManager;

    //[SerializeField] WeaponData weaponData;
    //[SerializeField] private int weaponId;
    //[SerializeField] string weaponName;
    [SerializeField] int weaponLv;
    int maxUpgradeLevel = 7;

    //public WeaponData WeaponData { get => weaponData; set { weaponData = value; } }
    //public int WeaponID { get => weaponId; set { weaponId = value; } }
    //public string WeaponName { get => weaponName; set { weaponName = value; } }
    //public int WeaponLv { get => weaponLv; set { weaponLv = value; } }
    //public Sprite sprite { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    public bool activeWeapon;

    //float baseAttackSpeed;
    float finalAttackSpeed;


    float attackTime;
    float attackSpeed;
    int multiAttack;

    [SerializeField] List<GameObject> targetList;

    private void Awake()
    {
        //upgradeButtonManager = FindObjectOfType<UpgradeManager>();
    }

    void Start()
    {
        SetUpWeapon();
        activeWeapon = false;
        weaponId = 11;
        //weaponName = "Lighning";
        //weaponLv = 1;

        multiAttack = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

       
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IIDamageable>() != null)
        {
            //IIDamageable attackTarget = other.GetComponent<IIDamageable>();
            //attackTarget.TakeDamage(5f);
            //Debug.Log("Player attack ");
            GameObject target = other.gameObject;

            //targetList.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<IIDamageable>() != null)
        {
            GameObject target = other.gameObject;
            targetList.Remove(target);
        }
    }

    public void Attack()
    {
        //if(targetList != null)
        //{
        //    if(Time.time > attackTime)
        //    {
        //        int count = 1;
        //        while (true)
        //        {
        //            int targetIndex = Random.Range(0, targetList.Count - 1);

        //            if (targetIndex < targetList.Count)
        //            {
        //                attackTime = Time.time + attackSpeed;

        //                IIDamageable attackTarget = targetList[targetIndex].GetComponent<IIDamageable>();
        //                attackTarget.TakeDamage(25);

        //                if (!targetList[targetIndex].activeSelf)
        //                {
        //                    targetList.RemoveAt(targetIndex);
        //                }
        //                //Debug.Log("Player attack ");
        //            }
        //            if (count++ == multiAttack)
        //                break;
        //        }                
        //    }                      
        //}

        activeWeapon = true;
        StartCoroutine(WeaponAttack());
    }

    IEnumerator WeaponAttack()
    {
        while (activeWeapon)
        {
            if (targetList != null)
            {
                Debug.Log("Lighint attack");
                if (Time.time > attackTime)
                {
                    int count = 1;
                    while (true)
                    {
                        int targetIndex = Random.Range(0, targetList.Count - 1);

                        if (targetIndex < targetList.Count)
                        {
                            attackTime = Time.time + attackSpeed;

                            IIDamageable attackTarget = targetList[targetIndex].GetComponent<IIDamageable>();
                            attackTarget.TakeDamage(25);

                            if (!targetList[targetIndex].activeSelf)
                            {
                                targetList.RemoveAt(targetIndex);
                            }
                            //Debug.Log("Player attack ");
                        }
                        if (count++ == multiAttack)
                            break;
                    }
                }
            }
            yield return new WaitForSeconds(finalAttackSpeed);
        }
        yield return null;  
    }

    public void SetWeaponHandler(PlayerController player)
    {
        playerController = player;
    }

    public void SetUpPlayerStats()
    {
        if (playerController != null)
        {
            finalAttackSpeed = baseAttackSpeed * ((100 - playerController.attackSpeed) / 100);
        }
        else
        {
            playerController = GetComponentInParent<PlayerController>();
        }
    }

    public void AddToUpgradeButton()
    {
        upgradeButtonManager = FindObjectOfType<UpgradeManager>();
        if (weaponLv <= maxUpgradeLevel)
        {
            upgradeButtonManager.currentUpgradePartList.Add(WeaponData.weaponType);
        }

    }

    public void Upgrade()
    {

    }
}
