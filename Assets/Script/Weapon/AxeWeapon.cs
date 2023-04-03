using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AxeWeapon : PlayerUpgradePower, IWeaponActive
{
    ObjectPool objectPool;
    public bool activeWeapon;
    public int maxHitCount;
    public int axeDamage;

    //[SerializeField] float baseAttackSpeed;
    [SerializeField] float finalAttackSpeed;


    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();                                                                                                         
        //upgradeButtonManager = FindObjectOfType<UpgradeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();

        weaponId = 2;

        maxHitCount = 1;

        weaponCurrentLv = 0;


        SetUpPlayerStats();

        objectPool = ObjectPool.Instance;
        playerController.axeLv = weaponCurrentLv;

        Physics.gravity = new Vector3(0, 0, -40f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetWeaponHandler(PlayerController player)
    {        
        playerController = player;
    }

    public void SetUpPlayerStats()
    {
        if(playerController != null)
        {
            finalAttackSpeed = baseAttackSpeed * ((100 - playerController.attackSpeed) / 100);
        }
        else
        {
            playerController = GetComponentInParent<PlayerController>();
        }        
    }

    public void Attack()
    {
        activeWeapon = true;
        StartCoroutine(WeaponAttack());
        //Debug.Log("Axe test");
    }

    IEnumerator WeaponAttack()
    {
        while (activeWeapon)
        {
            AudioManager.Instance.Play(AudioManager.Sound.SoundName.AxeAttack);
            for (int i = 0; i < maxHitCount; i++)
            {
                Vector3 direction = new Vector3(0, 0, 0);
                
                direction.x = Random.Range(playerController.transform.position.x - 0.3f, playerController.transform.position.x + 0.3f);
                direction.z = playerController.transform.position.z + 1f;

                GameObject axe = objectPool.SpawnObject("Axe", transform.position, Quaternion.identity);
                axe.GetComponent<Axe>().currentAxeDamage = weaponBaseDamage;
                axe.GetComponent<Axe>().Setup((direction - playerController.transform.position).normalized);
                axe.SetActive(true);
                //Debug.Log("Axe Test22");
            }
            yield return new WaitForSeconds(finalAttackSpeed);
        }
        
    }

    public override void Upgrade()
    {
        weaponCurrentLv++;
        Debug.Log("Upgrade Axe");
        switch (weaponCurrentLv)
        {
            case 1:
                weaponBaseDamage += 10;
                maxHitCount++;
                //spawnLocation[1].gameObject.SetActive(true);
                break;
            case 2:
                weaponBaseDamage += 10;

                break;
            case 3:
                weaponBaseDamage += 15;
                maxHitCount++;
                //spawnLocation[2].gameObject.SetActive(true);

                break;
            case 4:
                weaponBaseDamage += 15;


                break;
            case 5:
                weaponBaseDamage += 20;


                break;
            case 6:
                weaponBaseDamage += 20;
                maxHitCount++;
                break;
            case 7:
                weaponBaseDamage += 40;

                break;

        }
        
        playerController.axeLv = weaponCurrentLv;
    }

}
