using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;

    private void Update()
    {
        PollPauseInput();
    }

    private void PollPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Debug.Log("Game Unpaused");
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 1f;
        }
        else
        {
            Debug.Log("Game Paused");
            Time.timeScale = 0.01f;
            Time.fixedDeltaTime = Time.timeScale * 0.01f;
        }


        isPaused = !isPaused;
    }
}
