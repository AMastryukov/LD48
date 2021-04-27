using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    private TextMeshProUGUI[] creditTexts;

    private void Awake()
    {
        creditTexts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void ShowCredits(float totalTime = 10f)
    {
        for (int i = 0; i < creditTexts.Length; i++)
        {
            creditTexts[i].enabled = false;
        }

        StartCoroutine(LoopThroughCredits(totalTime));
    }

    private IEnumerator LoopThroughCredits(float totalTime)
    {
        float duration = totalTime / creditTexts.Length;

        for (int i = 0; i < creditTexts.Length; i++)
        {
            for (int j = 0; j < creditTexts.Length; j++)
            {
                creditTexts[j].enabled = false;
            }

            creditTexts[i].enabled = true;

            yield return new WaitForSeconds(duration);
        }
    }
}
