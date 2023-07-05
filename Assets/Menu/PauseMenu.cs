using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool hasControlOverMouse = false;

    static public bool isPaused = false;
    public Canvas existingCanvas;
    public Canvas pauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
       pauseCanvas.enabled = false;
       ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
                ResumeGame();
            else
            {
                PauseGame();
            }  
        }
        if (isPaused){
            Cursor.visible = true;
            StartCoroutine(GrantMouseControlCoroutine());
        }
    }

    public void PauseGame(){
        Cursor.visible = true;
        existingCanvas.enabled = false;
        pauseCanvas.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void ResumeGame(){
        Cursor.visible = false;
        existingCanvas.enabled = true;
        pauseCanvas.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
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
