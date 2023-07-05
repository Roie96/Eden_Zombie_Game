using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private bool hasControlOverMouse = false;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.visible = true;

        StartCoroutine(GrantMouseControlCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (hasControlOverMouse)
        {
            Cursor.visible = true;
            StartCoroutine(GrantMouseControlCoroutine());
        }
        
    }

private IEnumerator GrantMouseControlCoroutine(){
    yield return null; // Wait for one frame to ensure UI elements are properly rendered

    hasControlOverMouse = true;

    while (Cursor.lockState != CursorLockMode.None)
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        yield return null; // Wait for one frame
    }
}
}

