using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public RectTransform reticle;

    public Rigidbody rg;
    public float restingSize = 75;
    public float maxSize = 250;
    public float speed = 1;
    private float currentSize;
    void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rg = GetComponent<Rigidbody>();
        if(isMoving){
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        }
        else{
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);
        
    }

    public RectTransform getReticle()
    {
        return reticle;
    }

    bool isMoving{
        get{
            if(Input.GetAxis("Horizontal") != 0 ||
            Input.GetAxis("Vertical") != 0 ||
            Input.GetAxis("Mouse X") != 0 ||
            Input.GetAxis("Mouse Y") != 0)
            return true;
            else
                return false;
        }
    }

    
}
