using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BreakableObject : MonoBehaviour , IIDamageable
{
    [SerializeField] GameObject breakaboutObject;
    GameHandler gameHandler;
    ObjectPool objectPool;

    public int hitPoint = 10;
    public int currentHitPoint;

    public bool isDead  = false;
    PlayerController playerController;

    private void Awake()
    {
        gameHandler = FindObjectOfType<GameHandler>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameHandler.SpawnBreakableObjectListener.AddListener(RespawnObject);
        //gameHandler.SpawnBreakableObjectListener 
        objectPool = ObjectPool.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        isDead = false; 
        currentHitPoint = hitPoint;
        
    }

    public void TakeDamage(int damage)
    {
        
        if (isDead) { return; }
        //Debug.Log("BreakableObject Take Damage");
        currentHitPoint -= damage;
        if (currentHitPoint <=0)
        {
            Dead();
        }
        //throw new System.NotImplementedException();
    }

    void Dead()
    {
        if (isDead) { return; }
        isDead = true;
        GameObject spawnObject = objectPool.SpawnObject("ExpGem", this.transform.position, this.transform.rotation);
        spawnObject.SetActive(true);
        gameObject.SetActive(false);
        breakaboutObject.SetActive(false);
        
        
    }

    void RespawnObject()
    {
        //Debug.Log("Event Call");
        if (breakaboutObject.activeSelf) { return; }
        float dist = Vector3.Distance(playerController.transform.position, transform.position);
        if (dist > 20)
        {
            //Debug.Log("BreakableObject respawn object");
            breakaboutObject.SetActive(true);
            gameObject.SetActive(true);
        }
       
    }


    private void OnDestroy()
    {
        gameHandler.SpawnBreakableObjectListener.RemoveListener(RespawnObject);
    }
}
