using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BarricadeCollect : MonoBehaviour
{
    public GameObject ammo;
    public AudioSource pickUpSound;
    public static Action maxAmmoBarricade; 
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            pickUpSound.Play();
            maxAmmoBarricade?.Invoke();
            ammo.SetActive(false);
            SupplyDropsSystem.ExistAmmo--;
        }
    }
}