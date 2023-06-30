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
    public static int currAmmoBarricade = 500;
    private float gridSize = 0.25f;

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
            //if (currAmmoBarricade > 0 && !(tempBarricade.GetComponent<CheckOverlap>().overlap))
            if (currAmmoBarricade > 0)
            {
                PlaceBarricade();
            }
        }

        // Update white barricade position
        if (buildMode && Physics.Raycast(new Ray(cammeraTranform.position, cammeraTranform.forward), out _Hit))
        {
            // if(tempBarricade.GetComponent<CheckOverlap>().overlap){
            //     //Transform baricadeT = tempBarricade.GetComponent<CheckOverlap>().baricadePos.position;
            //     tempBarricade.transform.position = tempBarricade.GetComponent<CheckOverlap>().baricadePos.position;
            //     tempBarricade.transform.Translate(Vector3.right *0.5f);
            // }
            // else{
            //     tempBarricade.transform.position = _Hit.point;
            //     tempBarricade.transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(transform.eulerAngles.y + 90f) != 0 ?
            //      Mathf.RoundToInt((transform.eulerAngles.y + 90f) / 90f) * 90f : 0, 0);
            // }
                tempBarricade.transform.position = _Hit.point;
                tempBarricade.transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(transform.eulerAngles.y + 90f) != 0 ?
                Mathf.RoundToInt((transform.eulerAngles.y + 90f) / 90f) * 90f : 0, 0);
                
                Transform newT = newPositionBaricade(tempBarricade.transform);
                tempBarricade.transform.position = newT.position;
                tempBarricade.transform.rotation = newT.rotation;
                tempBarricade.transform.localScale = newT.localScale;


             
        }
    }
    public void PlaceBarricade()
    {
        // if(Physics.Raycast(new Ray(cammeraTranform.position, cammeraTranform.forward), out _Hit)){
        //     Instantiate(woodenBarricade, _Hit.point, tempBarricade.transform.rotation);
        //     currAmmoBarricade--;
        // }
        GameObject newBarricade = Instantiate(woodenBarricade, tempBarricade.transform.position, tempBarricade.transform.rotation);
        newBarricade.tag = "Barricade";
        currAmmoBarricade--;
    }

    public void maxAmmoBarricade()
    {
        currAmmoBarricade = 5;
    }

    public int getCurrAmmoBarricade()
    {
        return currAmmoBarricade;
    }

    Vector3 SnapToGrid(Vector3 position)
    {
         // Adjust the grid size as per your requirements
        float snappedX = Mathf.Floor(position.x / gridSize) * gridSize + gridSize / 5f;
        float snappedY = Mathf.Floor(position.y / gridSize) * gridSize + gridSize / 5f;
        float snappedZ = Mathf.Floor(position.z / gridSize) * gridSize + gridSize / 5f;
        return new Vector3(snappedX, snappedY, snappedZ);
    }

    Transform newPositionBaricade(Transform currTransform)
    {
        GameObject[] barricades = GameObject.FindGameObjectsWithTag("Barricade");
        float closestDistance = 2.5f;
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
