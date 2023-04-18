using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    Timer timer;
    GameHandler gameHandler;

    ObjectPool objectPool;
    
    GameObject spawnObject;
    public List<EnemyData> enemyDataList;
    EnemyController enemyController;

    public PlayerController playerController;


    float offSetPosition = 5f;
    public Transform spawnLocation;
    [SerializeField] List<ObjectSpawnController> spawnController;
    public Vector3 spawnPosition;

    public float spawnDelay = 1;
    public float spawnTime;

    int enemyCount;
    int enemyType;
    public int enemyDeadCount;


    int corountine;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Start()
    {
        gameHandler = GameHandler.Instance;
        enemyDeadCount = 0;
        spawnTime = 0;
        spawnDelay = 2;
        objectPool = ObjectPool.Instance;

        playerController.playerLevel = 1;
        LevelRule();

        corountine = 0;
        //StartCoroutine(SpawnEnemyFromLocation(0));
        StartCoroutine(CheckTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheckTimer()
    {
        float tmp = 10;
        while (!playerController.isDead)
        {
            //Debug.Log("Call check time");            
            //if(timer.minute >= tmp)
            //{
            //    tmp = timer.minute + 1;
            //    gameHandler.SpawnBreakableObject();
            //}

            //if (timer.second >= tmp)
            //{
            //    tmp = timer.minute + 1;
            //    gameHandler.SpawnBreakableObject();
            //}

            gameHandler.SpawnBreakableObject();
            yield return new WaitForSeconds(tmp);
        }

        
    }

    //Corutine Timer
    //Giu lai tham khao
    //IEnumerator SpawnEnemyFromLocation(int enemyType)
    //{
    //    //corountine ++;
    //    //Debug.Log("Dem coroutine: " + corountine);
    //    while (!playerController.isDead)
    //    {
    //        //if (!spawnEnemy) { yield break; }
    //        if(spawnTime > spawnDelay)
    //        {
    //            spawnTime = 0;
    //            for (int i = 0; i < 3; i++)
    //            {
    //                spawnController[i].SpawnEnemy(enemyDataList[enemyType]);
    //            }
    //        }

    //        spawnTime += Time.deltaTime;

    //        if (!spawnEnemy)
    //        {
    //            Debug.Log("Da toat Coroutine " + corountine);
    //            yield break;
    //        }

    //        yield return null;  
    //    }        
    //}


    void SpawnEnemyRandomPosition()
    {
        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + spawnDelay;

            float randomNumber = Random.Range(-6f, 6f);
            offSetPosition = 5f;
            spawnPosition.x = randomNumber > 0 ? spawnLocation.position.x + (offSetPosition + randomNumber) : spawnLocation.position.x + (-offSetPosition - randomNumber);

            spawnPosition.z = randomNumber > 0 ? spawnLocation.position.z + (offSetPosition + randomNumber) : spawnLocation.position.z + (-offSetPosition - randomNumber);


            spawnObject = objectPool.SpawnObject("Enemy", spawnPosition, spawnLocation.rotation);
            if (spawnObject != null)
            {
                if (spawnObject.GetComponent<EnemyController>() != null)
                {
                    enemyController = spawnObject.GetComponent<EnemyController>();
                    LevelRule();
                    enemyController.enemyData = enemyDataList[enemyType];
                    spawnObject.SetActive(true);
                }
            }
        }
    }

    public void LevelRule()
    {
        //Debug.Log(playerController.playerLevel);
        if (enemyDataList.Count > 0)
        {
            switch (playerController.playerLevel)
            {
                case 1:
                    //enemyType = 0;
                    //Debug.Log("chon case1");
                    //StartCoroutine(SpawnEnemyFromLocation(0));
                    for (int i = 0; i < 8; i++)
                    {
                        //Debug.Log("SpawnLocation " + i);
                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 4;
                        spawnController[i].enemyData = enemyDataList[0];
                        spawnController[i].spawnEnemy = true;
                    }
                    break;
                case 2:
                    //enemyType = 3;
                    //Debug.Log("chon case2");
                    for (int i = 8; i < 16; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 6;
                        spawnController[i].enemyData = enemyDataList[3];
                        spawnController[i].spawnEnemy = true;
                    }
                    //spawnEnemy = true;
                    break;
                case 3:
                    //enemyType = 3;
                    for (int i = 0; i < 8; i++)
                    {
                        //Debug.Log("SpawnLocation " + i);
                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 5;
                        spawnController[i].enemyData = enemyDataList[0];
                        spawnController[i].spawnEnemy = true;
                    }

                    for (int i = 8; i < 12; i++)
                    {
                        //Debug.Log("SpawnLocation " + i);
                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 6;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    //enemyType = 4;
                    for (int i = 12; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 7;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }

                    break;
                case 4:
                    //enemyType = 2;

                    for (int i = 0; i < 12; i++)
                    {
                        //Debug.Log("SpawnLocation " + i);
                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 5.5f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    //enemyType = 4;
                    for (int i = 12; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 7f;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }

                    break;
                case 5:

                    for (int i = 0; i < 6; i++)
                    {
                        //Debug.Log("SpawnLocation " + i);
                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 5;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 6; i < 10; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 7;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }
                    //enemyType = 4;                    
                    for (int i = 10; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 10;
                        spawnController[i].enemyData = enemyDataList[3];
                        spawnController[i].spawnEnemy = true;
                    }
                    break;
                case 7:
                    for (int i = 0; i < 6; i++)
                    {
                        //Debug.Log("SpawnLocation " + i);
                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 5;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 6; i < 12; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 6;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 12; i < spawnController.Count; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 3.5f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }

                    break;
                case 9:
                    for (int i = 0; i < spawnController.Count; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 3f;
                        spawnController[i].enemyData = enemyDataList[1];
                        spawnController[i].spawnEnemy = true;
                    }

                    break;
                case 10:
                    for (int i = 0; i < 10; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 3f;
                        spawnController[i].enemyData = enemyDataList[1];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 10; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 6;
                        spawnController[i].enemyData = enemyDataList[4];
                        spawnController[i].spawnEnemy = true;
                    }
                    break;

                case 11:
                    for (int i = 0; i < 6; i++)
                    {

                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 3f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 6; i < 12; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 4f;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 12; i < spawnController.Count; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 3.5f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    break;
                case 14:
                    for (int i = 0; i < 8; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 5f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 8; i < 12; i++)
                    {

                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 2.5f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 12; i < 16; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 3f;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }

                    break;
                case 16:
                    for (int i = 0; i < 8; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 3f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 8; i < 16; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 3.5f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }

                    break;
                case 18:
                    for (int i = 0; i < 4; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 4; i < 8; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 8; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 6f;
                        spawnController[i].enemyData = enemyDataList[5];
                        spawnController[i].spawnEnemy = true;
                    }
                    break;
                case 19:
                    for (int i = 0; i < 6; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 2f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 6; i < 8; i++)
                    {

                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }

                    for (int i = 8; i < 10; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 10; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 6f;
                        spawnController[i].enemyData = enemyDataList[5];
                        spawnController[i].spawnEnemy = true;
                    }
                    break;
                case 20:
                    for (int i = 0; i < 8; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 2.5f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 8; i < 12; i++)
                    {

                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 2f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }

                    for (int i = 12; i < 16; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 2.5f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    /*for (int i = 10; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 8f;
                        spawnController[i].enemyData = enemyDataList[5];
                        spawnController[i].spawnEnemy = true;
                    }*/
                    break;
                case 22:
                    for (int i = 0; i < 8; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 2.5f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 8; i < 12; i++)
                    {

                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }

                    for (int i = 12; i < 16; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 2f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    /*for (int i = 10; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 8f;
                        spawnController[i].enemyData = enemyDataList[5];
                        spawnController[i].spawnEnemy = true;
                    }*/
                    break;
                case 24:
                    for (int i = 0; i < 6; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 2f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }


                    for (int i = 6; i < 10; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 10; i < 16; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 5f;
                        spawnController[i].enemyData = enemyDataList[5];
                        spawnController[i].spawnEnemy = true;
                    }
                    break;
                case 25:
                    for (int i = 0; i < 6; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }

                    for (int i = 6; i < 10; i++)
                    {

                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 10; i < 14; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 2f;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 14; i < 16; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 1f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    /*for (int i = 10; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 8f;
                        spawnController[i].enemyData = enemyDataList[5];
                        spawnController[i].spawnEnemy = true;
                    }*/
                    break;
                case 27:
                    for (int i = 0; i < 8; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }

                    for (int i = 8; i < 12; i++)
                    {

                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 12; i < 14; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 1f;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 14; i < 16; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 1f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    /*for (int i = 10; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 8f;
                        spawnController[i].enemyData = enemyDataList[5];
                        spawnController[i].spawnEnemy = true;
                    }*/
                    break;
                case 30:
                    for (int i = 0; i < 8; i++)
                    {

                        spawnController[i].objectType = "Slime";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[2];
                        spawnController[i].spawnEnemy = true;
                    }

                    for (int i = 8; i < 12; i++)
                    {

                        spawnController[i].objectType = "Barrel";
                        spawnController[i].spawnDelay = 1.5f;
                        spawnController[i].enemyData = enemyDataList[7];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 12; i < 14; i++)
                    {
                        spawnController[i].objectType = "Treant";
                        spawnController[i].spawnDelay = 1f;
                        spawnController[i].enemyData = enemyDataList[8];
                        spawnController[i].spawnEnemy = true;
                    }
                    for (int i = 14; i < 16; i++)
                    {

                        spawnController[i].objectType = "Flower";
                        spawnController[i].spawnDelay = 0.8f;
                        spawnController[i].enemyData = enemyDataList[6];
                        spawnController[i].spawnEnemy = true;
                    }
                    /*for (int i = 10; i < spawnController.Count; i++)
                    {
                        spawnController[i].objectType = "Bat";
                        spawnController[i].spawnDelay = 8f;
                        spawnController[i].enemyData = enemyDataList[5];
                        spawnController[i].spawnEnemy = true;
                    }*/
                    break;

                case 35:

                    break;
                case 40:

                    break;
                case 45:

                    break;
                case 50:

                    break;


                    //default:

                    //break;

            }
        }
    }


    public void CountEnemyDead(EnemyController enemy)
    {
        enemyDeadCount++;
        UIManager.Instance.UpdateEnemyDeadCountUI(enemyDeadCount);
    }

    
}
