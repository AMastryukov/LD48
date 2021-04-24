using System;
using UnityEngine;

public abstract class BaseMinigame : MonoBehaviour
{
    public static Action<BaseMinigame> OnMinigameStarted;
    public static Action<BaseMinigame> OnMinigameFinished;

    [HideInInspector] public bool isActive = false;

    public virtual void StartMinigame()
    {
        isActive = true;
        OnMinigameStarted?.Invoke(this);
    }

    public virtual void FinishMinigame()
    {
        isActive = false;
        OnMinigameFinished?.Invoke(this);
    }
}
