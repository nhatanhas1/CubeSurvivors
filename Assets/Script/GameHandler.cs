using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameHandler : MonoBehaviour
{

    GameData gameData;
    string path;

    [SerializeField] Timer timer;
    [SerializeField] LevelController levelController;
    PlayerController playerController;

    public UnityEvent SpawnBreakableObjectListener;

    //[SerializeField] UnityEvent PauseGameListener;
    public UnityEvent PauseGameListener;

    public UnityEvent LevelUpListener;

    //public UnityEvent UpgradeButtonListener;
    //public UnityEvent PlayerUpgradeListener;

    public UnityEvent EnemyDeadListener;
    public UnityEvent PlayerDeadListener;
    
    bool gameIsPause;

    //public  float minutePlayTime;
    //public float secondPlayTime;
    

    // Start is called before the first frame update
    public static GameHandler Instance { get; private set; }

    private void Awake()
    {
        timer = FindAnyObjectByType<Timer>();
        levelController = FindAnyObjectByType<LevelController>();

        path = Application.persistentDataPath + "/Player.json";
        //Debug.Log(path);
        Instance = this;    
    }

    void Start()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.Play(AudioManager.Sound.SoundName.Background);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        gameIsPause = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        gameIsPause = false;
        Time.timeScale = 1f;
    }

    public void PlayerUpgrade()
    {
        //PlayerUpgradeListener?.Invoke();
    }

    
    public void GameOver()
    {
        StartCoroutine(OnGameOver());
    }

    IEnumerator OnGameOver()
    {
        //Debug.Log("GameOver");
        yield return new WaitForSecondsRealtime(1);
        //Resume();
        //SceneManager.LoadScene(0);
        SaveGame();
    }

    public void SaveGame()
    {
        gameData = new GameData();
        gameData.minute = timer.minute;
        gameData.second = timer.second;
        gameData.killCount = levelController.enemyDeadCount;
        //gameData.gameDataList.Add(1, gameData);


        //GameDatalist gameDataList = new GameDatalist();
        //GameDatalist.gameDataArray.Add(gameData);

        //SaveLoadGame.LoadDataJson(out GameDatalist gameDatalist, path);

        SaveLoadGame.SaveDataJson(gameData, path);
    }

    
    public void SpawnBreakableObject()
    {
        SpawnBreakableObjectListener?.Invoke();
    }
}


