using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class GameData 
{        
    public float minute;
    public float second;
    public int killCount;

    //public SortedList<int, GameData> sortedList = new SortedList<int, GameData>(10);

    public GameData()
    {

    }

    public GameData(float minute, float second, int killCount)
    {
        this.minute = minute;
        this.second = second;
        this.killCount = killCount;
    }


    //void SetTimer(float mminute, float second)
    //{
    //    this.minute = minute;
    //    this.second = second;

    //}

    //void GetTimer(out float minute, out float second)
    //{
    //    minute = this.minute;
    //    second = this.second;
    //}

    //void SetKillCount(int killCount)
    //{
    //    this.killCount = killCount;
    //}

    //int GetKillCount()
    //{
    //    return this.killCount;  
    //}

   

}

[System.Serializable]
public class GameDatalist
{
    //public SortedList<int, GameData> sortedList = new SortedList<int, GameData>(10);
    //GameData[] gameDataArray;

    public List<GameData> list;



    public GameDatalist()
    {

    }

    //public void AddToList(GameData gameData)
    //{

    //}

    public void AddToSortedlist(GameData gameData)
    {

        //for (int i = 0; i < sortedList.Count; i++)
        //{
        //    if (GameDatalist.sortedList[i] == null)
        //    {
        //        GameDatalist.sortedList.Add(i, gameData);
        //        Debug.Log("Add save");
        //    }
        //}

        //sortedList = new SortedList<int, GameData> ();
        //sortedList.Add(1,gameData);

        list = new List<GameData>();
        list.Add(new GameData(1,1,1));
        list.Add(new GameData(21, 1, 31));
        list.Add(new GameData(342, 12, 111));

        //sortedList.Values.ToList().ForEach(x => list.Add(x));

        
        //list.Add(gameData);
        //list.Sort();

    }
}

