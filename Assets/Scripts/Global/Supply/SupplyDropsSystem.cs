using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SupplyDropsSystem : MonoBehaviour
{
    public int xPos;
    public int zPos;
    public GameObject ammoCrate;

    public GameObject appleHealth;

    public static int ExistAmmo = 0;

    public static int ExistApples = 0;


    // Start is called before the first frame update
    void Update()
    {
        if(ExistAmmo< 5){
            ExistAmmo++;
            StartCoroutine(RandomCrateFall());
        }    

        if(ExistApples< 5){
            ExistApples++;
            StartCoroutine(RandomApple());
        }      
    }

    IEnumerator RandomCrateFall()
    {
        yield return new WaitForSeconds(5);
        xPos = Random.Range(380, 400);
        zPos = Random.Range(370, 400);
        Instantiate(ammoCrate, new Vector3(xPos, 50, zPos), Quaternion.identity);
    
    }

    IEnumerator RandomApple()
    {
        yield return new WaitForSeconds(5);
        xPos = Random.Range(380, 400);
        zPos = Random.Range(370, 400);
        Instantiate(appleHealth, new Vector3(xPos, 50, zPos), Quaternion.identity);
    
    }
}
