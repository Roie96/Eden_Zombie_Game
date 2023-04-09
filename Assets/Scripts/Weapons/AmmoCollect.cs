using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollect : MonoBehaviour
{
    public GameObject ammo;
    public AudioSource pickUpSound;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            pickUpSound.Play();
            GlobalAmmo.pistolAmmoCount += 10;
            ammo.SetActive(false);
            RandomDrop.ExistAmmo--;
        }
    }
}
