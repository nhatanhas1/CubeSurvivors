using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
    ObjectPool objectPool;

    GameObject spawnObject;

    public bool spawnEnemy;

    [SerializeField] float spawnTime;
    public float spawnDelay;
    
    EnemyController enemyController;

    public EnemyData enemyData;
    public string objectType;


    private void Awake()
    {
        spawnEnemy = false;
    }
    // Start is called before the first frame update
    void Start()
    {        
        objectPool = ObjectPool.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObject();
        //SpawnObject2();
        //SpawnObject3();
    }

    public void SpawnObject()
    {
        if (!spawnEnemy) { return; }
        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + spawnDelay;
            spawnObject = objectPool.SpawnObject(objectType, this.transform.position, this.transform.rotation);
            if (spawnObject != null)
            {
                enemyController = spawnObject.GetComponent<EnemyController>();
                enemyController.enemyData = enemyData;
                spawnObject.SetActive(true);
                //spawnObject.SetActiveRecursively(true);

            }
        }
    }

    public void SpawnEnemy(EnemyData enemyData)
    {      
        spawnObject = objectPool.SpawnObject("Enemy", this.transform.position, this.transform.rotation);
        if (spawnObject != null)
        {
            enemyController = spawnObject.GetComponent<EnemyController>();
            enemyController.enemyData = enemyData;
            spawnObject.SetActive(true);
        }
    }


}
