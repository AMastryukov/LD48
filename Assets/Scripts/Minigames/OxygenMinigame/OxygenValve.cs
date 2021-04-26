using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OxygenValve : MonoBehaviour
{
    public enum Orientation { Up, Right, Down, Left }
    public Orientation currentOrientation = Orientation.Up;
    [SerializeField] private List<AudioClip> clips;
    private AudioSource src;

    private bool isRotating = false;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void Rotate()
    {
        if (isRotating) { return; }
        print(clips.Count);
        src.clip = clips[UnityEngine.Random.Range(0, clips.Count)];
        src.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        src.Play();
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
    }
}
