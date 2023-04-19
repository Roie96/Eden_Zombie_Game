using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GlobalAmmo : MonoBehaviour
{
    public GameObject ammoText;

    void Update()
    {
        ammoText.GetComponent<TextMeshProUGUI>().text = "AMMO: " + Weapon_holder.GunScript.getCurrMagAmmo()+"/"+Weapon_holder.GunScript.getCurrAmmo();
    }
}
