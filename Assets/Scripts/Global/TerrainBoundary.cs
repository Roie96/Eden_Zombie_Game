using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TerrainBoundary : MonoBehaviour
{
    private Terrain terrain;
    private Bounds terrainBounds;
    
    private void Start()
    {
        // Get a reference to the terrain in your scene
        terrain = Terrain.activeTerrain;

        // Calculate the boundary of the terrain
        CalculateTerrainBounds();
    }

    private void CalculateTerrainBounds()
    {
        // Get the terrain's collider bounds to represent the terrain boundary
        terrainBounds = terrain.GetComponent<Collider>().bounds;
    }

    private void Update()
    {
        // Check the position of the player or any other game object
        // and ensure it stays within the terrain boundary

        // For example, assuming you have a player GameObject with a Rigidbody attached
        // Make sure to replace "player" with the actual name of your player GameObject.

        GameObject player = GameObject.Find("Player"); // Replace "Player" with your player's GameObject name
        
        if (player != null)
        {
            // Get the position of the player
            Vector3 playerPosition = player.transform.position;

            // Constrain the player within the terrain bounds
            float x = Mathf.Clamp(playerPosition.x, terrainBounds.min.x, terrainBounds.max.x);
            float y = Mathf.Clamp(playerPosition.y, terrainBounds.min.y, terrainBounds.max.y);
            float z = Mathf.Clamp(playerPosition.z, terrainBounds.min.z, terrainBounds.max.z);

            // Set the new constrained position for the player
            player.transform.position = new Vector3(x, y, z);
        }
    }
}