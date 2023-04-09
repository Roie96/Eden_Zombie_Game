using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunFire : MonoBehaviour
{
    public GameObject theGun;
    public AudioSource gunShot;
    public bool isFiring = false;
    public GameObject flash;

    void Update(){
        int ammo = GlobalAmmo.pistolAmmoCount;
        if(Input.GetButtonDown("Fire1")){
            if(isFiring == false  && ammo > 0){
                StartCoroutine(FireHandgun());
            }
        }

    }

    IEnumerator FireHandgun(){
        isFiring = true;
        theGun.GetComponent<Animator>().Play("PistolFire");
        flash.SetActive(true);
        gunShot.Play();
        GlobalAmmo.pistolAmmoCount--;
        yield return new WaitForSeconds(0.05f);
        flash.SetActive(false);
        yield return new WaitForSeconds(0.45f);
        theGun.GetComponent<Animator>().Play("New State");
        isFiring = false;
    }

}
