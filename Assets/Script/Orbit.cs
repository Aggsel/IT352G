using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orbit : MonoBehaviour
{
    private static readonly float EarthMass = 5.97237f * Mathf.Pow(10f, 18f); // Kiloton
    private static readonly float MoonMass = 7.342f * Mathf.Pow(10f, 16f); // Kiloton
    private static readonly float G = 6.67408f * Mathf.Pow(10f, -11f);

    [SerializeField] private VerletIntegration verletIntegration;
    [SerializeField] private Transform Earth;
    [SerializeField] private Transform Moon;

    [SerializeField] private float distance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;

    [SerializeField] private bool gravity = true;
    [SerializeField] private bool isMoon = true;

    void Start()
    {
        verletIntegration.SetMass(isMoon ? MoonMass : EarthMass);

        minDistance = Vector3.Distance(Earth.position, Moon.position);
        maxDistance = minDistance;
    }

    void FixedUpdate()
    {
        if(!gravity)
            return;
            
        Vector3 diffEarthMoon = isMoon ? Earth.transform.position - Moon.transform.position : Moon.transform.position - Earth.transform.position;
        
        Vector3 earthDir = Vector3.Normalize(diffEarthMoon);
        float gravitationalForce = CalculateGravitationalForce();

        verletIntegration.AddForce(earthDir * gravitationalForce);

        MinMaxDistance();
    }

    private float CalculateGravitationalForce()
    {
        float dist = Vector3.Distance(Earth.position, Moon.position);
        return G * EarthMass * MoonMass / Mathf.Pow(dist, 2);
    }

    public void ToggleGravity(){
        gravity = !gravity;
    }

    private void MinMaxDistance()
    {
        distance = Vector3.Distance(Earth.position, Moon.position);

        maxDistance = distance > maxDistance ? distance : maxDistance;
        minDistance = distance < minDistance ? distance : minDistance;
    }
}
