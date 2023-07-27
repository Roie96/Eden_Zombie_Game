using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Zombie : AI, Idamageable
{
    public ZombieData zombieDATA;

    public float health;

    // Create an event delegate with Zombie as the parameter
    public event Action<Zombie> OnDestroyedZombie;
    
    public void TakeDamage(float damage)
    {
        zombieDATA.health-=damage;
        if(zombieDATA.health<=0){
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        zombieDATA = Instantiate(zombieDATA);
        setSpeed(zombieDATA.speed);
    }


    private void OnDestroy()
    {
        EnemiesSystem.ExistZombie--;
        OnDestroyedZombie?.Invoke(this); // Invoke the event if there are subscribers
        OnDestroyedZombie = null; // Unsubscribe from the event
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public float getDamage(){
        return zombieDATA.damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //currHealth-=other.GetComponent<Zombie>().getDamage();
            PlayerManager player = other.GetComponent<PlayerManager>(); 
            player.takeDamage(getDamage());
        }
    }
}
