using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GunData gunData;
    public bool isFiring = false;
    public GameObject theGun;
    public GameObject hitMark;
    public AudioSource gunShot;
    public AudioSource reloadUpSound;


    public AudioSource noAmmo;
    
    public GameObject flash;

    public Reticle reticle;

    [SerializeField] private Transform muzzle;

    public Transform aimingPosition;
    public Transform defaultPosition;

    public Transform currentPosition;

    public bool isAiming;

    public float aimSpeed=3.0f;


    
    void Start()
    {
        gunData = Instantiate(gunData);
        AmmoCollect.maxAmmo += maxAmmo;
        gunData.reloading = false;
        currentPosition = GetComponent<Transform>();
        isAiming = false;
        
    }

    public void StopShooting()
    {
        gunData.isShooting = false;
        
        
    }

    void Update()
    {
        Debug.DrawRay( muzzle.position, muzzle.forward);
        timeSinceLastShot += Time.deltaTime;
        if(gunData.currMagAmmo == 0 && timeSinceLastShot > 1f / ((gunData.fireRate / 60f))){
            StartReload();
        }

        if(isAiming){
            currentPosition.position = Vector3.Lerp(currentPosition.position, aimingPosition.position, aimSpeed * Time.deltaTime);
            currentPosition.localScale = Vector3.Lerp(currentPosition.localScale, aimingPosition.localScale, aimSpeed * Time.deltaTime);
            
        }
        else{
            currentPosition.position = Vector3.Lerp(currentPosition.position, defaultPosition.position, aimSpeed * Time.deltaTime);
            currentPosition.localScale = Vector3.Lerp(currentPosition.localScale, defaultPosition.localScale, aimSpeed * Time.deltaTime);
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
    public bool getIsAuto(){
        return gunData.autoShoot;
    }

    public IEnumerator down_weapon()
    {
        gunData.reloading = false;
        gunData.isShooting = false;
        theGun.GetComponent<Animator>().Play(gunData.name+"_Down");
        yield return new WaitForSeconds(5);
        theGun.GetComponent<Animator>().Play("New State");
    }

    public IEnumerator up_weapon()
    {
        gunData.reloading = false;
        gunData.isShooting = false;
        theGun.GetComponent<Animator>().Play(gunData.name+"_Up");
        yield return new WaitForSeconds(5);
        theGun.GetComponent<Animator>().Play("New State");
    }

    public void StartReload(){
        if(! gunData.reloading && gunData.currMagAmmo < gunData.magSize && gunData.currAmmo!=0){
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload(){
        gunData.reloading = true;
        reloadUpSound.Play();
        theGun.GetComponent<Animator>().Play(gunData.name+"_Load");
        yield return new WaitForSeconds(gunData.reloadTime);
        theGun.GetComponent<Animator>().Play("New State");

        int toFill = gunData.magSize - gunData.currMagAmmo;
        if(toFill <= gunData.currAmmo){
            gunData.currAmmo-=toFill;
            gunData.currMagAmmo+=toFill;
        }

        else{

            gunData.currMagAmmo+=gunData.currAmmo;
            gunData.currAmmo=0;
        }
        gunData.reloading = false;
        
    }

    float timeSinceLastShot;

    public LayerMask zombiesLayer;
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / ((gunData.fireRate / 60f));
    public void Shoot2(){
            if(gunData.currMagAmmo > 0){
                if(CanShoot()){
                    if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance)){
                        Idamageable damage = hitInfo.transform.GetComponent<Idamageable>();
                         if (damage != null){
                                damage.TakeDamage(gunData.damage);
                                StartCoroutine(showHitmark());
                            }
                        damage?.TakeDamage(gunData.damage);
                    }
                    gunData.currMagAmmo--;
                    timeSinceLastShot = 0;
                    StartCoroutine(Shoot());

                    Collider [] zombies = Physics.OverlapSphere(transform.position, gunData.noiseDistance, zombiesLayer);
                    for (int i=0; i<zombies.Length; i++){
                        zombies[i].GetComponent<AI>().setToAware(); 
                    }
                        
                }
            }
            else{
                gunData.isShooting=false;
                if(! gunData.reloading)
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

    IEnumerator showHitmark() {
        hitMark.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Debug.Log("hit");
        hitMark.SetActive(false);
    }


}
