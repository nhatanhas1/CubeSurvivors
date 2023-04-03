using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using System;
using System.Linq;
using static SaveLoadGame;
using Unity.VisualScripting;

public static class SaveLoadGame 
{
    //static List<GameData> lastGameList = new List<GameData>();    
    //static int SaveCount;
    
    public static GameDatalist _GameDatalist;
    
    public static void UpdateLastSave()
    {

    }

    public static void SaveDataJson(GameData gameData, string path)
    {
        if(!LoadDataJson(out GameDatalist gameDatalist, path)) {
            _GameDatalist = gameDatalist;
        }
        else
        {
            _GameDatalist = gameDatalist;
        }
            
        if (_GameDatalist.list == null)
        {
            _GameDatalist.list = new List<GameData> { gameData };
        }
        else
        {
            _GameDatalist.list.Add(gameData);
        }

        SortedDataList(_GameDatalist);

        var file = JsonUtility.ToJson(_GameDatalist, true);

        //GameDatalist gameDatalist = new GameDatalist();      
        //gameDatalist.list = new List<GameData> { gameData };
        //var file = JsonUtility.ToJson(gameDatalist, true);
        System.IO.File.WriteAllText(path, file);
        //Debug.Log("Add save");
    }

    public static void SortedDataList(GameDatalist dataList)
    {
        for(int i = 0; i < dataList.list.Count; i++)
        {
            for(int j = i + 1; j < dataList.list.Count; j++)
            {
                if (dataList.list[j].minute > dataList.list[i].minute)
                {
                    GameData tmp = dataList.list[i];
                    dataList.list[i] = dataList.list[j];
                    dataList.list[j] = tmp;
                }else if (dataList.list[j].minute == dataList.list[i].minute)
                {
                    if(dataList.list[j].second > dataList.list[i].second)
                    {
                        GameData tmp = dataList.list[i];
                        dataList.list[i] = dataList.list[j];
                        dataList.list[j] = tmp;
                    }else if(dataList.list[j].second == dataList.list[i].second)
                    {
                        if (dataList.list[j].killCount > dataList.list[i].killCount)
                        {
                            GameData tmp = dataList.list[i];
                            dataList.list[i] = dataList.list[j];
                            dataList.list[j] = tmp;
                        }
                    }
                }
            }
        } 

        for(int i = 10; i < dataList.list.Count; i++)
        {
            dataList.list.Remove(dataList.list[i]);
        }
    }

    public static bool LoadDataJson(out GameDatalist _GameDatalist, string path)
    {
        try
        {
            var jsonText = System.IO.File.ReadAllText(path);
            _GameDatalist = JsonUtility.FromJson<GameDatalist>(jsonText);
            SaveLoadGame._GameDatalist = _GameDatalist;
            
            return true;

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            _GameDatalist = new GameDatalist();
            SaveLoadGame._GameDatalist = _GameDatalist;
            return false;
        }
        //return false;

    }

}
