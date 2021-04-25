using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OxygenValve : MonoBehaviour
{
    public enum Orientation { Up, Right, Down, Left }
    public Orientation currentOrientation = Orientation.Up;

    public static Action OnValveRotated;

    private bool isRotating = false;

    public void Rotate()
    {
        if (isRotating) { return; }

        StartCoroutine(RotateValve());
    }

    private IEnumerator RotateValve()
    {
        isRotating = true;

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

        if ((int)currentOrientation == Enum.GetNames(typeof(Orientation)).Length - 1)
        {
            currentOrientation = 0;
        }
        else
        {
            currentOrientation++;
        }

        transform.rotation = to;
        isRotating = false;

        OnValveRotated?.Invoke();
    }
}
