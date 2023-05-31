using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoffeeCollect : MonoBehaviour
{
    public GameObject coffee;
    public AudioSource pickUpSound;
    public static Action coffeeCollectEvent; 

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            pickUpSound.Play();
            coffeeCollectEvent?.Invoke();
            coffee.SetActive(false);
            SupplyDropsSystem.ExistCrate--;
        }
    }
}