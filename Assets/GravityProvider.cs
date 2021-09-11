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

    void FixedUpdate()
    {
        foreach (Verletintegration gravityObject in objects)
        {
            if (gravityObject.GetComponent<collision>().GetIfFalling())
            {
                gravityObject.ApplyGravity(Gravity*gravityObject.Mass);
            }
        }
    }
}
