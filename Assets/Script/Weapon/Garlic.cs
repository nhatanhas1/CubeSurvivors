using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Garlic : PlayerUpgradePower , IWeaponActive
{
    //[SerializeField] PlayerController playerController;   
    

    //[SerializeField] UpgradeManager upgradeButtonManager;

    //[SerializeField] WeaponData weaponData;
    //[SerializeField] private int weaponId;
    //[SerializeField] string weaponName;
    //[SerializeField] int weaponLv;
    //int maxUpgradeLevel = 7;

    //public WeaponData WeaponData { get => weaponData; set { weaponData = value; } }
    //public int WeaponID { get => weaponId; set { weaponId = value; } }
    //public string WeaponName { get => weaponName; set { weaponName = value; } }
    //public int WeaponLv { get => weaponLv; set { weaponLv = value; } }
    //public Sprite sprite { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField] ParticleSystem Effect;

    public bool activeWeapon;
    //int fireAuraDamage;
    //[SerializeField] float baseAttackSpeed;
    [SerializeField] float finalAttackSpeed;


    [SerializeField] List<GameObject> targetList;

    private void Awake()
    {
        //upgradeButtonManager = FindObjectOfType<UpgradeManager>();
        playerController = GetComponentInParent<PlayerController>();

    }

    void Start()
    {
        SetUpWeapon();
        weaponId = 3;        
        //weaponName = "Garlic";
        weaponCurrentLv = 0;
        //fireAuraDamage = 10;
        //baseAttackSpeed = 1.5f;
        SetUpPlayerStats();


        playerController.fireAuraLv = weaponCurrentLv;
        TurnOnEffect();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Attack();
    }

    void TurnOnEffect()
    {
        Effect.Play();
        //Debug.Log("Player Effect");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IIDamageable>() != null)
        {
            //IIDamageable attackTarget = other.GetComponent<IIDamageable>();
            //attackTarget.TakeDamage(5f);
            //Debug.Log("Player attack ");
            GameObject target = other.gameObject;

            targetList.Add(target);
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
        //    AudioManager.Instance.Play(AudioManager.Sound.SoundName.FireAttack);

        //    for (int i = 0; i < targetList.Count; i++)
        //    {
        //        IIDamageable attackTarget = targetList[i].GetComponent<IIDamageable>();
        //        attackTarget.TakeDamage(fireAuraDamage);
        //        //Debug.Log("Player attack ");
        //    }
        //}

        activeWeapon = true;
        StartCoroutine(WeaponAttack());
    }

    IEnumerator WeaponAttack()
    {
        while (activeWeapon)
        {
            AudioManager.Instance.Play(AudioManager.Sound.SoundName.FireAttack);
            if (targetList != null)
            {                
                //for (int i = 0; i < targetList.Count; i++)
                //{
                //    IIDamageable attackTarget = targetList[i].GetComponent<IIDamageable>();
                //    attackTarget.TakeDamage(fireAuraDamage);
                //    //Debug.Log("Player attack ");
                //}
                for (int i = targetList.Count - 1; i > -1; i--)
                {
                    if (!targetList[i].activeSelf)
                    {
                        targetList.Remove(targetList[i]);
                        //if (targetList.Count == 0) { return null; }
                    }
                    else
                    {
                        IIDamageable attackTarget = targetList[i].GetComponent<IIDamageable>();
                        attackTarget.TakeDamage(weaponBaseDamage);
                    }
                    
                }
            }
            yield return new WaitForSeconds(finalAttackSpeed);
        }

    }


    public override void Upgrade()
    {
        Debug.Log("Upgrade Cross");
        weaponCurrentLv++;
        switch (weaponCurrentLv)
        {
            case 1:
                weaponBaseDamage += 5;

                //spawnLocation[1].gameObject.SetActive(true);
                break;
            case 2:
                weaponBaseDamage += 5;

                break;
            case 3:
                weaponBaseDamage += 6;

                //spawnLocation[2].gameObject.SetActive(true);

                break;
            case 4:

                weaponBaseDamage += 6;

                break;
            case 5:

                weaponBaseDamage += 7;

                break;
            case 6:

                weaponBaseDamage += 7;

                //spawnLocation[3].gameObject.SetActive(true);
                break;
            case 7:

                weaponBaseDamage += 12;

                break;

        }
        
        playerController.fireAuraLv = weaponCurrentLv;
    }
}
