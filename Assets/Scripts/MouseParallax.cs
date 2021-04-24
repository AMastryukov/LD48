using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseParallax : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 5f;

    private Vector3 mouseScreenPos;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        mouseScreenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mouseScreenPos.z = 0;
        gameObject.transform.position = mouseScreenPos;

        transform.position = new Vector3(startPosition.x + (mouseScreenPos.x * parallaxFactor), startPosition.y + (mouseScreenPos.y * parallaxFactor), 0);
    }
}