using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField]
    public Transform cammeraTranform;

    private RaycastHit _Hit;
    [SerializeField]
    public GameObject woodenBarricade, tempBarricade;
    public bool buildMode;
    public bool placeBarricade; 
    public static int currAmmoBarricade = 5; 

    // Start is called before the first frame update
    void Start()
    {   
        buildMode = false;
        tempBarricade.SetActive(false);
        //subscribe to function in Barricade Collect
        BarricadeCollect.maxAmmoBarricade += maxAmmoBarricade;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }
        
        if(Input.GetKeyDown("e")){
            if(buildMode){
                buildMode = false;
                tempBarricade.GetComponent<CheckOverlap>().overlap = false;
            }
                
            else
                buildMode = true;

            tempBarricade.SetActive(buildMode);
        }
        if(Input.GetKeyDown("v") && buildMode){
            //build
            if (currAmmoBarricade > 0 && !(tempBarricade.GetComponent<CheckOverlap>().overlap)){
                PlaceBarricade();
            }
            buildMode = false;
            tempBarricade.GetComponent<CheckOverlap>().overlap = false;
            tempBarricade.SetActive(buildMode);

        }

        // update white barricade position
        if(buildMode && Physics.Raycast(new Ray(cammeraTranform.position, cammeraTranform.forward), out _Hit)){
            tempBarricade.transform.position = _Hit.point;
            tempBarricade.transform.eulerAngles = new Vector3 (0, Mathf.RoundToInt(transform.eulerAngles.y+90f) != 0 ? 
            Mathf.RoundToInt((transform.eulerAngles.y+90f) / 90f) * 90f :0, 0);
        }
        
    }

    public void PlaceBarricade()
    {
        if(Physics.Raycast(new Ray(cammeraTranform.position, cammeraTranform.forward), out _Hit)){
            Instantiate(woodenBarricade, _Hit.point, tempBarricade.transform.rotation);
            currAmmoBarricade--;
        }
    }

    public void maxAmmoBarricade(){
        currAmmoBarricade = 5;
    }

    public int getCurrAmmoBarricade(){
        return currAmmoBarricade;
    }

}