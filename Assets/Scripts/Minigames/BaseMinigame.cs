using System;
using UnityEngine;

public abstract class BaseMinigame : MonoBehaviour
{
    public static Action OnMinigameStarted;
    public static Action OnMinigameFinished;

    [HideInInspector] public bool isActive = false;

    public virtual void StartMinigame()
    {
        isActive = true;
        OnMinigameStarted?.Invoke();
    }

    public virtual void FinishMinigame()
    {
        isActive = false;
        OnMinigameFinished?.Invoke();
    }
}
