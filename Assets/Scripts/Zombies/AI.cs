using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IL3DN;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public IL3DN_SimpleFPSController FPSController;
    
    public float viewAngle = 120f;
    public float viewDistance = 10f;
    private bool isAware = false;
    private NavMeshAgent agent;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public virtual void Update()
    {
        if(getIsAware()){
            // Chase
            agent.SetDestination(FPSController.transform.position);
            
        }
        else{
            searchForPlayer();
        }
    }

    public void searchForPlayer(){
        if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(FPSController.transform.position)) < viewAngle / 2f){
            if(Vector3.Distance(FPSController.transform.position, transform.position) < viewDistance){
                setToAware();
            }
        }
    }

    public bool getIsAware(){
        return isAware;
    }

    public void setToAware(){
        Debug.Log("Zombie Chase!");
        isAware = true;
    }

    public void setToUnAware(){
        isAware = false;
    }

}
