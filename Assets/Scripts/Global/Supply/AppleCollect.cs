using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AppleCollect : MonoBehaviour
{
    public GameObject apple;
    public AudioSource pickUpSound;
    public static Action appleCollectEvent; 

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            pickUpSound.Play();
            appleCollectEvent?.Invoke();
            apple.SetActive(false);
            SupplyDropsSystem.ExistCrate--;
        }
    }
}