using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlinkingLight : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] Light point_light;
    private bool on;
    [SerializeField] float min_delay_seconds;
    [SerializeField] float max_delay_seconds;
    public Color c;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
        {
            rend.material.color = c;
            StartCoroutine(blink());
        }
            
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }
        point_light.color = c;
    }

    private IEnumerator blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(min_delay_seconds, max_delay_seconds));
            if (on)
            {
                rend.material.color = Color.grey;
                rend.material.SetColor("_EmissionColor", Color.black);
                point_light.enabled = false;
            }
            else
            {
                rend.material.color = c;
                rend.material.SetColor("_EmissionColor", c);
                point_light.enabled = true;
            }

            on = !on;
        }
        
    }
}
