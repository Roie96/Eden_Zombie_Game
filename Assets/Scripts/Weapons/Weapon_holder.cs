using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon_holder : MonoBehaviour
{
    public GameObject pistol;
    public GameObject ak47;

    public List<GameObject> guns = new List<GameObject>();

    public int currGun;
    //public static Action shootInput;

    public static Gun GunScript;

    public static Action shootInput;
    public static Action reloadInput;
    void Start()
    {
        guns.Add(pistol);
        guns.Add(ak47);
        guns[(currGun+1)%guns.Count].SetActive(false);
        guns[currGun].SetActive(true);
        GunScript = guns[currGun].GetComponent<Gun>();
        subscribe(GunScript);
        StartCoroutine(GunScript.up_weapon());
    }     

    bool isShoot;
    void Update(){
        
        if (PauseMenu.isPaused)
        {
            return;
        }
        
        if(Input.GetButtonDown("Fire1")){
            shootInput?.Invoke();
            isShoot=true;
        }

        if(Input.GetButtonUp("Fire1")){
            isShoot=false;
        }

        if(Input.GetButtonDown("Fire3")){
            GunScript.isAiming = false;
        }

        // if(Input.GetMouseButtonDown(1)){
        //     GunScript.isAiming = !GunScript.isAiming;
        // }
        if (Input.GetMouseButtonDown(1))
        {
            GunScript.isAiming = true; // Start aiming when the right mouse button is pressed
        }
        if (Input.GetMouseButtonUp(1))
        {
            GunScript.isAiming = false; // Stop aiming when the right mouse button is released
        }

        if (GunScript.isAiming)
        {
            GunScript.isAiming = true;
        }
        else
        {
            GunScript.isAiming = false;
        }

        if(isShoot && GunScript.getIsAuto()){
            shootInput?.Invoke();
        }

        if(Input.GetKeyDown("q")){
            next_gun();
        }
        if(Input.GetKeyDown("r")){
            GunScript.isAiming = false;
            reloadInput?.Invoke();
        }
    }

    void next_gun()
    {
        GunScript = guns[currGun].GetComponent<Gun>();
        guns[currGun].SetActive(false);
        currGun = (currGun+1)%guns.Count;
        GunScript.isAiming = false;
        unSubscribe(GunScript);

        GunScript = guns[currGun].GetComponent<Gun>();
        guns[currGun].SetActive(true);
        subscribe(GunScript);
        StartCoroutine(GunScript.up_weapon());    
    }

    void unSubscribe(Gun gun){
        reloadInput -= gun.StartReload;
        shootInput -= gun.Shoot2;
    }

    void subscribe(Gun gun){
        reloadInput += gun.StartReload;
        shootInput += gun.Shoot2;

    }
}
