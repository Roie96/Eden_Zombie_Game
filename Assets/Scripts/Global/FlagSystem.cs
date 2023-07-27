using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSystem : MonoBehaviour
{
    public static GameObject flagObject;
    public static Terrain terrain;
   

    void Start()
    {
        flagObject = GameObject.FindGameObjectWithTag("Flag");
        if(flagObject== null)
            Debug.LogError("No flagObject found in the scene!");
        terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("No active terrain found!");
        }
        flagObject.transform.position = terrain.terrainData.bounds.center;
        flagObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public static void newRandomFlagLocated(){
        Vector3 terrainCenter = terrain.terrainData.bounds.center;
        Vector3 newPosition = GetRandomTerrainPosition(terrainCenter.x* 1/2, terrainCenter);

        flagObject.transform.position = newPosition;  
    }

    public static Vector3 getFlagPosition()
    {
        return flagObject.transform.position;
    }

     static public Vector3 GetRandomTerrainPosition(float radius, Vector3 center = default(Vector3))
    {
        if (center.Equals(default(Vector3))){
            center = flagObject.transform.position;
        }
        // Get the terrain bounds
        Bounds terrainBounds = terrain.terrainData.bounds;

        // Clamp the center position to the terrain bounds
        center.x = Mathf.Clamp(center.x, terrainBounds.min.x, terrainBounds.max.x);
        center.z = Mathf.Clamp(center.z, terrainBounds.min.z, terrainBounds.max.z);

        // Generate a random point within the radius of the center position
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        Vector3 randomPos = center + new Vector3(randomOffset.x, 0f, randomOffset.y);

        // Get the terrain height at the random point
        float terrainHeight = terrain.SampleHeight(randomPos);
        randomPos.y = terrainHeight;

        return randomPos;
    }

}