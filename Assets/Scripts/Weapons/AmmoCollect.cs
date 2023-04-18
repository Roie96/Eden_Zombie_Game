using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AmmoCollect : MonoBehaviour
{
    public GameObject ammo;
    public AudioSource pickUpSound;
    public static Action maxAmmo; 
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            pickUpSound.Play();
            maxAmmo?.Invoke();
            ammo.SetActive(false);
            SupplyDropsSystem.ExistAmmo--;
        }
    }
}
