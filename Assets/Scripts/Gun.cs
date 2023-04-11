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

    [SerializeField] private Transform muzzle;

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
        // Debug.Log("StopShoot!");
        isShooting = false;
    }
    void Update()
    {
        Debug.DrawRay(muzzle.position, muzzle.forward);
        timeSinceLastShot += Time.deltaTime;
        if (isShooting && gunData.autoShoot && Input.GetButtonUp("Fire1"))
        {
            // Stop shooting if the left mouse button is released
            StopShooting();
        }
        if(gunData.currMagAmmo <= 0 && gunData.currAmmo>0){
            StartReload();
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
        theGun.GetComponent<Animator>().Play(gunData.name+"_Down");
        yield return new WaitForSeconds(5);
        theGun.GetComponent<Animator>().Play("New State");
    }

    public IEnumerator up_weapon()
    {
        theGun.GetComponent<Animator>().Play(gunData.name+"_Up");
        yield return new WaitForSeconds(5);
        theGun.GetComponent<Animator>().Play("New State");
    }

    public void StartReload(){
        if(! gunData.reloading){
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload(){
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        //HERE Animation
        if(gunData.currAmmo>gunData.magSize){
                gunData.currAmmo-=(gunData.magSize - gunData.currMagAmmo);
                gunData.currMagAmmo=gunData.magSize;
            }
        else{
            gunData.currMagAmmo=gunData.currAmmo;
            gunData.currAmmo=0;
        }
        gunData.reloading = false;
        
    }
    float timeSinceLastShot;
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / ((gunData.fireRate / 60f));
    public void Shoot2(){
            if(gunData.currMagAmmo > 0){
                if(CanShoot()){
                    if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance)){
                        Debug.Log(hitInfo.transform.name);
                        Idamageable damage = hitInfo.transform.GetComponent<Idamageable>();
                        damage?.TakeDamage(gunData.damage);
                    }
                    gunData.currMagAmmo--;
                    timeSinceLastShot = 0;
                    StartCoroutine(Shoot());
                }
            }
            else{
                noAmmo.Play();  
            }
    }

    IEnumerator Shoot(){
        theGun.GetComponent<Animator>().Play(gunData.name+"_Shoot");
        flash.SetActive(true);
        gunShot.Play();
        
        yield return new WaitForSeconds(0.1f);
        theGun.GetComponent<Animator>().Play("New State");
        flash.SetActive(false);
    }

}
