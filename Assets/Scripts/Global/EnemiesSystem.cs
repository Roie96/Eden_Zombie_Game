using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesSystem : MonoBehaviour
{
    public static GameObject walkerZombie;

    public static int ExistZombie = 0;

    void Start()
    {
        
    }

    void Update()
    { 

    }

    // public static void createZombies(int n)
    // {
    //     for(int i=0; i<n; i++)
    //         createZombie();
    // }

    public static GameObject createZombie()
    {
        Vector3 position = FlagSystem.GetRandomTerrainPosition(200);

        // Instantiate the zombie prefab and set its position on the NavMesh
        GameObject zombie = Instantiate(walkerZombie, position, Quaternion.identity);
        NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.Warp(position);
        }
        ExistZombie++;
        return zombie;
    }
}

