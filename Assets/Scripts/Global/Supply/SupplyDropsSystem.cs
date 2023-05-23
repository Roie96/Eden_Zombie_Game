using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SupplyDropsSystem : MonoBehaviour
{
    public int xPos;
    public int zPos;
    public GameObject barricadeCrate;
    public GameObject ammoCrate;
    public GameObject ammoDropUI;
    public List<GameObject> creates = new List<GameObject>();
    public int index;

    public GameObject appleHealth;

    public static int ExistAmmo = 0;

    public static int ExistApples = 0;

    // Start is called before the first frame update
    void Start()
    {
        creates.Add(barricadeCrate);
        creates.Add(ammoCrate);
        creates.Add(appleHealth);

    }

    void Update()
    {
        if(ExistAmmo< 5){
            ExistAmmo++;
            StartCoroutine(RandomCrateFall());
        }    

        //if(ExistApples< 5){
          //  ExistApples++;
          //  StartCoroutine(RandomApple());
       // }      
    }

    IEnumerator RandomCrateFall(Vector3 position = default(Vector3))
    {
        index = UnityEngine.Random.Range(0, 3);
        Debug.Log(index);
        yield return new WaitForSeconds(5);
        if(position.Equals(default(Vector3))){
            position = FlagSystem.GetRandomTerrainPosition(100);
        }
        ammoDropUI.SetActive(true);
        position.y+=50;
        Instantiate(creates[index], position, Quaternion.identity);
        ammoDropUI.SetActive(false);

    }

    // IEnumerator RandomApple(Vector3 position = default(Vector3))
    // {
    //     yield return new WaitForSeconds(5);
    //     if(position.Equals(default(Vector3))){
    //         position = FlagSystem.GetRandomTerrainPosition(100);
    //     }
        
    //     position.y+=50;
    //     Instantiate(appleHealth, position, Quaternion.identity);
    
    // }
}
