using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOverlap : MonoBehaviour
{
    public bool overlap;
    [SerializeField] public string Tag;

    private void OnTriggerStay(Collider other)
    {  
        if (other.CompareTag(Tag)){  
            overlap = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag)){
            overlap = false;
        }
    }
}