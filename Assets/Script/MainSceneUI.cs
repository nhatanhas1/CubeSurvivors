using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class MainSceneUI : MonoBehaviour
{
    string path;
    GameData gameData;
    GameDatalist gameDatalist;

    [SerializeField] List<ScorePanel> scorePanels;
    
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject scroceboard;
    private void Awake()
    {
        path = Application.persistentDataPath + "/Player.json";
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play(AudioManager.Sound.SoundName.StartTheme);
        SaveLoadGame.LoadDataJson(out GameDatalist gameDatalist, path);
        Debug.Log(SaveLoadGame._GameDatalist);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClicked(int sceneIndex)
    {
        Debug.Log("Start Button Clicked");
        Loader.Load(sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void TurnOnSroceboard()
    {
        //startMenu.SetActive(false);
        scroceboard.SetActive(true);
        LoadScoreboard();
    }



    public void LoadScoreboard()
    {
        //if(!SaveLoadGame.LoadDataJson(out gameData, path))
        //{
        //    gameData = new GameData(0, 0, 0);
        //}
        //else
        //{
        //    //gameData = new GameData();
        //    survivorsTime.order = $"Survivors Time:{gameData.minute}:{gameData.second} ";
        //    killCount.order = $"Kill Count:{gameData.killCount}";
        //}

        int count = 1;
        gameData = new GameData();
        if(scorePanels == null) { return; }
        if (!SaveLoadGame.LoadDataJson(out GameDatalist gameDatalist, path))
        {
            //gameData = new GameData(0, 0, 0);
            //gameDatalist =
        }
        else
        {
            for(int i = 0; i < scorePanels.Count; i++)
            {
                scorePanels[i].order.text = $"{count}";
                count++;
                if (i < gameDatalist.list.Count)
                {                    
                    scorePanels[i].survivorsTime.text = $"Survivors Time:{gameDatalist.list[i].minute}:{gameDatalist.list[i].second} ";
                    scorePanels[i].killCount.text = $"Kill Count:{gameDatalist.list[i].killCount}";
                }
                
                
                Debug.Log("UPdate scoreboard");
            }
            //gameData = new GameData();
            
        }

    }
}
