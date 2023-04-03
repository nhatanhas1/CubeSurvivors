using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName ="Weapon")]
public class WeaponData : ScriptableObject
{
    public enum WeaponType
    {
        Knife,
        Cross,
        Axe,
        Garlic,
        Lightning,

        Hp,
        AttackSpeed,
        MoveSpeed,        
        Armor,
        PickUp,

        Meat,
        Gold,

    }

    public WeaponType weaponType;

    public string weaponName;
    public Sprite sprite;

    public int weaponMaxLv;
    public int attackDamage;
    public float baseAttackSpeed;

    public List<string> weaponUpgradeInformation;

    
}
