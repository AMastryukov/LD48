using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour
{

    public GameObject valve1;
    public GameObject valve2;
    public GameObject valve3;

    List<float> rotation = new List<float> { 90.0f, 180.0f, -90.0f, 0.0f };

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate()
    {
        transform.Rotate(0.0f, 0.0f, rotation[i]);
        if (i==3)
        {
            i = 0;
        }

        else
        {
            i=i+1;
        }
    }
}
