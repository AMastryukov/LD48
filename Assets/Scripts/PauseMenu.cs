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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
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
            SetTimeScale(1f);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerCamera.enabled = true;
        }
        else
        {
            EnablePauseUI();
            SetTimeScale(0.02f);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerCamera.enabled = false;
        }

        isPaused = !isPaused;
    }

    public void GoToMainMenu()
    {
        SetTimeScale(1f);
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

    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
