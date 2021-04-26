using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselMovement : MonoBehaviour
{
    private float speed = 5f;

    private void FixedUpdate()
    {
        transform.position += Vector3.down * speed * Time.fixedDeltaTime;
    }

    public void SetSpeed(float newSpeed, float duration = 0f)
    {
        StartCoroutine(ChangeSpeed(newSpeed, duration));
    }

    private IEnumerator ChangeSpeed(float newSpeed, float duration = 0f)
    {
        float oldSpeed = speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            speed = Mathf.Lerp(oldSpeed, newSpeed, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        speed = newSpeed;
    }
}
