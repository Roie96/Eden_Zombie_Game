using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
  
    public static float currHealth = 100f;
    public static float maxHealth = 100f;
    public Image overlay; //our damage overlay gameobject
    public float duration;
    public float fadespeed;
    [Header("Damage Overlay")]

    private float durationTimer;

    void Start()
    {
        currHealth = maxHealth;
        AppleCollect.appleCollectEvent += fillHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g,overlay.color.b,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(overlay.color.a > 0){
            if(currHealth < 30)
                return;
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
}
