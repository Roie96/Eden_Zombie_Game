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
    public GameObject objectToPlace, tempObject;
    public bool buildMode;
    public bool placeBarricade;
    public static int currAmmoBarricade = 550;
    public bool isCollidingWithTerrain;

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

        if (Input.GetKeyDown("e"))
        {
            if (buildMode)
            {
                buildMode = false;
                tempBarricade.GetComponent<CheckOverlap>().overlap = false;
            }
            else
            {
                buildMode = true;
            }
            tempBarricade.SetActive(buildMode);
        }

        if (Input.GetKeyDown("v") && buildMode)
        {
            // Build
            if (currAmmoBarricade > 0 && isCollidingWithTerrain)
            {
                PlaceBarricade();
            }
        }
        if (buildMode)
        {
            // Check if the terrain is colliding with tempBarricade
            isCollidingWithTerrain = Physics.Raycast(new Ray(cammeraTranform.position, cammeraTranform.forward), out _Hit) &&
                _Hit.transform.tag == "Terrain";

            if (isCollidingWithTerrain)
            {
                tempBarricade.SetActive(true);
                tempBarricade.transform.position = _Hit.point;
                tempBarricade.transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(transform.eulerAngles.y + 90f) != 0 ?
                    Mathf.RoundToInt((transform.eulerAngles.y + 90f) / 90f) * 90f : 0, 0);

                Transform newT = newPositionBaricade(tempBarricade.transform);
                tempBarricade.transform.position = newT.position;
                tempBarricade.transform.rotation = newT.rotation;
                tempBarricade.transform.localScale = newT.localScale;
            }
            else
            {
                tempBarricade.SetActive(false);
            }
        }
      
    }
    public void PlaceBarricade(){ 
        GameObject newBarricade = Instantiate(woodenBarricade, tempBarricade.transform.position, tempBarricade.transform.rotation);
        newBarricade.tag = "Barricade";
        currAmmoBarricade--;
    }

    public void maxAmmoBarricade(){
        currAmmoBarricade = 10;
    }

    public int getCurrAmmoBarricade(){
        return currAmmoBarricade;
    }

    Transform newPositionBaricade(Transform currTransform){
        GameObject[] barricades = GameObject.FindGameObjectsWithTag("Barricade");
        float closestDistance = 3f;
        Vector3 closestPosition = currTransform.position;
        Transform tempBaricade = null;

        foreach (GameObject barricade in barricades)
        {
            float distance = Vector3.Distance(currTransform.position, barricade.transform.position);
            if (distance < closestDistance && currTransform != barricade.transform)
            {
                closestDistance = distance;
                closestPosition = barricade.transform.position;
                tempBaricade = barricade.transform;
            }
        }
        if(currTransform.position != closestPosition){
            currTransform.rotation = tempBaricade.rotation;
            if(currTransform.position.x < closestPosition.x){ //right
                currTransform.transform.position = closestPosition;
                currTransform.transform.Translate(-currTransform.transform.forward * 1.5f, Space.World);
            }
            else{ //left
                currTransform.transform.position = closestPosition;
                currTransform.transform.Translate(currTransform.transform.forward * 1.5f, Space.World);
            }
        }
        return currTransform;
    }
}
