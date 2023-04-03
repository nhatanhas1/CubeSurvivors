using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;

    ObjectPool objectpool;
    public string poolType;

    [SerializeField] private float timer;
    [SerializeField] private float duration;
  

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        duration = 5;
        objectpool = ObjectPool.Instance;
        poolType = "Bullet";
        //rb.velocity = Vector3.right;
        //Debug.Log("Bullet start");
    }

    private void OnEnable()
    {
        rb.velocity = Vector3.right;
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > duration)
        {
            timer = 0;
            ReturnObject();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void ReturnObject()
    {
        objectpool.ReturnObjectToPool(poolType,this.gameObject);
    }
}
