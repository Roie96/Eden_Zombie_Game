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

    public Gun GunScript;
    void Start()
    {
        guns.Add(pistol);
        guns.Add(ak47);
        guns[(currGun+1)%guns.Count].SetActive(false);
        guns[currGun].SetActive(true);
        GunScript = guns[currGun].GetComponent<Gun>();
        StartCoroutine(GunScript.up_weapon());
    }     
    void Update(){
        if(Input.GetButtonDown("Fire1")){
            GunScript.StartShooting();
        }

        if(Input.GetButtonDown("Jump")){
            next_gun();
        }
    }

    void next_gun()
    {
        GunScript = guns[currGun].GetComponent<Gun>();
        //StartCoroutine(GunScript.down_weapon());
        
        guns[currGun].SetActive(false);

        currGun = (currGun+1)%guns.Count;
        GunScript = guns[currGun].GetComponent<Gun>();
        guns[currGun].SetActive(true);
        StartCoroutine(GunScript.up_weapon());
        
    }
}
