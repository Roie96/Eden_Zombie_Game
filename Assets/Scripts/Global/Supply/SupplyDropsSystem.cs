using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SupplyDropsSystem : MonoBehaviour
{
    public GameObject barricadeCrate;
    public GameObject ammoCrate;
    public GameObject appleHealth;
    public List<GameObject> creates = new List<GameObject>();

    public static int ExistCrate = 0;

    // Start is called before the first frame update
    void Start()
    {
        creates.Add(barricadeCrate);
        creates.Add(ammoCrate);
        creates.Add(appleHealth);
    }

    void Update()
    {
        if(ExistCrate< 5){
            ExistCrate++;
            //StartCoroutine(RandomCrateFall());
            RandomCrateFall();
        }    

        //if(ExistApples< 5){
          //  ExistApples++;
          //  StartCoroutine(RandomApple());
       // }      
    }
    void RandomCrateFall(Vector3 position = default(Vector3))
    {
        int index = UnityEngine.Random.Range(0, 3);
        Debug.Log(index);
        if(position.Equals(default(Vector3))){
            position = FlagSystem.GetRandomTerrainPosition(100);
        }
        position.y+=50;
        Instantiate(creates[index], position, Quaternion.identity);
    }
}
