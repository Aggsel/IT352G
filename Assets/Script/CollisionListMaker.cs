using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionListMaker : MonoBehaviour
{
    public List<floor> floors;

    void Start()
    {
        floors = FindObjectsOfType<floor>().ToList();
    }
}
