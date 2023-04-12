using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string poolType;
        public GameObject prefabInPool;
        public int poolSize;
    }


    public List<Pool> poolList = new List<Pool>();

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    
    //Singleton
    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        CreateObjectPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateObjectPool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //Duyet qua danh sach pool trong poolList
        foreach (Pool pool in poolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                //Instantiate(pool.prefabInPool).SetActive(false);

                pool.prefabInPool.SetActive(false);

                GameObject obj = Instantiate(pool.prefabInPool);
                obj.transform.SetParent(this.transform);
                //obj.SetActive(false);
                objectPool.Enqueue(obj);

            }

            poolDictionary.Add(pool.poolType, objectPool);
        }
    }


    public GameObject SpawnObject(string poolType,Vector3 spawnPosition, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolType) || poolDictionary[poolType].Count <= 0)
        {
            //Debug.Log("PoolType:" + poolType + "dont exist");
            //Debug.Log("Or Pool is emty");
            return null;
        }
        //Debug.Log("Dictionary: " + poolDictionary[poolType].Count);
        GameObject objectToSpawn = poolDictionary[poolType].Dequeue();
        //objectToSpawn.SetActive(false);

        objectToSpawn.transform.position = spawnPosition;
        objectToSpawn.transform.rotation = rotation;

        //objectToSpawn.SetActive(true);

        //poolDictionary[poolType].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public GameObject SpawnObjectSequentially(string poolType, Vector3 spawnPosition, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolType) || poolDictionary[poolType].Count <= 0)
        {
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[poolType].Dequeue();
        objectToSpawn.SetActive(false);

        objectToSpawn.transform.position = spawnPosition;
        objectToSpawn.transform.rotation = rotation;

        

        poolDictionary[poolType].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public void ReturnObjectToPool(string poolType, GameObject returnObject)
    {
        returnObject.SetActive(false);
        poolDictionary[poolType].Enqueue(returnObject);

    }

}



public interface IPooledObject
{
    void OnObjectSpawn();
}


