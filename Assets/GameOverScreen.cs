using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Canvas existingCanvas;
    public Canvas pauseCanvas;
    
    
    public void Setup(int score){
        Cursor.visible = true;
        existingCanvas.enabled = false;
        pauseCanvas.enabled = false;
        gameObject.SetActive(true);
        scoreText.text = "SCORE: ROUND " + score.ToString();
        Time.timeScale = 0f;
    }

    void Update()
     {
        Cursor.visible = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            existingCanvas.enabled = false;
            pauseCanvas.enabled = false;
        }
    }

  
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

   public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }     
}

