using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   
    PlayerController playerController;
    [SerializeField] UpgradeManager upgradeManager;
    GameHandler gameHandler;
    ObjectPool objectPool;


    [SerializeField] SliderBar HealthBar;
    [SerializeField] SliderBar ExpBar;
    [SerializeField] TextMeshProUGUI levelUI;
    [SerializeField] TextMeshProUGUI enemyDeadCountUI;
    [SerializeField] TextMeshProUGUI timerUI;

    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject upgradePanel;

    [SerializeField] GameObject subMenuPanel;
    [SerializeField] TextMeshProUGUI playerHpUI;
    [SerializeField] TextMeshProUGUI playerArmorUI;
    [SerializeField] TextMeshProUGUI playerMoveSpeedUI;
    [SerializeField] TextMeshProUGUI playerAttackSpeedUI;
    [SerializeField] TextMeshProUGUI playerPickupRadiusUI;
    [SerializeField] TextMeshProUGUI StoneLvUI;
    [SerializeField] TextMeshProUGUI AxeLvUI;
    [SerializeField] TextMeshProUGUI CrossLvUI;
    [SerializeField] TextMeshProUGUI FireAuraLvUI;




    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
        playerController = FindAnyObjectByType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameHandler.Instance;
        objectPool = ObjectPool.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TurnOnSubMenu();
        }
    }

    public void SetUpPlayerUI(int maxHealth,int currentHitPoint ,int expToLevelUp ,int playerExp ,int playerLevel)
    {
        //HealthBar.SetUpBar(maxHealth, currentHitPoint);
        SetUpHeathBar(maxHealth,currentHitPoint);
        ExpBar.SetUpBar(expToLevelUp, playerExp);
        levelUI.text = $"{playerLevel}";
        
    }

    public void SetUpHeathBar(int maxHealth, int currentHitPoint)
    {
        HealthBar.SetUpBar(maxHealth, currentHitPoint);
    }

    public void UpdateHealthBar(int currenHitPoint)
    {
        HealthBar.SetBar(currenHitPoint);
    }

    public void UpdateExpBar(int playerExp)
    {
        ExpBar.SetBar(playerExp);
    }

    public void PlayerLevelUp(int playerExp, int expToLevelUp, int playerLevel)
    {
        ExpBar.SetUpBar(expToLevelUp, playerExp);
        levelUI.text = $"{playerLevel}";
        
        TurnOnUpgradePanel();

    }

    public void TurnOnUpgradePanel()
    {
        gameHandler.Pause();
        upgradePanel.SetActive(true);
        upgradeManager.SetupUpgradeButtonList();
    }

    public void TurnOffUpgradePanel()
    {
        gameHandler.Resume();
        upgradePanel.SetActive(false);
    }

    public void UpdateEnemyDeadCountUI(int enemyDeadCount)
    {
        enemyDeadCountUI.text = $"Kill: {enemyDeadCount}";
    }

    public void GameOverUI()
    {
        gameOver.gameObject.SetActive(true);
        AudioManager.Instance.GetAudioSource(AudioManager.Sound.SoundName.Background).Pause();
        AudioManager.Instance.Play(AudioManager.Sound.SoundName.GameOver);

    }

    public void TryAgainButtonClicked(int sceneIndex)
    { 
        //Debug.Log("Start Button Clicked");
        Loader.Load(sceneIndex);
    }


    public void TurnOnSubMenu()
    {
        gameHandler.Pause();
        subMenuPanel.SetActive(true);
        playerController.GetPlayerStatus();
    }

    public void GetPlayerStatusUi(float pickupRadius, int stoneLV, int axeLv, int crossLv, int FireAuraLv)
    {
        playerHpUI.text = $"HP:  {playerController.currentHitPoint}/{playerController.hitPoint}";
        playerArmorUI.text = $"Armor: {playerController.armor}";
        playerAttackSpeedUI.text = $"ASPD: {playerController.attackSpeed}";
        playerMoveSpeedUI.text = $"MoveSpeed: {playerController.moveSpeed}";
        playerPickupRadiusUI.text = $"PickupRadius: {pickupRadius}";

        StoneLvUI.text = $"LV: {stoneLV}";
        AxeLvUI.text = $"LV: {axeLv}";
        CrossLvUI.text = $"LV: {crossLv}";
        FireAuraLvUI.text = $"LV: {FireAuraLv}";

    }

    public void StartScene()
    {
        Debug.Log("asd");
        SceneManager.LoadScene(0);
    }

    public void ResummeButton()
    {
        Debug.Log("Test");
        gameHandler.Resume();
        subMenuPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
