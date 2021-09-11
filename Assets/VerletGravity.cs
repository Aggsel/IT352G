using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerletGravity : MonoBehaviour
{
    private Vector3 _previousPosition;
    private Vector3 _currentPosition;

    private Vector3 _accumulatedForces;
    [SerializeField] private Vector2 startVelocity;
    [SerializeField] private float airFriction = 0.1f;
    [SerializeField] private float groundFriction = 0.2f;
    [SerializeField] private float mass;
    
    private float massInverse;
    private float velocity;

    private Vector3 buttonForce = new Vector3();

    void Start()
    {
        massInverse = 1 / mass;
        Vector3 startVelocity3 = new Vector3(startVelocity.x, startVelocity.y);
        _currentPosition = transform.position;
        _previousPosition = _currentPosition - startVelocity3 * Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        AddForce(new Vector3(0.0f, -9.81f, 0.0f) * mass); // Add gravity force

        float dampening = transform.position.y <= 0.0f ? groundFriction : airFriction;
        
        Vector3 tempPos = _currentPosition;
        _currentPosition = (2 - dampening) * _currentPosition - (1 - dampening) * _previousPosition +
                           _accumulatedForces * (Time.fixedDeltaTime * Time.fixedDeltaTime * massInverse);
        _previousPosition = tempPos;
        _currentPosition = _currentPosition.y <= 0.0f ? new Vector3(_currentPosition.x, 0.0f) : _currentPosition;
        transform.position = _currentPosition;
        _accumulatedForces = Vector3.zero;

        velocity = Vector3.Distance(_currentPosition, _previousPosition);
    }


    public void AddForce(Vector3 force)
    {
        _accumulatedForces += force;
    }

    public void AddForceInGame()
    {
        AddForce(buttonForce);
    }

    public void SetXForce(InputField inputField)
    {
        buttonForce = new Vector3(float.Parse(inputField.text), buttonForce.y, 0.0f);
    }
    
    public void SetYForce(InputField inputField)
    {
        buttonForce = new Vector3(buttonForce.x, float.Parse(inputField.text), 0.0f);
    }
}
