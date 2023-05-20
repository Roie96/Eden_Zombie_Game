using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GlobalAmmo : MonoBehaviour
{
    public GameObject ammoText;
    public GameObject barricadeText;
    public BuildSystem buildSystem; // Add a public variable to hold the reference to the BuildSystem class

       void Start()
    {
        // Assign the reference to the BuildSystem class
        buildSystem = FindObjectOfType<BuildSystem>();
    }

    void Update()
    {
        barricadeText.GetComponent<TextMeshProUGUI>().text = "BARRICADE: "+buildSystem.getCurrAmmoBarricade();
        ammoText.GetComponent<TextMeshProUGUI>().text = "AMMO: " + Weapon_holder.GunScript.getCurrMagAmmo()+"/"+Weapon_holder.GunScript.getCurrAmmo();
    }
}
