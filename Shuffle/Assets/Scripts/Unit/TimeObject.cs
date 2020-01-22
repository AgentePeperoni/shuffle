using System;
using System.Collections.Generic;

using UnityEngine;

public class TimeObject : PreparationObject
{
    public List<ObjectAction> TimeObjectActions { get; protected set; }

    [SerializeField]
    protected float _distanceMultiplier = 1;

    public Vector3 InitialPosition { get; set; }
    public Quaternion InitialRotation { get; set; }
    
    public virtual void SetCurrentFrame(int frame)
    {
        if (TimeObjectActions.Count <= frame)
            while (TimeObjectActions.Count <= frame)
                TimeObjectActions.Add(new ObjectAction());

        ResetPosition();
        for (int i = 0; i < frame; ++i)
            ResolveAction(TimeObjectActions[i]);
    }

    public virtual void InsertAction(int index, Action action)
    {
        if (TimeObjectActions.Count <= index)
        {
            while (TimeObjectActions.Count <= index)
                TimeObjectActions.Add(new ObjectAction());
        }

        TimeObjectActions[index].Add(action.actionType);
    }

    public virtual void RemoveAction(int index, Action action)
    {
        if (TimeObjectActions.Count <= index)
            return;

        TimeObjectActions[index].Remove(action.actionType);
    }

    protected void Awake()
    {
        if (TimeObjectActions == null)
            TimeObjectActions = new List<ObjectAction>();

        InitialPosition = transform.position;
        InitialRotation = transform.rotation;

        IsReady = true;
    }

    protected virtual void ResetPosition()
    {
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;
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
