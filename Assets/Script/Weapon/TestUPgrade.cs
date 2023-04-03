using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class TestUPgrade : MonoBehaviour
{
    public GameObject testWeapon1Object;
    public GameObject testWeapon2Object;

    public GameObject testEnemy;




    TestWeapon testWeapon;
    TestWeapon2 testWeapon2;
    PlayerUpgradePower PlayerUpgradePower;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerUpgradePower = testWeapon1Object.GetComponent<PlayerUpgradePower>();
        //testWeapon = testWeapon1Object.GetComponent<TestWeapon>();
        //testWeapon2 = testWeapon2Object.GetComponent<TestWeapon2>();



        //PlayerUpgradePower.Attack();
        //testWeapon.Attack();        
        //testWeapon2.Attack();

        //Debug.Log("");
        //PlayerUpgradePower = testWeapon2Object.GetComponent<PlayerUpgradePower>();
        //PlayerUpgradePower.Attack();
        //Debug.Log(PlayerUpgradePower.WeaponData.weaponName);
        //PlayerUpgradePower = testWeapon1Object.GetComponent<PlayerUpgradePower>();
        //PlayerUpgradePower.Attack();
        //Debug.Log(PlayerUpgradePower.WeaponData.weaponName);

        Debug.Log(NavMesh.GetSettingsCount());

        Instantiate(testEnemy,this.transform);
        Debug.Log("Them Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
