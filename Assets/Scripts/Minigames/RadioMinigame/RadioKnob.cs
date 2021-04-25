using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioKnob : Interactable
{
    private bool is_rotating = false;
    [SerializeField] private RadioMinigame minigame;
    [SerializeField] private ToggleOption option;

    public override void Interact()
    {
        if (is_rotating)
        {
            return;
        }
        base.Interact();
        StartCoroutine(RotateKnob());
        minigame.toggle(option);
    }

    private IEnumerator RotateKnob()
    {
        is_rotating = true;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation * Quaternion.Euler(Vector3.up * -90f);

        float duration = 0.5f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = to;
        is_rotating = false;
    }
}
