using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBallPosition : MonoBehaviour
{
    [SerializeField] private VerletIntegration ballVerlet;
    [SerializeField] private Toggle setNewVelocity;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 newCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            print(newCurrentPos);

            Vector3 diffVector = newCurrentPos - ballVerlet.CurrentPosition;
            Vector3 newPrevPos = ballVerlet.PreviousPosition + diffVector;

            ballVerlet.PreviousPosition = new Vector3(newPrevPos.x, newPrevPos.y, 0.0f);
            ballVerlet.CurrentPosition = new Vector3(newCurrentPos.x, newCurrentPos.y, 0.0f);

            if (!setNewVelocity.isOn) ballVerlet.SetPreviousPosition();
        }
    }
    
}
