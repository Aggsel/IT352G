using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    float yDifference;
    bool falling = true;
    int counter = 0;

    List<floor> floors;

    private void Start()
    {
        floors = FindObjectOfType<CollisionListMaker>().floors;
    }

    void FixedUpdate()
    {
        if(counter < 2)
        {
            if (floors.Count < FindObjectOfType<CollisionListMaker>().floors.Count)
            {
                floors = FindObjectOfType<CollisionListMaker>().floors;
                Debug.Log("Updated the list");
            }
            counter++;
        }

        if (falling)
        {
            foreach (floor x in floors)
            {
                yDifference = transform.position.y - x.transform.position.y;

                if (yDifference < x.transform.localScale.y && falling)
                {
                    falling = false;
                }
                /*else
                {
                    if (!falling)
                    {
                        falling = true;
                    }
                }*/
            }
        }
    }

    public bool GetIfFalling()
    {
        return falling;
    }
}
