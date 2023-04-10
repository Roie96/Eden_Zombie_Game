using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   [SerializeField] GunData gunData;
   public bool isFiring = false;
    public GameObject theGun;
    public AudioSource gunShot;

    public AudioSource noAmmo;
    
    public GameObject flash;

    private bool isShooting=false;
    void Start()
    {
        AmmoCollect.maxAmmo += maxAmmo;
    }

    public void StartShooting()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    public void StopShooting()
    {
        isShooting = false;
    }
    void Update()
    {
        if (isShooting && gunData.autoShoot && Input.GetButtonUp("Fire1"))
        {
            // Stop shooting if the left mouse button is released
            StopShooting();
        }
        if(gunData.currMagAmmo <= 0 && gunData.currAmmo>0){
            StartCoroutine(Reload());
        }
    }

    public void maxAmmo(){
        gunData.currAmmo = gunData.magSize * 3;
    }

    public int getCurrMagAmmo(){
        return gunData.currMagAmmo;
    }
    public int getCurrAmmo(){
        return gunData.currAmmo;
    }
    public int getMagSize(){
        return gunData.magSize;
    }

    public IEnumerator down_weapon()
    {
        Debug.Log(gunData.name+"_Down");
        theGun.GetComponent<Animator>().Play(gunData.name+"_Down");
        yield return new WaitForSeconds(5);
        theGun.GetComponent<Animator>().Play("New State");
    }

    public IEnumerator up_weapon()
    {
        Debug.Log(gunData.name+"_Up");
        theGun.GetComponent<Animator>().Play(gunData.name+"_Up");
        yield return new WaitForSeconds(5);
        theGun.GetComponent<Animator>().Play("New State");
    }


    public IEnumerator Reload(){
        if(gunData.currAmmo>gunData.magSize){
                gunData.currAmmo-=(gunData.magSize - gunData.currMagAmmo);
                gunData.currMagAmmo=gunData.magSize;
            }
        else{
            gunData.currMagAmmo=gunData.currAmmo;
            gunData.currAmmo=0;
        }
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator Shoot(){
        isShooting=true;
        bool autoShoot = gunData.autoShoot;

        do{
        if(gunData.currMagAmmo > 0){
        theGun.GetComponent<Animator>().Play(gunData.name+"_Shoot");
        flash.SetActive(true);
        if(!gunShot.isPlaying)
            gunShot.Play();
        gunData.currMagAmmo--;
        yield return new WaitForSeconds(0.05f);
        flash.SetActive(false);
        }
        else if(gunData.currMagAmmo <= 0 && gunData.currAmmo>0){
            StartCoroutine(Reload());
        }
        else{
            Debug.Log("No Ammo" + name);
            noAmmo.Play();
        }
        yield return new WaitForSeconds(gunData.fireRate);
        theGun.GetComponent<Animator>().Play("New State");
        }while(isShooting && autoShoot);
        gunShot.Stop();
        StopShooting();
    }

}
