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
        HandGunFire.shootInput += StartShooting;
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
    }
    IEnumerator Shoot(){
        isShooting=true;
        bool autoShoot = gunData.autoShoot;

        do{
        if(gunData.currAmmo > 1){
        theGun.GetComponent<Animator>().Play(gunData.name);
        flash.SetActive(true);
        if(!gunShot.isPlaying)
            gunShot.Play();
        gunData.currAmmo--;
        yield return new WaitForSeconds(0.05f);
        flash.SetActive(false);
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
