using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Knife : PlayerUpgradePower , IWeaponActive
{
    ObjectPool objectPool;

    public bool activeWeapon;

    public int knifeDamage;
    //[SerializeField] float baseAttackSpeed;
    [SerializeField] float finalAttackSpeed;

    [SerializeField] List<Transform> spawnLocation;

    private void Awake()
    {
        //upgradeButtonManager = FindObjectOfType<UpgradeManager>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapon();
        //activeWeapon = false;
        weaponId = 0;
        //weaponName = "Knife";
        //knifeDamage = 5;
        //baseAttackSpeed = 1;
        SetUpPlayerStats();

        objectPool = ObjectPool.Instance;
        weaponCurrentLv = 0;
        playerController.stoneLv = weaponCurrentLv;
        //Type = IWeapon.WeaponType.Knife;
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
        if (playerController != null)
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
    }

    IEnumerator WeaponAttack()
    {
        while (activeWeapon)
        {
            AudioManager.Instance.Play(AudioManager.Sound.SoundName.KnifeAttack);

            for (int i = 0; i < spawnLocation.Count; i++)
            {
                if (spawnLocation[i].gameObject.active == true)
                {
                    GameObject bullet = objectPool.SpawnObject("Bullet", spawnLocation[i].transform.position, Quaternion.identity);
                    if (bullet != null)
                    {
                        bullet.GetComponent<Bullet>().Setup(playerController.faceDir, weaponBaseDamage);
                        bullet.SetActive(true);
                    }
                }
            }
            yield return new WaitForSeconds(finalAttackSpeed);
        }

    }

    public override void Upgrade()
    {
        Debug.Log("Knife Upgrade");
        weaponCurrentLv++;
        switch (weaponCurrentLv)
        {
            case 1:
                weaponBaseDamage += 5;
                spawnLocation[1].gameObject.SetActive(true);
                break;
            case 2:
                weaponBaseDamage += 5;
                spawnLocation[2].gameObject.SetActive(true);
                break;
            case 3:
                weaponBaseDamage += 7;
                spawnLocation[3].gameObject.SetActive(true);
                break;
            case 4:
                weaponBaseDamage += 7;
                spawnLocation[4].gameObject.SetActive(true);
                break;
            case 5:
                weaponBaseDamage += 9;
                spawnLocation[5].gameObject.SetActive(true);
                break;
            case 6:
                weaponBaseDamage += 9;
                //spawnLocation[6].gameObject.SetActive(true);
                break;
            case 7:
                weaponBaseDamage += 15;

                break;

        }
        
        playerController.stoneLv = weaponCurrentLv;
    }

}
