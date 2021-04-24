using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float cooldown = 0f;
    public InteractableEvent OnInteracted;

    private float currentCooldown = 0f;

    public void Interact()
    {
        if (currentCooldown > 0f)
        {
            return;
        }

        OnInteracted?.Invoke();

        currentCooldown = cooldown;
    }

    private void Update()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentCooldown < 0f)
        {
            currentCooldown = 0f;
        }
    }
}