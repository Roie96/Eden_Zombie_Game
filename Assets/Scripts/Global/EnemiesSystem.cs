using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesSystem : MonoBehaviour
{
    public int xPos;
    public int zPos;

    public float areaAroundThePlayer;

    public GameObject walkerZombie;

    public static int ExistZombie = 0;

    public static int maxZombies = 15;

    public Transform playerTransform;


    void Start()
    {
        areaAroundThePlayer = 100f;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {           
        if(ExistZombie < maxZombies){
            ExistZombie++;
            StartCoroutine(createZombie());
        }
    }

    IEnumerator createZombie()
    {
        yield return new WaitForSeconds(5);

        Vector3 position = FlagSystem.GetRandomTerrainPosition(200);

        // Instantiate the zombie prefab and set its position on the NavMesh
        GameObject zombie = Instantiate(walkerZombie, position, Quaternion.identity);
        NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.Warp(position);
        }

    }
}

