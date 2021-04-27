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
    private Credits credits;

    private SimonMinigame simonMinigame;
    private OxygenMinigame oxygenMinigame;
    private CodeMinigame codeMinigame;
    private RadioMinigame radioMinigame;

    private TextMeshProUGUI[] texts;

    private float[] vesselLightsIntensities;

    Action finishMinigameEvent;
    bool minigameFinished = false;

    private void Awake()
    {
        gravityManager = FindObjectOfType<GravityManager>();
        audioManager = FindObjectOfType<AudioManager>();
        playerCamera = FindObjectOfType<PlayerCamera>();
        vesselMovement = FindObjectOfType<VesselMovement>();
        credits = FindObjectOfType<Credits>();

        simonMinigame = FindObjectOfType<SimonMinigame>();
        oxygenMinigame = FindObjectOfType<OxygenMinigame>();
        codeMinigame = FindObjectOfType<CodeMinigame>();
        radioMinigame = FindObjectOfType<RadioMinigame>();

        texts = FindObjectsOfType<TextMeshProUGUI>();

        vesselLightsIntensities = new float[vesselLights.Length];
        for (int i = 0; i < vesselLights.Length; i++)
        {
            vesselLightsIntensities[i] = vesselLights[i].intensity;
        }
    }

    private void Start()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        vesselMovement.SetSpeed(3f);

        yield return IntroSequence();
        yield return FirstSequence();
        yield return SecondSequence();
        yield return ThirdSequence();
        yield return GameWinSequence();

        yield return null;
    }

    private IEnumerator IntroSequence()
    {
        #region Entry Point
        // Lock the player controls
        playerCamera.IsLocked = true;
        playerCamera.FadeCamera(1f, 0f);

        audioManager.PlayShipAmbience(0);

        yield return new WaitForSeconds(2);

        yield return audioManager.WaitForVoiceline(0);
        #endregion

        #region Introductory Voice Lines
        // Unlock player controls
        playerCamera.FadeCamera(0f, 2f);
        playerCamera.IsLocked = false;

        // Let the player fuck around in the ship for a bit
        yield return new WaitForSeconds(5f);

        yield return audioManager.WaitForVoiceline(1);
        #endregion

        #region First Blackout
        // Turn off lights, shake screen, disable gravity
        vesselMovement.SetSpeed(25f, 1f);
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();
        DimLights();

        audioManager.PlayShipCrash();

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
        audioManager.PlayShipAmbience(1);

        yield return new WaitForSeconds(2f);
        yield return audioManager.WaitForVoiceline(2);

        simonMinigame.StartMinigame();

        #region Wait For Door Open
        bool doorOpened = false;

        Action doorOpenEvent = () => 
        {
            doorOpened = true;
        };

        SimonMinigame.OnDoorsOpened += doorOpenEvent;

        while (!doorOpened)
        { 
            yield return null; 
        }

        SimonMinigame.OnDoorsOpened -= finishMinigameEvent;

        #endregion

        #region Wait for Minigame Completion
        minigameFinished = false;

        finishMinigameEvent = () => 
        { 
            minigameFinished = true;
        };

        SimonMinigame.OnMinigameFinished += finishMinigameEvent;

        // This has to be after the event is subscribned to, or else it will break
        yield return audioManager.WaitForVoiceline(3);

        while (!minigameFinished)
        { 
            yield return null; 
        }

        SimonMinigame.OnMinigameFinished -= finishMinigameEvent;

        #endregion

        yield return audioManager.WaitForVoiceline(4);
        #endregion

        #region Second Blackout
        // Turn off lights, shake screen, disable gravity
        vesselMovement.SetSpeed(25f, 1f);
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();
        DimLights();

        audioManager.PlayShipCrash();

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
        audioManager.PlayShipAmbience(2);

        oxygenMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        OxygenMinigame.OnMinigameFinished += finishMinigameEvent;

        // Has to be after event subscribtion to minigame
        yield return new WaitForSeconds(2f);
        yield return audioManager.WaitForVoiceline(5);

        while (!minigameFinished)
        {
            yield return null;
        }

        OxygenMinigame.OnMinigameFinished -= finishMinigameEvent;
        #endregion

        #region Simon Minigame
        simonMinigame.StartMinigame(8);

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        SimonMinigame.OnMinigameFinished += finishMinigameEvent;

        // Has to be after the event subscription to minigame
        yield return audioManager.WaitForVoiceline(6);

        while (!minigameFinished)
        {
            yield return null;
        }

        SimonMinigame.OnMinigameFinished -= finishMinigameEvent;

        yield return audioManager.WaitForVoiceline(7);
        #endregion

        #region Third Blackout
        // Turn off lights, shake screen, disable gravity
        vesselMovement.SetSpeed(25f, 1f);
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();
        DimLights();

        audioManager.PlayShipCrash();

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

        codeMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        CodeMinigame.OnMinigameFinished += finishMinigameEvent;

        yield return new WaitForSeconds(2f);
        yield return audioManager.WaitForVoiceline(8);

        while (!minigameFinished)
        {
            yield return null;
        }

        CodeMinigame.OnMinigameFinished -= finishMinigameEvent;
        #endregion

        #region Radio Minigame
        EnableLights();

        radioMinigame.StartMinigame();

        minigameFinished = false;

        finishMinigameEvent = () =>
        {
            minigameFinished = true;
        };

        RadioMinigame.OnMinigameFinished += finishMinigameEvent;

        yield return audioManager.WaitForVoiceline(9);

        while (!minigameFinished)
        {
            yield return null;
        }

        RadioMinigame.OnMinigameFinished -= finishMinigameEvent;
        #endregion

        #region Final Blackout
        yield return audioManager.WaitForVoiceline(10);

        // Turn off lights, shake screen, disable gravity
        vesselMovement.SetSpeed(25f, 1f);
        playerCamera.ShakeCamera(5, 0.02f);
        gravityManager.DisableGravity();
        DimLights();

        audioManager.PlayShipCrash();

        yield return new WaitForSeconds(5f);

        vesselMovement.SetSpeed(3f, 1f);
        gravityManager.EnableGravity();
        playerCamera.ShakeCamera(0.5f, 0.25f);

        yield return new WaitForSeconds(0.5f);

        DisableLights();

        yield return new WaitForSeconds(5f);
        #endregion
    }

    private IEnumerator GameWinSequence()
    {
        audioManager.PlayShipAmbience(3);

        // Teleport player camera and lock it
        vesselMovement.SetSpeed(0f);
        playerCamera.FadeCamera(1f, 0f);
        playerCamera.IsLocked = true;
        playerCamera.transform.position = brokenVesselCamera.transform.position;
        playerCamera.transform.rotation = brokenVesselCamera.transform.rotation;
        playerCamera.transform.SetParent(brokenVesselCamera);
        playerCamera.WaveCamera(100f, 2f);

        yield return new WaitForSeconds(3f);

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
        audioManager.StopShipAmbience();
        audioManager.PlayEndCredits();

        yield return new WaitForSeconds(2f);
        credits.ShowCredits(audioManager.EndCreditsSongLength - 2f);
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

        ResetLightIntensity();
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

    private void DimLights()
    {
        for (int i = 0; i < vesselLights.Length; i++)
        {
            vesselLights[i].intensity = vesselLightsIntensities[i] / 2f;
        }
    }

    private void ResetLightIntensity()
    {
        for (int i = 0; i < vesselLights.Length; i++)
        {
            vesselLights[i].intensity = vesselLightsIntensities[i];
        }
    }
}
