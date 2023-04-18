using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New ExpGems", menuName = "Items/ExpGems")]
public class ExpGemData : ItemsData
{
    [SerializeField] int exp;

    public override void UseItems(PlayerController player,GameObject expGemObject)
    {
        player.GetExp(exp);
        ObjectPool.Instance.ReturnObjectToPool("ExpGem", expGemObject);
    }
}