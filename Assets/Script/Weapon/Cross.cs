using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Cross : MonoBehaviour 
{
    // Start is called before the first frame update
    ObjectPool objectPool;
    public string poolType;

    private Vector3 shootDir;

    public  List<Transform> spawnLocation;

    public int currentCrossDamage;
    public int currentStartSpeed;
    [SerializeField] private float timer;
    [SerializeField] private float duration;
    [SerializeField] float startSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float moveSpeed;
    void Start()
    {
        objectPool = ObjectPool.Instance;
        duration = 2.2f;        
        poolType = "Cross";
        currentStartSpeed = 13;
        turnSpeed = 18;
        moveSpeed = currentStartSpeed;

    }

    // Update is called once per frame
    void Update()
    {        
        transform.position += shootDir * moveSpeed * Time.deltaTime;
        transform.Rotate(0f, 480f*Time.deltaTime, 0f, Space.Self);
        moveSpeed -= turnSpeed * Time.deltaTime;

        //Debug.Log("BUllet dd");        
        ObjectDuration();


    }

    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
        
    }

    public void Setup(Vector3 shootDir)
    {
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
        moveSpeed = currentStartSpeed;
        objectPool.ReturnObjectToPool(poolType, this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IIDamageable>() != null)
        {
            IIDamageable damageable = other.GetComponent<IIDamageable>();
            damageable.TakeDamage(currentCrossDamage);
        }        
    }
}
