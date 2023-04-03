using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour 
{
    // Start is called before the first frame update
    ObjectPool objectPool;
    public string poolType;

    private Vector3 shootDir;
    public int bulletDamage = 15;
    
    [SerializeField] private float timer;
    public float duration;
    public float moveSpeed;
    void Start()
    {
        objectPool = ObjectPool.Instance;
        duration = 1.5f;        
        poolType = "Bullet";
        moveSpeed = 20;
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position += shootDir * moveSpeed * Time.deltaTime;
        //Debug.Log("BUllet dd");        
        ObjectDuration();

    }

    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
        
    }

    public void Setup(Vector3 shootDir,int damage)
    {
        bulletDamage = damage;
        this.shootDir = shootDir;
    }

    
    void ObjectDuration()
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
        objectPool.ReturnObjectToPool(poolType, this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IIDamageable>() != null)
        {
            IIDamageable damageable = other.GetComponent<IIDamageable>();
            damageable.TakeDamage(bulletDamage);
        }        
    }
}
