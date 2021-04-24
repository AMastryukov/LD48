using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimonMinigame : BaseMinigame
{
    public enum InputType { Lever, Button, Valve }

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
        int sequenceLength = 5;
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add((InputType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(InputType)).Length));
        }

        foreach(InputType type in sequence)
        {
            Debug.Log(type.ToString());
        }

        UpdateFeedback();
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
            Debug.Log("GOOD JOB!");

            if (currentInputIndex == sequence.Count - 1)
            {
                FinishMinigame();
                return;
            }

            currentInputIndex++;
        }
        else
        {
            Debug.Log("You suck lol");

            currentInputIndex = 0;
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
