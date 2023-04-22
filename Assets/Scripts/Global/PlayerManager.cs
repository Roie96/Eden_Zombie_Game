using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerManager : MonoBehaviour
{
    public static float currHealth = 100f;
    public static float maxHealth = 100f;
    void Start()
    {
        currHealth = maxHealth;
        AppleCollect.appleCollectEvent += fillHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillHealth(){
        currHealth = maxHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie")
        {
            currHealth-=other.GetComponent<Zombie>().getDamage();
        }
    }
}
