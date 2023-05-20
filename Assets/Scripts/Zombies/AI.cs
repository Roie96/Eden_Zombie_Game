using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IL3DN;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform playerTransform;
    
    public float viewAngle = 120f;
    public float viewDistance = 10f;
    private bool isAware = false;
    protected NavMeshAgent agent;
    private Renderer renderer;

    private Vector3 randomPointToWalk;

    protected float walkSpeed;
    protected float runSpeed;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        renderer = GetComponent<Renderer>();
        CreateRandomPoint();
    }
    public virtual void Update()
    {
        if(getIsAware()){
            // Chase
            agent.SetDestination(playerTransform.position);
            renderer.material.color = Color.red;
        }
        else{
            agent.speed = walkSpeed;
            searchForPlayer();
        }
    }

    public void searchForPlayer(){
        //find player
        if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(playerTransform.position)) < viewAngle / 2f){
            if(Vector3.Distance(playerTransform.position, transform.position) < viewDistance){
                setToAware();
            }
        }

        // keep walking
        
        else if((transform.position - randomPointToWalk).magnitude < 1f){
            CreateRandomPoint();         
        }
        if(agent.isOnNavMesh)
            agent.SetDestination(randomPointToWalk);
    }

    private void CreateRandomPoint(){
        int xPos = (int)Mathf.Round(playerTransform.position.x - 100f / 2 + Random.Range(0, 100f));
        int zPos = (int)Mathf.Round(playerTransform.position.z - 100f / 2 + Random.Range(0, 100f));
        Vector3 position = new Vector3(xPos, 0, zPos);
        NavMeshHit hit;

        // Sample the position on the NavMesh to find a valid y coordinate
        if (NavMesh.SamplePosition(position, out hit, 50f, NavMesh.AllAreas))
        {
            position = hit.position;
        }
        randomPointToWalk = position;

        randomPointToWalk = FlagSystem.GetRandomTerrainPosition(50);
    }

    public bool getIsAware(){
        return isAware;
    }

    public void setToAware(){
        isAware = true;
        agent.speed = runSpeed;
    }

    public void setToUnAware(){
        isAware = false;
    }

    public void setAngleView(float angle){
        viewAngle = angle;
    }

    public void setDistanceView(float distance){
        distance = viewDistance;
    }

    public void setSpeed(float newSpeed){
        runSpeed = newSpeed;
        walkSpeed = newSpeed/4;
    }

}