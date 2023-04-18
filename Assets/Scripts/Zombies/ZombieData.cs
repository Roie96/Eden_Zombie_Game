using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Zombie", menuName="Zombies/Zombie")]
public class ZombieData : ScriptableObject
{
    public new string name;

    public float health;

    public float speed;

    public float damage;

}
