using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PhysicsInteractable : Interactable
{
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rigidbody.velocity.magnitude > 1f)
        {
            // TODO: play a bang sound
        }
    }

    public override void Interact(MonoBehaviour interactor = null)
    {
        Vector3 forceVector = transform.position - interactor.transform.position;
        ApplyForce(forceVector, 2f); 

        base.Interact(interactor);
    }

    public void ApplyForce(Vector3 force, float magnitude)
    {
        rigidbody.AddForce(force * magnitude, ForceMode.Impulse);
    }

    public void ApplyTorque()
    {
        float minTorque = -0.25f;
        float maxTorque = 0.25f;

        Vector3 randomVector = new Vector3(
            Random.Range(minTorque, maxTorque), 
            Random.Range(minTorque, maxTorque), 
            Random.Range(minTorque, maxTorque));

        rigidbody.AddTorque(randomVector);
    }
}
