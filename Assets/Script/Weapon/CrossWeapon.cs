using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static PlayerController;

public class CrossWeapon : PlayerUpgradePower , IWeaponActive
{
    ObjectPool objectPool;

    [SerializeField] FindClosest closestEnemy;

    Transform target;
    Vector3 targetPos;
    Vector3 shootDir;

    public bool activeWeapon;

    //public int crossDamage;
    public int startSpeed;
    public int maxHitCount =1;

    //float baseAttackSpeed;
    float finalAttackSpeed;

    //public UpgradePart upgradePart;


    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        playerController = GetComponentInParent<PlayerController>();
        closestEnemy = FindObjectOfType<FindClosest>();
        //upgradeButtonManager = FindObjectOfType<UpgradeManager>();
    }

    //public int currentHitCount;

    void Start()
    {
        SetUpWeapon();

        weaponId = 1;

        SetUpPlayerStats();

        maxHitCount = 1;
        startSpeed = 13;

        //weaponMaxLv = 0;
        
        shootDir = playerController.faceDir;
        objectPool = ObjectPool.Instance;

        weaponCurrentLv = 0;        
        playerController.crossLv = weaponCurrentLv;

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
        //Debug.Log("CrossWeapon Attack");
        //audioSource.PlayOneShot(audioSource.clip);
        //AudioManager.Instance.Play(AudioManager.Sound.SoundName.CrossAttack);
        //for (int i = 0; i < maxHitCount; i++)
        //{
        //    //Tang cong muc tieu gan nhat
        //    //if (closestEnemy.FindClosestEnemy() != null)
        //    //{
        //    //    shootDir = closestEnemy.FindClosestEnemy().transform.position - playerController.transform.position;

        //    //}
        //    //else
        //    //{
        //    //    shootDir = playerController.faceDir;
        //    //}


        //    //Tang cong random trong list muc tieu gan nhat
        //    closestEnemy.UppdateTargetList();
        //    if (closestEnemy.targetList.Count > 0)
        //    {
        //        target = closestEnemy.targetList[Random.Range(0, closestEnemy.targetList.Count)].transform;
        //        targetPos = new Vector3(target.position.x, 0, target.position.z);
        //        shootDir = (targetPos - playerController.transform.position).normalized;
        //        //Debug.Log(shootDir);
        //    }
        //    else
        //    {
        //        shootDir = playerController.faceDir;
        //    }

        //    GameObject cross = objectPool.SpawnObject("Cross", transform.position, Quaternion.identity);

        //    cross.GetComponent<Cross>().Setup(shootDir.normalized);
        //    cross.GetComponent<Cross>().currentCrossDamage = crossDamage;
        //    cross.GetComponent<Cross>().currentStartSpeed = startSpeed;
        //    cross.SetActive(true);
        //}        

        activeWeapon = true;
        StartCoroutine(WeaponAttack());
    }

    IEnumerator WeaponAttack()
    {
        while (activeWeapon)
        {
            AudioManager.Instance.Play(AudioManager.Sound.SoundName.CrossAttack);
            for (int i = 0; i < maxHitCount; i++)
            {
                //Tang cong random trong list muc tieu gan nhat
                closestEnemy.UppdateTargetList();
                if (closestEnemy.targetList.Count > 0)
                {
                    target = closestEnemy.targetList[Random.Range(0, closestEnemy.targetList.Count)].transform;
                    targetPos = new Vector3(target.position.x, 0, target.position.z);
                    shootDir = (targetPos - playerController.transform.position).normalized;
                    //Debug.Log(shootDir);
                }
                else
                {
                    shootDir = playerController.faceDir;
                }

                GameObject cross = objectPool.SpawnObject("Cross", transform.position, Quaternion.identity);

                cross.GetComponent<Cross>().Setup(shootDir.normalized);
                cross.GetComponent<Cross>().currentCrossDamage = weaponBaseDamage;
                cross.GetComponent<Cross>().currentStartSpeed = startSpeed;
                cross.SetActive(true);
            }
            yield return new WaitForSeconds(finalAttackSpeed);
        }

    }

    public override void Upgrade()
    {
        //Debug.Log("Upgrade Cross");
        weaponCurrentLv++;
        switch (weaponCurrentLv)
        {
            case 1:
                weaponBaseDamage += 5;
                maxHitCount++;
                break;
            case 2:
                weaponBaseDamage += 5;
                startSpeed += 1;
                break;
            case 3:
                weaponBaseDamage += 10;
                maxHitCount++;

                break;
            case 4:
                weaponBaseDamage += 10;
                startSpeed += 1;
                break;
            case 5:
                weaponBaseDamage += 15;
                break;
            case 6:
                weaponBaseDamage += 15;
                maxHitCount++;
                //startSpeed += 1;
                //spawnLocation[3].gameObject.SetActive(true);
                break;
            case 7:
                weaponBaseDamage += 25;
                startSpeed += 1;

                break;

        }
        
        playerController.crossLv = weaponCurrentLv;
    }

}
