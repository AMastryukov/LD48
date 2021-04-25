using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PhysicsInteractable : Interactable
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude > 1f)
        {
            // TODO: play a bang sound
        }
    }

    public override void Interact(MonoBehaviour interactor, RaycastHit hit)
    {
        Vector3 forceVector = transform.position - interactor.transform.position;
        rb.AddForceAtPosition(forceVector, hit.point, ForceMode.Impulse);

        base.Interact();
    }

    public void ApplyForce(Vector3 force, float magnitude)
    {
        rb.AddForce(force * magnitude, ForceMode.Impulse);
    }

    public void ApplyTorque()
    {
        float minTorque = -0.25f;
        float maxTorque = 0.25f;

        Vector3 randomVector = new Vector3(
            Random.Range(minTorque, maxTorque), 
            Random.Range(minTorque, maxTorque), 
            Random.Range(minTorque, maxTorque));

        rb.AddTorque(randomVector);
    }
}
