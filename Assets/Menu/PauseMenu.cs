using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
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
            PauseGame();
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
}
