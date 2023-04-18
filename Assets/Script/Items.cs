using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public interface IConsumable
{
    void UseItem(PlayerController player);
}

public class Items : MonoBehaviour ,IConsumable
{
    [SerializeField] ItemsData itemsData;

    //[SerializeField] ExpGemData expGemData;
       
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem(PlayerController player)
    {
        itemsData.UseItems(player, this.gameObject);
        //player.GetExp(expGemData.exp);
        //ObjectPool.Instance.ReturnObjectToPool("ExpGem",this.gameObject);
        //player.playerExp += expGemData.exp;
    }
}
