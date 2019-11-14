using System;
using System.Collections.Generic;

using UnityEngine;

public class TimeObject : PreparationObject
{
    public List<ObjectAction> TimeObjectActions { get; protected set; }

    [SerializeField]
    protected float _distanceMultiplier = 1;

    protected Vector3 _initialPosition;
    protected Quaternion _initialRotation;
    
    public virtual void SetCurrentFrame(int frame)
    {
        if (TimeObjectActions.Count < frame)
            while (TimeObjectActions.Count <= frame)
                TimeObjectActions.Add(new ObjectAction());

        ResetPosition();
        for (int i = 0; i < frame; ++i)
            ResolveAction(TimeObjectActions[i]);
    }

    public virtual void InsertAction(int index, Actions action)
    {
        if (TimeObjectActions.Count <= index)
        {
            while (TimeObjectActions.Count <= index)
                TimeObjectActions.Add(new ObjectAction());
        }

        TimeObjectActions[index].Add(action);
    }

    public virtual void RemoveAction(int index, Actions action)
    {
        if (TimeObjectActions.Count <= index)
            return;

        TimeObjectActions[index].Remove(action);
    }

    protected void Awake()
    {
        if (TimeObjectActions == null)
            TimeObjectActions = new List<ObjectAction>();

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        IsReady = true;
    }

    protected virtual void ResetPosition()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }

    protected virtual void ResolveAction(ObjectAction action)
    {
        transform.position += TranslateToLocal(action) * _distanceMultiplier;
    }

    protected Vector3 TranslateToLocal(ObjectAction action)
    {
        Vector3 localMovement =
            Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z
                ) * action.MovementDirection;

        return localMovement;
    }
}
