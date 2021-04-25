using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GravityManager gravityManager;
    private AudioManager audioManager;
    private PlayerCamera playerCamera;

    private void Awake()
    {
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
        yield return null;
    }

    private IEnumerator FirstSequence()
    {
        yield return null;
    }

    private IEnumerator SecondSequence()
    {
        yield return null;
    }

    private IEnumerator ThirdSequence()
    {
        yield return null;
    }

    private IEnumerator GameWinSequence()
    {
        yield return null;
    }

    private IEnumerator GameFailSequence()
    {
        yield return null;
    }
}
