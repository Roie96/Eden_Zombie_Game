using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerManager : MonoBehaviour
{
    public static float health = 100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie")
        {
            health-=other.GetComponent<Zombie>().getDamage();
            Debug.Log(health);
        }
    }
}
