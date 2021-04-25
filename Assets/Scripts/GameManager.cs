using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Light[] vesselLights;

    private WorldManager worldManager;
    private GravityManager gravityManager;
    private AudioManager audioManager;
    private PlayerCamera playerCamera;

    private SimonMinigame simonMinigame;
    private OxygenMinigame oxygenMinigame;
    private CodeMinigame codeMinigame;

    private TextMeshProUGUI[] texts;

    Action finishMinigameEvent;
    bool minigameFinished = false;

    private void Awake()
    {
        worldManager = FindObjectOfType<WorldManager>();
        gravityManager = FindObjectOfType<GravityManager>();
        audioManager = FindObjectOfType<AudioManager>();
        playerCamera = FindObjectOfType<PlayerCamera>();

        simonMinigame = FindObjectOfType<SimonMinigame>();
        oxygenMinigame = FindObjectOfType<OxygenMinigame>();
        codeMinigame = FindObjectOfType<CodeMinigame>();

        texts = FindObjectsOfType<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        //yield return IntroSequence();
        //yield return FirstSequence();
        yield return SecondSequence();
        yield return ThirdSequence();

        if (true)
        {
            yield return GameWinSequence();
        }
    }

    private IEnumerator IntroSequence()
    {
        #region Credits and Interview
        // Lock the player controls
        playerCamera.IsLocked = true;
        playerCamera.FadeCamera(1f, 0f);

        Debug.Log("Credits roll while interview plays in the background");

        yield return new WaitForSeconds(2f);
        #endregion

        #region Introductory Voice Lines
        // Unlock player controls
        playerCamera.FadeCamera(0f, 2f);
        playerCamera.IsLocked = false;

        Debug.Log("You are descending, everything seems good");

        yield return new WaitForSeconds(5f);
        #endregion

        #region First Blackout
        Debug.Log("What's happening?! Why are you descending so fast?");

        // Turn off lights, shake screen, disable gravity
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();

        yield return new WaitForSeconds(5f);

        gravityManager.EnableGravity();
        playerCamera.ShakeCamera(1f, 0.1f);

        yield return new WaitForSeconds(0.5f);

        DisableLights();

        yield return new WaitForSeconds(3f);
        #endregion
    }

    private IEnumerator FirstSequence()
    {
        #region Simon Minigame
        EnableLights();

        Debug.Log("You should run diagnostics (Simon Game). The panel is behind you.");

        simonMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () => 
        { 
            minigameFinished = true;
        };

        SimonMinigame.OnMinigameFinished += finishMinigameEvent;

        while (!minigameFinished)
        { 
            yield return null; 
        }

        SimonMinigame.OnMinigameFinished -= finishMinigameEvent;

        Debug.Log("Everything seems to be fine.");

        yield return new WaitForSeconds(2f);
        #endregion

        #region Second Blackout
        Debug.Log("AGAIN? What is happening!?");

        // Turn off lights, shake screen, disable gravity
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();

        yield return new WaitForSeconds(5f);

        gravityManager.EnableGravity();
        playerCamera.ShakeCamera(1f, 0.1f);

        yield return new WaitForSeconds(0.5f);

        DisableLights();

        yield return new WaitForSeconds(3f);
        #endregion
    }

    private IEnumerator SecondSequence()
    {
        #region Oxygen Minigame
        EnableLights();

        Debug.Log("Do the Oxygen Minigame. Check the front panel for information and spin the valves to match the orientations.");

        oxygenMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        OxygenMinigame.OnMinigameFinished += finishMinigameEvent;

        while (!minigameFinished)
        {
            yield return null;
        }

        OxygenMinigame.OnMinigameFinished -= finishMinigameEvent;

        Debug.Log("Oxygen is back up.");

        yield return new WaitForSeconds(2f);
        #endregion

        #region Simon Minigame
        EnableLights();

        Debug.Log("Simon Game again. You already know how this is done.");

        simonMinigame.StartMinigame(8);

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        SimonMinigame.OnMinigameFinished += finishMinigameEvent;

        while (!minigameFinished)
        {
            yield return null;
        }

        SimonMinigame.OnMinigameFinished -= finishMinigameEvent;

        Debug.Log("Everything seems to be fine.");

        yield return new WaitForSeconds(2f);
        #endregion

        #region Third Blackout
        Debug.Log("What is that? Did you see it? Some formation... or... something outside the vessel!");

        // Turn off lights, shake screen, disable gravity
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();

        yield return new WaitForSeconds(5f);

        gravityManager.EnableGravity();
        playerCamera.ShakeCamera(1f, 0.1f);

        yield return new WaitForSeconds(0.5f);

        DisableLights();

        yield return new WaitForSeconds(3f);
        #endregion
    }

    private IEnumerator ThirdSequence()
    {
        #region Code Minigame
        EnableLights();

        Debug.Log("Code minigame time.");

        oxygenMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        OxygenMinigame.OnMinigameFinished += finishMinigameEvent;

        while (!minigameFinished)
        {
            yield return null;
        }

        OxygenMinigame.OnMinigameFinished -= finishMinigameEvent;

        Debug.Log("Oxygen is back up.");

        yield return new WaitForSeconds(2f);
        #endregion

        #region Oxygen Minigame
        EnableLights();

        Debug.Log("Do the Oxygen Minigame. Check the front panel for information and spin the valves to match the orientations.");

        oxygenMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        OxygenMinigame.OnMinigameFinished += finishMinigameEvent;

        while (!minigameFinished)
        {
            yield return null;
        }

        OxygenMinigame.OnMinigameFinished -= finishMinigameEvent;

        Debug.Log("Oxygen is back up.");

        yield return new WaitForSeconds(2f);
        #endregion

        #region Simon Minigame
        EnableLights();

        Debug.Log("Simon Game again. You already know how this is done.");

        simonMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        SimonMinigame.OnMinigameFinished += finishMinigameEvent;

        while (!minigameFinished)
        {
            yield return null;
        }

        SimonMinigame.OnMinigameFinished -= finishMinigameEvent;

        Debug.Log("Everything seems to be fine.");

        yield return new WaitForSeconds(2f);
        #endregion

        #region Third Blackout
        Debug.Log("What is that? Did you see it? Some formation... or... something outside the vessel!");

        // Turn off lights, shake screen, disable gravity
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();

        yield return new WaitForSeconds(5f);

        gravityManager.EnableGravity();
        playerCamera.ShakeCamera(1f, 0.1f);

        yield return new WaitForSeconds(0.5f);

        DisableLights();

        yield return new WaitForSeconds(3f);
        #endregion
    }

    private IEnumerator GameWinSequence()
    {
        

        yield return null;
    }

    private IEnumerator GameFailSequence()
    {
        // Restart scene

        yield return null;
    }

    private void EnableLights()
    {
        for (int i = 0; i < vesselLights.Length; i++)
        {
            vesselLights[i].enabled = true;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].enabled = true;
        }
    }

    private void DisableLights()
    {
        for (int i = 0; i < vesselLights.Length; i++)
        {
            vesselLights[i].enabled = false;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].enabled = false;
        }
    }
}
