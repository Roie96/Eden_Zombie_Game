using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        

        Vector3 position = new Vector3(x, 0, z);
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(position, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
        {
            // The hit position contains the valid position on the NavMesh
            Vector3 validPosition = hit.position;
            float y = validPosition.y; // This is the Y coordinate of the valid position
        }

        // Debug.Log("X: " + xPos);
        // Debug.Log("Z: " + zPos);
        Instantiate(walkerZombie, new Vector3(xPos, playerTransform.position.y, zPos), Quaternion.identity);
    }
}
