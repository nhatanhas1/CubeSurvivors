using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using static PlayerController;

public class PlayerController : MonoBehaviour, IIDamageable
{
    //[SerializeField] AudioSource audioSource;
    //ObjectPool objectPool;
    UIManager uiManager;
    GameHandler gameHandler;
    //public UpgradeManager upgradeManager;


    public bool isDead = false;
    public float takeDamageDelay = 0.5f;
    public float takeDamageTime = 0;

    public int playerExp;
    public int playerLevel = 1;
    int expToLevelUp;

    [SerializeField] TextMeshProUGUI levelUI;

    
    public int hitPoint;
    public int currentHitPoint;

    public int armor;
    int finalDamageTake;

    public float moveSpeed;
    public float attackSpeed;

    public float attackTime;

    [SerializeField] List<GameObject> activeWeaponList;
    [SerializeField] List<GameObject> AllWeaponList;
    //public List<UpgradePart> upgradePartList;

    public float pickupRadius;
    public int stoneLv;
    public int axeLv;
    public int crossLv;
    public int fireAuraLv;

    IWeapon IWeapon;
    IWeaponActive IWeapon_Active;

    Vector3 moveDir;
    public Vector3 faceDir;

    //[SerializeField] Transform objectSpawnPosition;
    float angel;

    Rigidbody rb;

    private Animator animator;


    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.Instance;
        gameHandler = GameHandler.Instance;
        SetUpStats();        
        //SetUpActiveWeapon();
        Attack();
        
        //audioSource.Play();
        //audioSource.Pause();
        //audioSource.UnPause();  
    }

    void SetUpStats()
    {
        playerExp = 0;
        playerLevel = 1;
        expToLevelUp = 50;
        moveSpeed = 3;
        isDead = false;
        hitPoint = 100;
        armor = 3;
        faceDir = Vector3.back;
        currentHitPoint = hitPoint;
        attackTime = 2;
        SetUpWeapon();
        uiManager.SetUpPlayerUI(hitPoint, currentHitPoint, expToLevelUp, playerExp, playerLevel);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimation();

        //Attack();

        //Debug.Log(moveDir);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void GetPlayerStatus()
    {        
        uiManager.GetPlayerStatusUi(pickupRadius,stoneLv,axeLv,crossLv,fireAuraLv);
    }

    void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");

            rb.velocity = moveDir * moveSpeed;
        }
        else
        {
            moveDir = Vector3.zero;
            rb.velocity = moveDir;
        }
    }

    void MoveAnimation()
    {         
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                faceDir = Vector3.left;
                //Debug.Log("FaceDir left" + faceDir);
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                faceDir = Vector3.right;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                faceDir = Vector3.forward;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                
                faceDir = Vector3.back;
                animator.SetInteger("Direction", 0);
            }
            angel = Mathf.Atan2(faceDir.x, faceDir.z) * Mathf.Rad2Deg - 90;
            //Debug.Log(angel);
        }
            
    }
    
    void SetUpWeapon()
    {
        if(AllWeaponList.Count > 0)
        {
            for(int i = 0; i < AllWeaponList.Count; i++)
            {
                IWeapon = AllWeaponList[i].GetComponent<IWeapon>();
                IWeapon.SetWeaponHandler(this);
                
            }
        }
    }

    public void SetUpActiveWeapon()
    {
        for (int i = 0; i < activeWeaponList.Count; i++)
        {
            IWeapon_Active = activeWeaponList[i].GetComponent<IWeaponActive>();
            IWeapon_Active.SetUpPlayerStats();
        }
    }

    void Attack()
    {
        //if(Time.time > attackTime)
        //{
        //    //Debug.Log("Player Attack");
        //    attackTime = Time.time + 2f;
        //    for(int i = 0; i < activeWeaponList.Count; i++)
        //    {
        //        IWeapon_Active = activeWeaponList[i].GetComponent<IWeaponActive>();
        //        IWeapon_Active.Attack();
        //    }            
        //}

        for (int i = 0; i < activeWeaponList.Count; i++)
        {
            IWeapon_Active = activeWeaponList[i].GetComponent<IWeaponActive>();
            IWeapon_Active.Attack();
        }

    }

    void StopAttack()
    {

    }


    public void TakeDamage(int damage)
    {
        if (!isDead && Time.time>takeDamageTime)
        {
            takeDamageTime = Time.time + takeDamageDelay;
            finalDamageTake = damage > armor ? (damage - armor) : 0;
            currentHitPoint -= finalDamageTake;
            uiManager.UpdateHealthBar(currentHitPoint);
            if (currentHitPoint <= 0)
            {
                Dead();
            }
                      
            //Debug.Log("Player Take Damage" + finalDamageTake);
        }        
    }

    public void GetExp(int exp)
    {
        AudioManager.Instance.Play(AudioManager.Sound.SoundName.GetExp);

        playerExp += exp;
        if(playerExp >= expToLevelUp)
        {
            gameHandler.LevelUpListener?.Invoke();
            LevelUp();
        }
        
        uiManager.UpdateExpBar(playerExp);
    }

    void LevelUp()
    {
        AudioManager.Instance.Play(AudioManager.Sound.SoundName.LevelUp);
        AudioManager.Instance.GetAudioSource(AudioManager.Sound.SoundName.Background).Pause();
        //audioSource.Pause();
        playerLevel++;
        expToLevelUp += 100;
        playerExp = 0;

        uiManager.PlayerLevelUp(playerExp, expToLevelUp, playerLevel);

        //Debug.Log(expToLevelUp);

    }

    
    public void Dead()
    {
        if (!isDead)
        {
            isDead = true;
            gameHandler.PlayerDeadListener?.Invoke();
            //Debug.Log("Player Dead");
        }
    }

}

public interface IWeapon {

    //public enum WeaponType
    //{
    //    Knife
    //}

    //public WeaponType Type { get; set; }

    public WeaponData WeaponData { get; set; }

    //public playerController.UpgradePart UpgradePart { get; set; }
    public int WeaponLv { get; set; }

    public int WeaponID { get; set; }
    public Sprite sprite { get; set; }
    public string WeaponName { get; set; }
    void SetWeaponHandler(PlayerController player);
    //void Attack();    
    void AddToUpgradeButton(UpgradeManager upgradeManager);

    void Upgrade();

}

public interface IWeaponActive
{
    void Attack();

    public void SetUpPlayerStats();
}

