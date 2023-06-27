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
            if (currAmmoBarricade > 0)
            {
                PlaceBarricade();
            }
        }

        // Update white barricade position
        if (buildMode && Physics.Raycast(new Ray(cammeraTranform.position, cammeraTranform.forward), out _Hit))
        {
            tempBarricade.transform.position = SnapToGrid(_Hit.point);
             tempBarricade.transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(transform.eulerAngles.y + 90f) != 0 ?
                 Mathf.RoundToInt((transform.eulerAngles.y + 90f) / 90f) * 90f : 0, 0);
        }
    }
public void PlaceBarricade()
{
    if (Physics.Raycast(new Ray(cammeraTranform.position, cammeraTranform.forward), out _Hit))
    {
        GameObject newBarricade = Instantiate(woodenBarricade, _Hit.point, tempBarricade.transform.rotation);
        currAmmoBarricade--;

        Vector3 closestPosition = FindClosestBarricade(newBarricade.transform.position);
        Vector3 direction = (closestPosition - newBarricade.transform.position).normalized;
        float offset = (newBarricade.transform.localScale.x + tempBarricade.transform.localScale.x) * 0.5f;

        Vector3 tempBarricadePosition = closestPosition + direction * offset;
        tempBarricadePosition = SnapToGrid(tempBarricadePosition);
        tempBarricade.transform.position = new Vector3(tempBarricadePosition.x, tempBarricade.transform.position.y, tempBarricadePosition.z);
    }
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
        float snappedX = Mathf.Floor(position.x / gridSize) * gridSize + gridSize / 1f;
        float snappedY = position.y;
        float snappedZ = Mathf.Floor(position.z / gridSize) * gridSize + gridSize / 1f;
        return new Vector3(snappedX, snappedY, snappedZ);
    }

    Vector3 FindClosestBarricade(Vector3 position)
    {
        GameObject[] barricades = GameObject.FindGameObjectsWithTag("Barricade");
        float closestDistance = Mathf.Infinity;
        Vector3 closestPosition = Vector3.zero;

        foreach (GameObject barricade in barricades)
        {
            float distance = Vector3.Distance(position, barricade.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = barricade.transform.position;
            }
        }

        return closestPosition;
    }
}
