using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimonMinigame : BaseMinigame
{
    public enum InputType { Lever, Button, Valve }

    [SerializeField] private List<GameObject> sequenceLights;
    [SerializeField] private TextMeshProUGUI feedbackText;

    private List<InputType> sequence = new List<InputType>();
    private int currentInputIndex = 0;

    private void Start()
    {
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
        base.FinishMinigame();
    }

    private void SetupMinigame()
    {
        isActive = false;

        int sequenceLength = 5;
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add((InputType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(InputType)).Length));
        }

        StartCoroutine(DisplaySequence());
    }

    private IEnumerator DisplaySequence()
    {
        yield return new WaitForSeconds(0.5f);

        // Flash the little things in sequence to show the player what the order is
        for(int i = 0; i < sequence.Count; i++)
        {
            feedbackText.text = (i + 1).ToString();

            sequenceLights[(int)sequence[i]].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.65f);
            sequenceLights[(int)sequence[i]].gameObject.SetActive(false);

            yield return new WaitForSeconds(0.15f);
        }

        UpdateFeedback();
        isActive = true;
    }

    private void UpdateFeedback()
    {
        feedbackText.text = (sequence.Count - currentInputIndex).ToString();
    }

    private void RegisterInput(InputType input)
    {
        if (!isActive) return;

        if (input == sequence[currentInputIndex])
        {
            if (currentInputIndex == sequence.Count - 1)
            {
                FinishMinigame();
                return;
            }

            currentInputIndex++;
        }
        else
        {
            isActive = false;
            currentInputIndex = 0;

            feedbackText.text = "X";

            StartCoroutine(DisplaySequence());

            return;
        }

        UpdateFeedback();
    }

    public void PullLever()
    {
        RegisterInput(InputType.Lever);
    }

    public void PushButton()
    {
        RegisterInput(InputType.Button);
    }

    public void UseValve()
    {
        RegisterInput(InputType.Valve);
    }
}
