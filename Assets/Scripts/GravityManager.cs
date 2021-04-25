using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private PhysicsInteractable[] physicsInteractables;
    private Vector3 defaultGravity;

    private bool gravityEnabled = true;

    private void Awake()
    {
        physicsInteractables = FindObjectsOfType<PhysicsInteractable>();
        defaultGravity = Physics.gravity;
    }

    public void DisableGravity()
    {
        Physics.gravity = Vector3.zero;

        for(int i = 0; i < physicsInteractables.Length; i++)
        {
            Vector3 randomForce = new Vector3(Random.Range(-0.25f, 0.25f), 1f, Random.Range(-0.25f, 0.25f));

            physicsInteractables[i].ApplyForce(randomForce, 0.25f);
            physicsInteractables[i].ApplyTorque();
        }

        gravityEnabled = false;
    }

    public void EnableGravity(float duration = 1f)
    {
        Physics.gravity = defaultGravity;
        gravityEnabled = true;
    }
}
