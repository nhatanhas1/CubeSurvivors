using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnObjectManager : MonoBehaviour
{

    GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        GameHandler.Instance.EnemyDeadListener.AddListener(SpawnItemsWhenEnemyDead);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnItemsWhenEnemyDead(EnemyController enemy)
    {
        Debug.Log("Span EXp Gem");
        spawnObject = ObjectPool.Instance.SpawnObject("ExpGem", enemy.transform.position, enemy.transform.rotation);
        if (spawnObject != null)
        {
            spawnObject.gameObject.SetActive(true);
        }
    }
}
