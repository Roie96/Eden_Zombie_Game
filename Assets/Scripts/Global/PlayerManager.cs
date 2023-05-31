using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static int appleCount; 
    public static float currHealth = 100f;
    public static float maxHealth = 100f;
    public Image overlay; //our damage overlay gameobject
    public float duration;
    public float fadespeed;
    [Header("Damage Overlay")]

    private float durationTimer;

    void Start()
    {
        appleCount = 0;
        currHealth = maxHealth;
        AppleCollect.appleCollectEvent += maxAmmoApples;
        overlay.color = new Color(overlay.color.r, overlay.color.g,overlay.color.b,0);
    }

    // Update is called once per frame
    void Update()
    {
        // heal action
        if(Input.GetKeyDown("f") && appleCount > 0 && currHealth < maxHealth)
        {
            fillHealth();
            appleCount--;
        }

        //damage screen
        if(overlay.color.a > 0 && currHealth > 30){
            // if(currHealth < 30)
            //     return;
            durationTimer += Time.deltaTime;
            if(durationTimer > duration){
                // fade image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadespeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g,overlay.color.b,tempAlpha);
            }
        }
    }

    public void fillHealth(){
        currHealth = maxHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie")
        {
            currHealth-=other.GetComponent<Zombie>().getDamage();
            durationTimer = 0;
            overlay.color = new Color(overlay.color.r, overlay.color.g,overlay.color.b,1);

        }
    }

    public void maxAmmoApples(){
        appleCount = 1;
    }
}
