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
    private NavMeshAgent agent;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public virtual void Update()
    {
        if(getIsAware()){
            // Chase
            agent.SetDestination(playerTransform.position);
            
        }
        else{
            searchForPlayer();
        }
    }

    public void searchForPlayer(){
        if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(playerTransform.position)) < viewAngle / 2f){
            if(Vector3.Distance(playerTransform.position, transform.position) < viewDistance){
                setToAware();
            }
        }
    }

    public bool getIsAware(){
        return isAware;
    }

    public void setToAware(){
        isAware = true;
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

}
