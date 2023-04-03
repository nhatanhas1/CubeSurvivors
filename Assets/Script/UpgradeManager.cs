using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class UpgradeManager : MonoBehaviour
{    
    [SerializeField] PlayerController playerController;
    //[SerializeField] List<Sprite> spriteList;
    IWeapon IWeapon;

    public List<UpgradeButton> upgradeButtonList;

    public List<GameObject> weaponList;
       
    [SerializeField] int currentUpgradePart;

    public List<WeaponData.WeaponType> currentUpgradePartList;

    // Start is called before the first frame update
    private void Awake()
    {
        if(playerController != null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }

    private void OnEnable()
    {
        //SetupUpgradeButtonList();
    }

    void UpdatePartList()
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            IWeapon = weaponList[i].GetComponent<IWeapon>();
            IWeapon.AddToUpgradeButton(this);
            //Debug.Log(IWeapon.WeaponName + i);
        }
    }

    public void SetupUpgradeButtonList()
    {
        //Debug.Log("setUp BUtton");

        currentUpgradePartList.Clear();
        //SetupUpgradePartList();
        //playerController.UpdatePartList();
        UpdatePartList();

        if (upgradeButtonList.Count > 0)
        {
            for (int i = 0; i < upgradeButtonList.Count; i++)
            {
                //Debug.Log(currentUpgradePartList.Count);
                if (currentUpgradePartList.Count > 0)
                {                    
                    //Debug.Log("currentUpgradePartCount " + currentUpgradePartList.Count);
                    currentUpgradePart = Random.Range(0, currentUpgradePartList.Count - 1);
                    //Debug.Log("currentUpgradePart" + currentUpgradePart);
                    upgradeButtonList[i].weaponType = currentUpgradePartList[currentUpgradePart];

                    //Debug.Log("UpgradePart: " + currentUpgradePartList[currentUpgradePart]);
                    GetUpgradeInformation(currentUpgradePartList[currentUpgradePart], upgradeButtonList[i]);
                    //Debug.Log("currentpart la " + currentUpgradePartList[currentUpgradePart] + "ID la" + (int)currentUpgradePartList[currentUpgradePart]);

                    currentUpgradePartList.RemoveAt(currentUpgradePart);
                }
                else
                {
                    upgradeButtonList[i].gameObject.SetActive(false);
                }

            }
            //Debug.Log(currentUpgradePartList.Count);
        }
        //currentUpgradePartList.Clear();
    }

    void GetUpgradeInformation(WeaponData.WeaponType weaponType, UpgradeButton upgradeButton)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            PlayerUpgradePower playerUpgradePower = weaponList[i].GetComponent<PlayerUpgradePower>();
            if (playerUpgradePower.WeaponData.weaponType == weaponType)
            {
                upgradeButton.image.sprite = playerUpgradePower.WeaponData.sprite;
                //upgradeButton.upgradeInformation.text = playerUpgradePower.GetUpgradeInformation(playerUpgradePower.weaponCurrentLv);

                if (playerUpgradePower.WeaponData.weaponUpgradeInformation[playerUpgradePower.weaponCurrentLv] != null)
                {
                    upgradeButton.upgradeInformation.text = playerUpgradePower.WeaponData.weaponUpgradeInformation[playerUpgradePower.weaponCurrentLv];
                }

                //upgradeButton.upgradeInformation.text = iWeapon.WeaponData.weaponUpgradeInformation;

            }
        }
    }

    public void Upgrade(WeaponData.WeaponType weaponType)
    {
        //Debug.Log("Upgrade ID " + (int)weaponType + " upgrade part " + weaponType);

        for (int i = 0; i < weaponList.Count; i++)
        {
            IWeapon IWeapon = weaponList[i].GetComponent<IWeapon>();
            if (IWeapon.WeaponData.weaponType == weaponType)
            {
                IWeapon.Upgrade();
                //playerUpgradePower.Upgrade();

                AudioManager.Instance.GetAudioSource(AudioManager.Sound.SoundName.LevelUp).Stop();
                AudioManager.Instance.GetAudioSource(AudioManager.Sound.SoundName.Background).UnPause();
                //Debug.Log("Upgrade " + upgradePart);
            }
            //else
            //{
            //    Debug.Log("khong tiem thay" + upgradePart + "voi Id la" + (int)upgradePart);
            //}
        }
    }
}
