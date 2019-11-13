using System;
using System.Collections.Generic;

using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public List<ObjectAction> TimeObjectActions { get; protected set; }

    [SerializeField]
    private float _distanceMultiplier = 1;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    
    public void SetCurrentFrame(int frame)
    {
        if (TimeObjectActions.Count < frame)
            while (TimeObjectActions.Count <= frame)
                TimeObjectActions.Add(new ObjectAction());

        ResetPosition();
        for (int i = 0; i < frame; ++i)
            ResolveAction(TimeObjectActions[i]);
    }

    public void InsertAction(int index, Actions action)
    {
        if (TimeObjectActions.Count <= index)
        {
            while (TimeObjectActions.Count <= index)
                TimeObjectActions.Add(new ObjectAction());
        }

        TimeObjectActions[index].Add(action);
    }

    public void RemoveAction(int index, Actions action)
    {
        if (TimeObjectActions.Count <= index)
            return;

        TimeObjectActions[index].Remove(action);
    }

    private void Awake()
    {
        if (TimeObjectActions == null)
            TimeObjectActions = new List<ObjectAction>();

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    private void ResetPosition()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }

    private void ResolveAction(ObjectAction action)
    {
        Vector3 localMovement = 
            Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z
                ) * action.MovementDirection;

        transform.position += localMovement * _distanceMultiplier;
    }
}
