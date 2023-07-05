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

    private bool hasControlOverMouse = false;

    public void Setup(int score)
    {
        Cursor.visible = true;
        existingCanvas.enabled = false;
        pauseCanvas.enabled = false;
        gameObject.SetActive(true);
        scoreText.text = "SCORE: ROUND " + score.ToString();
        Time.timeScale = 0f;

        StartCoroutine(GrantMouseControlCoroutine());
    }

private IEnumerator GrantMouseControlCoroutine()
{
    yield return null; // Wait for one frame to ensure UI elements are properly rendered

    hasControlOverMouse = true;

    while (Cursor.lockState != CursorLockMode.None)
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        yield return null; // Wait for one frame
    }
}

    void Update()
    {
        if (hasControlOverMouse)
        {
            Cursor.visible = true;
        }

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
