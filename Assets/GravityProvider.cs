using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityProvider : MonoBehaviour
{
    [SerializeField] Vector3 Gravity = new Vector3(0, -9.81f, 0);

    private List<Verletintegration> objects;
    void Start()
    {
        objects = GameObject.FindObjectsOfType<Verletintegration>().ToList();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Verletintegration gravityObject in objects)
        {
            gravityObject.ApplyGravity(Gravity*gravityObject.Mass);
        }
    }
}
