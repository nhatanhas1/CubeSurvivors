using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour , IIDamageable , IPooledObject
{
    
    ObjectPool objectPool;
    GameHandler gameHandler;

    [SerializeField] Rigidbody rb;

    public EnemyData enemyData;
    public string poolType;

    public string nameEnemy;

    //Sprite sprite;

    bool isDead;
    public int hitPoint;
    public int currentHitPoint;

    public int armor;
    public int attackDamage;
    int finalDamageTake;

    public float moveSpeed;
    public float attackSpeed;
    public float attackTime;

    //public float timer;
    //public float lifeTime;

    GameObject spawnObject;
    public Transform target;

    NavMeshAgent navMeshAgent;

    //[SerializeField] bool checkOnMesh;

    [SerializeField] Animator enemyAnimator;
    [SerializeField] Transform reSpawn;
    //public RuntimeAnimatorController runtimeAnimatorController;

    private void Awake()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerController>().transform;
        rb = GetComponent<Rigidbody>();
           
    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.enabled = false;
        //poolType = "Enemy";
        gameHandler = GameHandler.Instance;
        objectPool = ObjectPool.Instance;

        SetUpStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    private void OnEnable()
    {
        attackTime = 0;       
        if (enemyData != null)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = enemyData.sprite;
            SetUpStats();
        }
        else { return; }
        StartCoroutine(UpdatePath());

    }

    private void OnDisable()
    {
        
    }

    void SetUpStats()
    {
        //enemyAnimator = GetComponentInChildren<Animator>();
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //target = FindObjectOfType<playerController>().transform;
        isDead = false;
        hitPoint = enemyData.hitPoint;
        armor = enemyData.armor;
        attackDamage = enemyData.attackDamage;
        attackSpeed = enemyData.attackSpeed;
        moveSpeed = enemyData.moveSpeed;
        navMeshAgent.speed = enemyData.moveSpeed;

        enemyAnimator.runtimeAnimatorController = enemyData.runtimeAnimatorController;

        currentHitPoint = hitPoint;
    }

    public void OnObjectSpawn()
    {

    }
   

    IEnumerator UpdatePath()
    {
        while (target != null)
        {
            //Debug.Log("test");
            NavMeshHit hit;
            if (NavMesh.SamplePosition(Vector3.zero, out hit, 50.0f, NavMesh.AllAreas))
            {
                Vector3 result = hit.position;
                navMeshAgent.enabled = true;
            }
            else
            {
                navMeshAgent.enabled = false;
            }
            if (navMeshAgent.isOnNavMesh)
            {
                

                //checkOnMesh = true;
                navMeshAgent.stoppingDistance = target.GetComponent<CapsuleCollider>().radius / 2;
                navMeshAgent.destination = target.position;
                if (navMeshAgent.hasPath)
                {
                    //Debug.Log("tim duoc duong");
                }
                else
                {

                    //Debug.Log("khong tim duoc duong");

                    //checkOnMesh = false;
                    //Debug.Log("cant move");
                    //Dead();
                    //objectPool.ReturnObjectToPool(poolType, this.gameObject);


                    //Vector3 direction = (target.position - transform.position).normalized;
                    //transform.position += direction * moveSpeed * Time.deltaTime;

                    //if ((target.position - transform.position).magnitude > 0.5f)
                    //{
                    //    Vector3 direction = (target.position - transform.position).normalized;
                    //    Vector3 newPosition = transform.position + (direction * Time.deltaTime * moveSpeed);
                    //    if (rb != null)
                    //    {
                    //        rb.MovePosition(newPosition);
                    //        Debug.Log("Chay bang Rb.MOvePosition " + newPosition);
                    //    }
                    //    else
                    //    {
                    //        rb = GetComponent<Rigidbody>();
                    //    }

                    //}
                }
            }
            //Debug.Log("Enemy Tim duong");
            
            yield return new WaitForSeconds(.5f);
        }

    }


    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            //Debug.Log("Enemy Take Damage");
            finalDamageTake = damage > armor ? (damage - armor) : 0;
            currentHitPoint -= finalDamageTake;
            PopupDamage();
            if (currentHitPoint <= 0)
            {
                Dead();
            }
        }
                
    }

    public void PopupDamage()
    {
        spawnObject = objectPool.SpawnObjectSequentially("DamagePopup", this.transform.position, transform.rotation);
        DamagePopup damagePopup = spawnObject.GetComponent<DamagePopup>();
        spawnObject.SetActive(true);
        damagePopup.Setup(finalDamageTake);
        //spawnObject.SetActive(true);
    }

    void Dead()
    {
        if (isDead) { return; }
        isDead = true;
        gameHandler.EnemyDeadListener?.Invoke(this);
        //spawnObject = objectPool.SpawnObject("ExpGem", this.transform.position, this.transform.rotation);
        //if(spawnObject != null)
        //{
        //    spawnObject.gameObject.SetActive(true);
        //}
        objectPool.ReturnObjectToPool(poolType, this.gameObject);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IIDamageable>() != null)
        {
            IIDamageable attackTarget = other.GetComponent<IIDamageable>();
            //GameObject attackTarget1 = other.gameObject;
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Attack(attackTarget);
            }
                        
        }
    }



    void Attack(IIDamageable target)
    {
        if (Time.time > attackTime)
        {
            attackTime = Time.time + attackSpeed;
            target.TakeDamage(attackDamage);
            //Debug.Log("Enemy Attack");
        }
    }
    
}
