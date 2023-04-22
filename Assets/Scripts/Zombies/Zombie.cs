using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Zombie : AI, Idamageable
{
    public ZombieData zombieDATA;

    public float health;

    
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
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public float getDamage(){
        return zombieDATA.damage;
    }
}
