using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    static public bool isPaused = false;
    public Canvas existingCanvas;
    public Canvas pauseCanvas;
    // public GameObject optionsMenu;
    // public GameObject pauseMenuUI;


    // Start is called before the first frame update
    void Start()
    {
       pauseCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
     {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if (PauseMenu.isPaused)
            // {
            //     ResumeGame();
            // }
            // else
            // {
                PauseGame();
            // }
        }
    }

    public void PauseGame(){
        existingCanvas.enabled = false;
        pauseCanvas.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void ResumeGame(){
        existingCanvas.enabled = true;
        pauseCanvas.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    // public void LoadOptions(){
    //    optionsMenu.SetActive(true);
    //    pauseMenuUI.SetActive(false);

    // }

    //  public void LoadPauseMenu(){
    //    optionsMenu.SetActive(false);
    //    pauseMenuUI.SetActive(true);

    // }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
