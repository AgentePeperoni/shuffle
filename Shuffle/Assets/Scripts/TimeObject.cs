using System;
using System.Collections.Generic;

using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public List<ObjectAction> Actions;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    
    public void SetCurrentFrame(int frame)
    {

    }

    private void Awake()
    {
        if (Actions == null)
            Actions = new List<ObjectAction>();

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    
}
