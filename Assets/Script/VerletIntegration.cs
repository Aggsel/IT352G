using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerletIntegration : MonoBehaviour
{
    private Vector3 _previousPosition;
    private Vector3 _currentPosition;

    private Vector3 _accumulatedForces;
    [SerializeField] private Vector2 startVelocity;
    [SerializeField] private float airFriction = 0.1f;
    [SerializeField] private float groundFriction = 0.2f;
    [SerializeField] private float mass;
    
    private float massInverse;

    private Vector3 buttonForce;
    private Vector3 posUpdateVelocity;


    public Vector3 PreviousPosition
    {
        get => _previousPosition;
        set => _previousPosition = value;
    }

    public Vector3 CurrentPosition
    {
        get => _currentPosition;
        set => _currentPosition = value;
    }

    void Start()
    {
        massInverse = 1 / mass;
        CurrentPosition = transform.position;
        SetPreviousPosition(new Vector3(startVelocity.x, startVelocity.y));
    }

    void FixedUpdate()
    {
        AddForce(new Vector3(0.0f, -9.81f, 0.0f) * mass); // Add gravity force

        float dampening = transform.position.y <= 0.0f ? groundFriction : airFriction;
        
        Vector3 tempPos = CurrentPosition;
        CurrentPosition = (2 - dampening) * CurrentPosition - (1 - dampening) * PreviousPosition +
                           _accumulatedForces * (Time.fixedDeltaTime * Time.fixedDeltaTime * massInverse);
        PreviousPosition = tempPos;
        CurrentPosition = CurrentPosition.y <= 0.0f ? new Vector3(CurrentPosition.x, 0.0f) : CurrentPosition;
        transform.position = CurrentPosition;
        _accumulatedForces = Vector3.zero;

    }

    public void AddForce(Vector3 force)
    {
        _accumulatedForces += force;
    }

    public void AddForceInGame()
    {
        AddForce(buttonForce);
    }

    public void SetPreviousPosition()
    {
        PreviousPosition = CurrentPosition - posUpdateVelocity * Time.fixedDeltaTime;
    }
    
    public void SetPreviousPosition(Vector3 velocity)
    {
        PreviousPosition = CurrentPosition - velocity * Time.fixedDeltaTime;
    }

    public void SetXForce(InputField inputField)
    {
        buttonForce.x = float.Parse(inputField.text);
    }
    
    public void SetYForce(InputField inputField)
    {
        buttonForce.y = float.Parse(inputField.text);
    }

    public void SetXVelocity(InputField inputField)
    {
        posUpdateVelocity.x = float.Parse(inputField.text);
    }
    
    public void SetYVelocity(InputField inputField)
    {
        posUpdateVelocity.y = float.Parse(inputField.text);
    }
}
