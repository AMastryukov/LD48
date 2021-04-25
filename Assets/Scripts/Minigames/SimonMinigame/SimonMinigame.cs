using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimonMinigame : BaseMinigame
{
    [SerializeField] private List<GameObject> sequenceLights;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Transform[] doors;

    private int sequenceLength = 5;
    private bool acceptInput = false;
    private List<int> sequenceIDs = new List<int>();
    private int currentInputIndex = 0;
    private Color[] lightColors = { Color.green, Color.yellow, Color.red };

    private void Awake()
    {
        for (int i = 0; i < sequenceLights.Count; i++)
        {
            sequenceLights[i].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColors[i]);
            sequenceLights[i].GetComponent<Light>().color = lightColors[i];
            sequenceLights[i].SetActive(false);
        }
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
        Debug.Log("[SIMON MINIGAME] Win");

        StartCoroutine(CloseDoorsCoroutine());

        base.FinishMinigame();
    }

    private void SetupMinigame()
    {
        acceptInput = false;

        sequenceIDs.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            sequenceIDs.Add(UnityEngine.Random.Range(0, 3));
        }

        StartCoroutine(OpenDoorsCoroutine());
        StartCoroutine(DisplaySequence());
    }

    private IEnumerator DisplaySequence()
    {
        yield return new WaitForSeconds(1f);

        // Flash the little things in sequence to show the player what the order is
        for(int i = 0; i < sequenceIDs.Count; i++)
        {
            feedbackText.text = (i + 1).ToString();

            sequenceLights[(int)sequenceIDs[i]].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.65f);
            sequenceLights[(int)sequenceIDs[i]].gameObject.SetActive(false);

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
    }

    private IEnumerator CloseDoorsCoroutine()
    {
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
    }
}
