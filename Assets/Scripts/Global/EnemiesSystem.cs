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
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        yield return new WaitForSeconds(5);

        xPos = (int)Mathf.Round(playerTransform.position.x - areaAroundThePlayer / 2 + Random.Range(0, areaAroundThePlayer));
        zPos = (int)Mathf.Round(playerTransform.position.z - areaAroundThePlayer / 2 + Random.Range(0, areaAroundThePlayer));
        

        // Vector3 position = new Vector3(xPos, 0, zPos);

        // // Debug.Log("X: " + xPos);
        // // Debug.Log("Z: " + zPos);
        // Instantiate(walkerZombie, position, Quaternion.identity);

        Vector3 position = new Vector3(xPos, 0, zPos);
        NavMeshHit hit;

        // Sample the position on the NavMesh to find a valid y coordinate
        if (NavMesh.SamplePosition(position, out hit, 20f, NavMesh.AllAreas))
        {
            position = hit.position;
        }

        // Instantiate the zombie prefab and set its position on the NavMesh
        GameObject zombie = Instantiate(walkerZombie, position, Quaternion.identity);
        NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.Warp(position);
        }

    }
}

