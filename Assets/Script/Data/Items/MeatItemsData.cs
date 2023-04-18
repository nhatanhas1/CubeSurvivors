using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Meat",menuName ="Items/Meat")]
public class MeatItemsData : ItemsData
{
    [SerializeField] int healPoint;

    public override void UseItems(PlayerController player, GameObject expGemObject)
    {
        player.currentHitPoint +=healPoint;
        //ObjectPool.Instance.ReturnObjectToPool("ExpGem", expGemObject);
    }
}
