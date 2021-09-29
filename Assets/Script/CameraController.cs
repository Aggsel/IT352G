using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] private Transform target;

    void Start(){
        if(target == null){
            Debug.LogWarning("No target attached to camera controller.", this);
            this.enabled = false;
            return;
        }
        
        offset = transform.position - target.position;
    }

    void FixedUpdate(){
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.2f);
    }
}
