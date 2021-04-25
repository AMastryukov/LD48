using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OxygenValve : MonoBehaviour
{

    public enum Orientation { up, right, down, left }
    public Orientation currentOrientation = Orientation.up;

    public static Action OnValveRotated;

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

        if ((int)currentOrientation == Enum.GetNames(typeof(Orientation)).Length - 1)
        {
            currentOrientation = 0;
        }
        else
        {
            currentOrientation++;
        }

        transform.Rotate(Vector3.up * -90f);

        
        OnValveRotated?.Invoke();


    }
}
