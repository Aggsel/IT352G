using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verletintegration : MonoBehaviour
{
    [SerializeField] private Vector3 startVelocity;
    [SerializeField] private float mass = 1;
    [SerializeField] private float damping = 0.01f;

    public float Mass => mass;

    private Vector3 previousPos;
    private Vector3 currentPos;

    private Vector3 accumulatedForces = Vector3.zero;
    private float delta;
    private float inverseMass;

    void Start()
    {
        delta = Time.fixedDeltaTime;
        currentPos = transform.position;
        previousPos = currentPos - startVelocity * delta;
        inverseMass = 1 / mass;
    }

    void FixedUpdate()
    {
        Vector3 tempPos = currentPos;
        if (GetComponent<collision>().GetIfFalling())
        {
            currentPos = (2 - damping) * currentPos - (1-damping)*previousPos + accumulatedForces * (delta * delta * inverseMass);
        } // else: glide/bounce along floor
        previousPos = tempPos;
        transform.position = currentPos;
        accumulatedForces = Vector3.zero;
    }

    public void ApplyForceButton()
    {
        ApplyForce(new Vector3(15000, 0, 35000));
    }
    
    public void ApplyForce(Vector3 force)
    {
        accumulatedForces += force;
    }

    public void ApplyGravity(Vector3 gravity)
    {
        ApplyForce(gravity);
    }
}
