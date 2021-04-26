using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Light[] vesselLights;
    [SerializeField] private Transform brokenVesselCamera;
    [SerializeField] private GameObject sparkParticles;
    [SerializeField] private GameObject[] abuelos;

    private GravityManager gravityManager;
    private AudioManager audioManager;
    private PlayerCamera playerCamera;
    private VesselMovement vesselMovement;

    private SimonMinigame simonMinigame;
    private OxygenMinigame oxygenMinigame;
    private CodeMinigame codeMinigame;
    private RadioMinigame radioMinigame;

    private TextMeshProUGUI[] texts;

    Action finishMinigameEvent;
    bool minigameFinished = false;

    private void Awake()
    {
        gravityManager = FindObjectOfType<GravityManager>();
        audioManager = FindObjectOfType<AudioManager>();
        playerCamera = FindObjectOfType<PlayerCamera>();
        vesselMovement = FindObjectOfType<VesselMovement>();

        simonMinigame = FindObjectOfType<SimonMinigame>();
        oxygenMinigame = FindObjectOfType<OxygenMinigame>();
        codeMinigame = FindObjectOfType<CodeMinigame>();
        radioMinigame = FindObjectOfType<RadioMinigame>();

        texts = FindObjectsOfType<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        vesselMovement.SetSpeed(3f);

        //yield return IntroSequence();
        //yield return FirstSequence();
        //yield return SecondSequence();
        yield return ThirdSequence();
        yield return GameWinSequence();

        yield return null;
    }

    private IEnumerator IntroSequence()
    {
        #region Credits and Interview
        // Lock the player controls
        playerCamera.IsLocked = true;
        playerCamera.FadeCamera(1f, 0f);

        Debug.Log("Credits roll while interview plays in the background");

        yield return new WaitForSeconds(5f);
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
        vesselMovement.SetSpeed(25f, 1f);
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();

        yield return new WaitForSeconds(5f);

        vesselMovement.SetSpeed(3f, 1f);
        gravityManager.EnableGravity();
        playerCamera.ShakeCamera(1f, 0.1f);

        yield return new WaitForSeconds(0.5f);

        DisableLights();

        yield return new WaitForSeconds(2f);
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
        vesselMovement.SetSpeed(25f, 1f);
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();

        yield return new WaitForSeconds(5f);

        vesselMovement.SetSpeed(3f, 1f);
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
        vesselMovement.SetSpeed(25f, 1f);
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();

        yield return new WaitForSeconds(5f);

        vesselMovement.SetSpeed(3f, 1f);
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

        Debug.Log("Send me the detailed diagnostic encoded data! Code minigame");

        codeMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        CodeMinigame.OnMinigameFinished += finishMinigameEvent;

        while (!minigameFinished)
        {
            yield return null;
        }

        CodeMinigame.OnMinigameFinished -= finishMinigameEvent;

        Debug.Log("I'm receiving the diagnostics. It's decrypting...");

        yield return new WaitForSeconds(2f);
        #endregion

        #region Radio Minigame
        EnableLights();

        Debug.Log("Oh my God... You have to - *breaking up*! Fix- the -- radio!");

        radioMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        RadioMinigame.OnMinigameFinished += finishMinigameEvent;

        while (!minigameFinished)
        {
            yield return null;
        }

        RadioMinigame.OnMinigameFinished -= finishMinigameEvent;

        yield return new WaitForSeconds(5f);
        #endregion

        #region Final Blackout
        Debug.Log("Listen to me! There is something outside of the ship! You must head for the surface IMMEDIATELY!");

        // Turn off lights, shake screen, disable gravity
        vesselMovement.SetSpeed(25f, 1f);
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();

        yield return new WaitForSeconds(5f);

        vesselMovement.SetSpeed(3f, 1f);
        gravityManager.EnableGravity();
        playerCamera.ShakeCamera(0.5f, 0.25f);

        yield return new WaitForSeconds(0.5f);

        DisableLights();

        yield return new WaitForSeconds(0.25f);
        #endregion
    }

    private IEnumerator GameWinSequence()
    {
        Debug.Log("Loud crash, you are in the pit");

        // Teleport player camera and lock it
        vesselMovement.SetSpeed(0f);
        playerCamera.FadeCamera(1f, 0f);
        playerCamera.IsLocked = true;
        playerCamera.transform.position = brokenVesselCamera.transform.position;
        playerCamera.transform.rotation = brokenVesselCamera.transform.rotation;
        playerCamera.transform.SetParent(brokenVesselCamera);
        playerCamera.WaveCamera(100f, 2f);

        yield return new WaitForSeconds(3f);

        Debug.Log("You only see the light in the front of the vessel. Interior lights are out.");

        PlaySpark();

        yield return new WaitForSeconds(3f);

        playerCamera.FadeCamera(1f, 1f);

        PlaySpark();

        yield return new WaitForSeconds(2f);

        // Blink and show aliens
        for (int i = 0; i < abuelos.Length; i++)
        {
            abuelos[i].SetActive(true);
            playerCamera.FadeCamera(0f, 0.25f);

            yield return new WaitForSeconds(4f - i);

            if (i == abuelos.Length - 1)
            {
                playerCamera.FadeCamera(1f, 0f);
            }
            else
            {
                playerCamera.FadeCamera(1f, 1f);
            }

            yield return new WaitForSeconds(2f);
        }

        // Play soundtrack
        audioManager.PlayEndCredits();

        yield return new WaitForSeconds(audioManager.EndCreditsSongLength);

        SceneManager.LoadScene("Main Menu");
    }

    private void PlaySpark()
    {
        // Play spark particle
        playerCamera.FadeCamera(0f, 1f);
        sparkParticles.GetComponent<ParticleSystem>().Play();
        sparkParticles.GetComponent<AudioSource>().Play();
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
