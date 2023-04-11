using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandGunFire : MonoBehaviour
{
    public static GameObject theGun;
    public AudioSource gunShot;
    public bool isFiring = false;
    public GameObject flash;

    public static Action shootInput; 
    // void Update(){
    //     if(Input.GetButtonDown("Fire1")){
    //         shootInput?.Invoke();
    //     }

    //     if(Input.GetButtonDown("q")){
    //         shootInput?.Invoke();
    //     }
    // }

}
