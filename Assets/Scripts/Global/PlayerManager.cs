using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using IL3DN;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    
    public static int appleCount; 
    public static int coffeeCount; 
    public static float currHealth = 100f;
    public static float maxHealth = 100f;
    public Image overlay; //our damage overlay gameobject
    public float duration;
    public float fadespeed;
    [Header("Damage Overlay")]
    private bool isSpeedBoostActive = false;
    
    private float durationTimer;

    [Header("Speed Boost")]
    public IL3DN_SimpleFPSController fpsController;
    public float speedBoostDuration = 5f;
    public float speedBoostMultiplier = 2f;
    public  MainGameSystem  MainGameSystem;
    

    void Start()
    {
        appleCount = 0;
        coffeeCount = 0;
        currHealth = maxHealth;
        AppleCollect.appleCollectEvent += maxAmmoApples;
        CoffeeCollect.coffeeCollectEvent += maxAmmoCoffee;
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

        // speed boost action
        if(Input.GetKeyDown("x") && coffeeCount > 0)
        {
            speedBoost();
            coffeeCount--;
        }

        //damage screen
        if(overlay.color.a > 0 && currHealth > 30){
            // if(currHealth < 30)
            //     return;
            
            if(durationTimer > duration){
                // fade image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadespeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g,overlay.color.b,tempAlpha);
            }
        }
        durationTimer += Time.deltaTime;
    }




    public void fillHealth(){
        currHealth = maxHealth;
    }

    public void takeDamage(float damage)
    {
        if(durationTimer > 1){
            currHealth-=damage;
            durationTimer = 0;
            overlay.color = new Color(overlay.color.r, overlay.color.g,overlay.color.b,1);
        }

        if(currHealth<=0)
            MainGameSystem.gameOver();
    }

    public void maxAmmoApples(){
        appleCount = 1;
    }

    public void maxAmmoCoffee(){
        coffeeCount = 1;
    }

    public void speedBoost(){
        if (!isSpeedBoostActive)
        {
            StartCoroutine(ActivateSpeedBoost());
        }
    }


    IEnumerator ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;

        // Store the original walk speed
        float originalWalkSpeed = fpsController.m_WalkSpeed;

        // Calculate the boosted walk speed
        float boostedWalkSpeed = originalWalkSpeed * speedBoostMultiplier;

        // Apply the boosted walk speed
        fpsController.m_WalkSpeed = boostedWalkSpeed;

        // Wait for the duration of the speed boost
        yield return new WaitForSeconds(speedBoostDuration);

        // Reset the walk speed back to the original value
        fpsController.m_WalkSpeed = originalWalkSpeed;

        isSpeedBoostActive = false;
    }
}
