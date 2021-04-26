using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private Transform[] voidPieces;
    [SerializeField] private Transform voidBottom;

    private Transform vessel;
    private float yOffset = 35f;

    private void Awake()
    {
        vessel = FindObjectOfType<VesselMovement>().transform;
    }

    private void Update()
    {
        if (voidPieces[1].transform.position.y > vessel.position.y)
        {
            voidPieces[0].transform.position = voidPieces[2].transform.position + Vector3.down * yOffset;

            // Shift the void pieces
            Transform temp = voidPieces[0];
            voidPieces[0] = voidPieces[1];
            voidPieces[1] = voidPieces[2];
            voidPieces[2] = temp;
        }
    }
}
