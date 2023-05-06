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

    IEnumerator RandomCrateFall(Vector3 position = default(Vector3))
    {
        yield return new WaitForSeconds(5);
        if(position.Equals(default(Vector3))){
            position = FlagSystem.GetRandomTerrainPosition(100);
        }
        
        position.y+=50;
        Instantiate(ammoCrate, position, Quaternion.identity);
    
    }

    IEnumerator RandomApple(Vector3 position = default(Vector3))
    {
        yield return new WaitForSeconds(5);
        if(position.Equals(default(Vector3))){
            position = FlagSystem.GetRandomTerrainPosition(100);
        }
        
        position.y+=50;
        Instantiate(appleHealth, position, Quaternion.identity);
    
    }
}
