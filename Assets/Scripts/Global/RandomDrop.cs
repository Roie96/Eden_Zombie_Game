using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomDrop : MonoBehaviour
{
    public int xPos;
    public int zPos;
    public GameObject ammoCrate;

    public GameObject targetBall;

    public GameObject zombieCrate;
    public static int ExistAmmo = 0;

    public static int ExistTarget = 0;

    public static int ExistZombie = 0;


    // Start is called before the first frame update
    void Update()
    {
        if(ExistAmmo< 5){
            ExistAmmo++;
            StartCoroutine(RandomCrateFall());
        }

        if(ExistTarget < 5){
            ExistTarget++;
            StartCoroutine(RandomTarget());
        }


        
        
        if(ExistZombie < 5 + 1 ){
            ExistZombie++;
            StartCoroutine(createZombie());
        }
    }

    IEnumerator createZombie()
    {
        yield return new WaitForSeconds(5);
        xPos = Random.Range(380, 400);
        zPos = Random.Range(370, 400);
        Instantiate(zombieCrate, new Vector3(xPos, 10, zPos), Quaternion.identity);
    }

    IEnumerator RandomCrateFall()
    {
        yield return new WaitForSeconds(5);
        xPos = Random.Range(380, 400);
        zPos = Random.Range(370, 400);
        Instantiate(ammoCrate, new Vector3(xPos, 50, zPos), Quaternion.identity);
    }
    IEnumerator RandomTarget()
    {
        yield return new WaitForSeconds(5);
        xPos = Random.Range(380, 400);
        zPos = Random.Range(370, 400);
        Instantiate(targetBall, new Vector3(xPos, Random.Range(10, 15), zPos), Quaternion.identity);
        

    }
}
