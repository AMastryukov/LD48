using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenMinigame : BaseMinigame
{
    [SerializeField] private Canvas computerCanvas;
    [SerializeField] private OxygenValve[] valves;
    [SerializeField] private Image[] displayImages;
    [SerializeField] private Sprite[] directionSprites;

    private OxygenValve.Orientation[] desiredOrientations;
    private List<int> randomOrder = new List<int> { 0, 1, 2 };
    private Color[] colors = { Color.red, Color.green, Color.yellow };

    private void Awake()
    {
        computerCanvas.enabled = false;
        
        // Color the valves
        for (int i = 0; i < valves.Length; i++)
        {
            foreach(MeshRenderer mesh in valves[i].GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material.color = colors[i];
            }
        }
    }

    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        ResetMinigame();
        base.StartMinigame();
    }

    public override void FinishMinigame()
    {
        computerCanvas.enabled = false;
        base.FinishMinigame();
    }

    public void CheckSolution()
    {
        if (!isActive) { return; }

        for (int i = 0; i < valves.Length; i++)
        {
            if (valves[i].currentOrientation != desiredOrientations[i])
            {
                ResetMinigame();
                return;
            }
        }

        FinishMinigame();
    }

    private void ResetMinigame()
    {
        computerCanvas.enabled = true;

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

            displayImages[i].sprite = directionSprites[(int)desiredOrientations[randomIndex]];
            displayImages[i].color = colors[randomIndex];
        }
    }
}
