using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Axe : MonoBehaviour 
{
    // Start is called before the first frame update
    
    ObjectPool objectPool;
    public string poolType;

    private Vector3 shootDir;

    public  List<Transform> spawnLocation;


    public int currentAxeDamage;

    [SerializeField] private float timer;
    [SerializeField] private float duration;
    [SerializeField] float velocity;


    bool isFirstUpdate = true;
    private void Awake()
    {
       
    }
    void Start()
    {
        objectPool = ObjectPool.Instance;
        duration = 1.5f;        
        poolType = "Axe";
        velocity = 20;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstUpdate)
        {
            transform.GetComponent<Rigidbody>().velocity = shootDir * velocity;
            //Debug.Log(shootDir);
            isFirstUpdate = false;
        }
        transform.Rotate(0, 720f * Time.deltaTime, 0, Space.Self);
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
        isFirstUpdate = true;
        objectPool.ReturnObjectToPool(poolType, this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IIDamageable>() != null)
        {
            IIDamageable damageable = other.GetComponent<IIDamageable>();


            damageable.TakeDamage(currentAxeDamage);

        }        
    }
}
