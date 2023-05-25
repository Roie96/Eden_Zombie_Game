using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashBaricade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {  
        if (other.CompareTag("Zombie"))
        {
            Vector3 pushDirection = other.transform.position - transform.position;
            pushDirection.y = 0f; // Optionally, set to 0 if you don't want vertical movement
            pushDirection.Normalize();
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            playerRigidbody.AddForceAtPosition(pushDirection * 80, other.transform.position ,ForceMode.Impulse);
        }
    }
}
