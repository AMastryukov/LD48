using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerCamera playerCamera;
    private Canvas canvas;

    private bool isPaused = false;

    private void Awake()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
        canvas = GetComponent<Canvas>();
    }

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
            DisablePauseUI();
            playerCamera.enabled = true;

            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 1f;
        }
        else
        {
            EnablePauseUI();
            playerCamera.enabled = false;

            Time.timeScale = 0.01f;
            Time.fixedDeltaTime = Time.timeScale * 0.01f;
        }


        isPaused = !isPaused;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void EnablePauseUI()
    {
        canvas.enabled = true;
    }

    private void DisablePauseUI()
    {
        canvas.enabled = false;
    }
}
