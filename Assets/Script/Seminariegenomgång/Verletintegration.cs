using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verletintegration : MonoBehaviour
{
    [SerializeField] private Vector3 startVelocity;
    [SerializeField] private float mass = 1;
    [SerializeField] private float damping = 0.01f;
    [SerializeField] private float floorDamping = 1.5f;

    public float Mass => mass;

    private Vector3 previousPos;
    private Vector3 currentPos;

    private Vector3 accumulatedForces = Vector3.zero;
    private float delta;
    private float inverseMass;
    private float currentDamping = 0f;

    void Start()
    {
        delta = Time.fixedDeltaTime;
        currentPos = transform.position;
        previousPos = currentPos - startVelocity * delta;
        inverseMass = 1 / mass;
        currentDamping = startVelocity.x * delta;
    }

    void FixedUpdate()
    {
        Vector3 tempPos = currentPos;
        if (GetComponent<collision>().GetIfFalling())
        {
            currentPos = (2 - damping) * currentPos - (1-damping)*previousPos + accumulatedForces * (delta * delta * inverseMass);
        } // else: glide along floor
        else
        {
            if(currentPos.x < previousPos.x)
            {
                currentPos = new Vector3(transform.position.x - Mathf.Abs(currentDamping), transform.position.y, transform.position.z);
            }
            else
            {
                currentPos = new Vector3(transform.position.x + currentDamping, transform.position.y, transform.position.z);
            }
            if(currentDamping > 0)
            {
                currentDamping = currentDamping / 2;
            }
            else
            {
                currentDamping = 0;
            }
            Debug.Log(currentDamping);
        }
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
