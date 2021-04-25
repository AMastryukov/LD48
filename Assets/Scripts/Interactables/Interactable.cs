using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float cooldown = 1f;
    public InteractableEvent OnInteracted;

    private float currentCooldown = 0f;

    public virtual void Interact()
    {
        if (currentCooldown > 0f)
        {
            return;
        }

        OnInteracted?.Invoke();

        currentCooldown = cooldown;
    }

    public virtual void Interact(MonoBehaviour interactor)
    {
        Interact();
    }

    public virtual void Interact(MonoBehaviour interactor, RaycastHit hit)
    {
        Interact();
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