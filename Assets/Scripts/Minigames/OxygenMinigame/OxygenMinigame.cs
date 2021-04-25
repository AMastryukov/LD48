using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenMinigame : BaseMinigame
{
    [SerializeField] private OxygenValve[] valves;
    [SerializeField] private TextMeshProUGUI[] displayTexts;
    [SerializeField] private Image[] displayImages;
    [SerializeField] private Sprite[] directionSprites;

    private OxygenValve.Orientation[] desiredOrientations;
    private List<int> randomOrder = new List<int> { 0, 1, 2 };

    public override void StartMinigame()
    {
        SetupMinigame();
        base.StartMinigame();
    }

    private void OnEnable()
    {
        OxygenValve.OnValveRotated += CheckRotations;
    }

    private void OnDisable()
    {
        OxygenValve.OnValveRotated -= CheckRotations;
    }

    private void CheckRotations()
    {
        for (int i = 0; i < valves.Length; i++)
        {
            if (valves[i].currentOrientation != desiredOrientations[i])
            {
                return;
            }
        }

        Debug.Log("Game Win");

        FinishMinigame();
    }

    private void SetupMinigame()
    {
        desiredOrientations = new OxygenValve.Orientation[valves.Length];

        // Generate random orientations for each valve
        for (int i = 0; i < desiredOrientations.Length; i++)
        {
            while (desiredOrientations[i] == valves[i].currentOrientation)
            {
                desiredOrientations[i] = (OxygenValve.Orientation)UnityEngine.Random.Range(0, Enum.GetNames(typeof(OxygenValve.Orientation)).Length);
            }
        }

        // Randomize the valve placements to add abstraction
        randomOrder = randomOrder.OrderBy(i => UnityEngine.Random.value).ToList();

        for (int i = 0; i < randomOrder.Count; i++)
        {
            int randomIndex = randomOrder[i];

            displayImages[randomIndex].sprite = directionSprites[(int)desiredOrientations[i]];
            displayTexts[i].text = (randomOrder[randomIndex] + 1).ToString();
        }
    }

    void Start()
    {
        StartMinigame();
    }
}
