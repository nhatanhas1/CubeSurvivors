using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Items", menuName = "Items/ItemTest")]
public class ItemsData : ScriptableObject
{
    public string itemsName;

    public virtual void UseItems(PlayerController player, GameObject items)
    {
        //player.GetExp(exp);
        //ObjectPool.Instance.ReturnObjectToPool("ExpGem", this.gameObject);
    }

}


