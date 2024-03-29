using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI; //reference to the pause menu UI

    void Update()
    {
        //check for input to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    //function to pause the game
    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; //stop time to pause the game
        pauseMenuUI.SetActive(true); //activate the pause menu UI
    }

    //function to resume the game
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; //resume time to resume the game
        pauseMenuUI.SetActive(false); //feactivate the pause menu UI
    }
}