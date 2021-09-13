using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private Verletintegration attachedObject;
    [SerializeField] private Vector3 attachmentOffset;
    [SerializeField] private Vector3 anchorPoint;
    [SerializeField] private Vector3 restingPoint;
    [SerializeField] private float springConstant;

    private Verletintegration shouldApplyForce = null;

    private void Start()
    {
        shouldApplyForce = GetComponent<Verletintegration>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPosition = (attachedObject.transform.position - attachmentOffset);
        Vector3 x = currentPosition - ((transform.position - anchorPoint) - restingPoint);
        
        attachedObject.ApplyForce(-springConstant*x);
        shouldApplyForce?.ApplyForce(springConstant*x);
    }

    private void OnDrawGizmos()
    {
        Vector3 currentPosition = (attachedObject.transform.position - attachmentOffset);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere((transform.position - anchorPoint), 0.1f);
        Gizmos.DrawLine((transform.position -anchorPoint), currentPosition);
    }
}
