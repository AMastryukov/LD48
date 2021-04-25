using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private WorldManager worldManager;
    private GravityManager gravityManager;
    private AudioManager audioManager;
    private PlayerCamera playerCamera;

    private void Awake()
    {
        worldManager = FindObjectOfType<WorldManager>();
        gravityManager = FindObjectOfType<GravityManager>();
        audioManager = FindObjectOfType<AudioManager>();
        playerCamera = FindObjectOfType<PlayerCamera>();
    }

    private void Start()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        yield return IntroSequence();
        yield return FirstSequence();
        yield return SecondSequence();
        yield return ThirdSequence();

        if (true)
        {
            yield return GameWinSequence();
        }
    }

    private IEnumerator IntroSequence()
    {
        playerCamera.enabled = false;
        playerCamera.FadeCamera(1f, 0f);

        // Play voice lines and wait for them
        yield return new WaitForSeconds(5f);

        playerCamera.FadeCamera(0f, 5f);
        playerCamera.enabled = true;

        // Turn off lights, shake screen, disable gravity

        yield return null;
    }

    private IEnumerator FirstSequence()
    {
        // Trigger Simon Minigame

        yield return null;
    }

    private IEnumerator SecondSequence()
    {
        // Trigger Oxygen Minigame


        // Trigger Simon Minigame


        // Turn off lights, shake screen, disable gravity


        yield return null;
    }

    private IEnumerator ThirdSequence()
    {
        // Trigger Oxygen Minigame


        // Trigger Code Minigame


        // Trigger Simon Minigame


        // Turn off lights, shake screen, disable gravity


        yield return null;
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
}
