using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimonMinigame : BaseMinigame
{
    public static Action OnDoorsOpened;

    [SerializeField] private Animator leverAnimator;
    [SerializeField] private GameObject knob;
    [SerializeField] private GameObject button;
    [SerializeField] private List<GameObject> sequenceLights;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Transform[] doors;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;

    private int sequenceLength = 5;
    private bool acceptInput = false;
    private List<int> sequenceIDs = new List<int>();
    private int currentInputIndex = 0;
    private Color[] lightColors = { Color.green, Color.yellow, Color.red };
    private bool doorsOpen = false;
    private bool isPressed = false;
    private bool isTurning = false;
    private AudioSource src;

    private void Awake()
    {
        for (int i = 0; i < sequenceLights.Count; i++)
        {
            sequenceLights[i].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            sequenceLights[i].GetComponent<Light>().color = lightColors[i];
            sequenceLights[i].GetComponent<Light>().enabled = false;
        }
        src = GetComponent<AudioSource>();
    }

    public void StartMinigame(int difficulty)
    {
        sequenceLength = difficulty;
        StartMinigame();
    }

    public override void StartMinigame()
    {
        SetupMinigame();
        base.StartMinigame();
    }

    public override void FinishMinigame()
    {
        feedbackText.text = "EZ";

        StartCoroutine(CloseDoorsCoroutine());

        base.FinishMinigame();
    }

    private void SetupMinigame()
    {
        currentInputIndex = 0;
        acceptInput = false;

        sequenceIDs.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            sequenceIDs.Add(UnityEngine.Random.Range(0, 3));
        }
    }

    public void PlayerOpenDoors()
    {
        if (doorsOpen || !isActive) return;
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        yield return OpenDoorsCoroutine();
        yield return DisplaySequence();
    }

    private IEnumerator DisplaySequence()
    {
        yield return new WaitForSeconds(1f);

        // Flash the little things in sequence to show the player what the order is
        for(int i = 0; i < sequenceIDs.Count; i++)
        {
            feedbackText.text = (i + 1).ToString();

            sequenceLights[sequenceIDs[i]].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColors[sequenceIDs[i]]);
            sequenceLights[sequenceIDs[i]].GetComponent<Light>().enabled = true;

            yield return new WaitForSeconds(0.65f);

            sequenceLights[sequenceIDs[i]].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            sequenceLights[sequenceIDs[i]].GetComponent<Light>().enabled = false;

            yield return new WaitForSeconds(0.15f);
        }

        UpdateFeedback();
        acceptInput = true;
    }

    private void UpdateFeedback()
    {
        feedbackText.text = (sequenceIDs.Count - currentInputIndex).ToString();
    }

    public void RegisterInput(int inputID)
    {
        if (!acceptInput || !isActive) return;

        if (inputID == 2) { leverAnimator.Play(0); }
        else if (inputID == 1 && !isPressed)
        {
            StartCoroutine(AnimateButton());
        }
        else if (inputID == 0 && !isTurning)
        {
            StartCoroutine(AnimateKnob());
        }

        src.clip = click;
        src.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        src.Play();

        if (inputID == sequenceIDs[currentInputIndex])
        {
            if (currentInputIndex == sequenceIDs.Count - 1)
            {
                FinishMinigame();
                return;
            }

            currentInputIndex++;
        }
        else
        {
            acceptInput = false;
            currentInputIndex = 0;

            feedbackText.text = "X";

            StartCoroutine(DisplaySequence());

            return;
        }

        UpdateFeedback();
    }

    private IEnumerator OpenDoorsCoroutine()
    {
        src.clip = doorOpen;
        src.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        src.Play();

        Quaternion fromLeft = doors[0].transform.rotation;
        Quaternion fromRight = doors[1].transform.rotation;
        Quaternion toLeft = doors[0].transform.rotation * Quaternion.Euler(Vector3.forward * 130f);
        Quaternion toRight = doors[1].transform.rotation * Quaternion.Euler(Vector3.forward * -130f);

        float duration = 1f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            doors[0].transform.rotation = Quaternion.Slerp(fromLeft, toLeft, timeElapsed / duration);
            doors[1].transform.rotation = Quaternion.Slerp(fromRight, toRight, timeElapsed / duration);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        doorsOpen = true;

        OnDoorsOpened?.Invoke();
    }

    private IEnumerator CloseDoorsCoroutine()
    {
        src.clip = doorClose;
        src.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        src.Play();

        Quaternion fromLeft = doors[0].transform.rotation;
        Quaternion fromRight = doors[1].transform.rotation;
        Quaternion toLeft = doors[0].transform.rotation * Quaternion.Euler(Vector3.forward * -130f);
        Quaternion toRight = doors[1].transform.rotation * Quaternion.Euler(Vector3.forward * 130f);

        float duration = 1f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            doors[0].transform.rotation = Quaternion.Slerp(fromLeft, toLeft, timeElapsed / duration);
            doors[1].transform.rotation = Quaternion.Slerp(fromRight, toRight, timeElapsed / duration);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        doorsOpen = false;
    }

    private IEnumerator AnimateButton()
    {
        isPressed = true;
        Vector3 original = button.transform.localPosition;
        button.transform.localPosition = original - Vector3.up * 0.03f;
        yield return new WaitForSeconds(0.2f);
        button.transform.localPosition = original;
        isPressed = false;
    }

    private IEnumerator AnimateKnob()
    {
        isTurning = true;
        Quaternion from = knob.transform.localRotation;
        Quaternion to = knob.transform.localRotation * Quaternion.Euler(Vector3.up * -90f);

        float duration = 0.5f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            knob.transform.localRotation = Quaternion.Slerp(from, to, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        knob.transform.localRotation = to;
        isTurning = false;
    }
}
