using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    [SerializeField] GameObject floor = null; //change to something more general

    float yDifference;
    bool falling = true;

    void FixedUpdate()
    {
        yDifference = transform.position.y - floor.transform.position.y;

        if (yDifference < floor.transform.localScale.y && falling)
        {
            falling = false;
        }
    }

    public bool GetIfFalling()
    {
        return falling;
    }
}
