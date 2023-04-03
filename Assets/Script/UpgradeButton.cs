using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static PlayerController;

public class UpgradeButton : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField] PlayerController playerController;

    [SerializeField] UpgradeManager upgradeManager;
    public Image image;
    public TextMeshProUGUI upgradeInformation;
    public WeaponData.WeaponType weaponType;
    //WeaponData weaponData;

    //[SerializeField] string upgradeInformation;

    //public int upgradePart;

    public void UpgradePlayer()
    {
        //playerController.Upgrade(upgradePart);

        upgradeManager.Upgrade(weaponType);
        //Debug.Log("Upgrade Park: " + upgradePart);
    }
}
