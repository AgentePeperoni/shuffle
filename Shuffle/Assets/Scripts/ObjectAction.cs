using System;

using UnityEngine;

public class ObjectAction
{
    public Actions ObjectActions { get; protected set; }
    public Vector3 MovementDirection { get; protected set; }

    public ObjectAction()
    {
        ObjectActions = Actions.None;
        MovementDirection = Vector3.zero;
    }

    public void Add(Actions toAdd)
    {
        ObjectActions |= toAdd;
        CalculateDirection();
    }

    public void Remove(Actions toRemove)
    {
        ObjectActions ^= toRemove;
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        MovementDirection = Vector3.zero;

        if (ObjectActions.HasFlag(Actions.MoveForward))
            MovementDirection += Vector3.forward;
        if (ObjectActions.HasFlag(Actions.MoveRight))
            MovementDirection += Vector3.right;
        if (ObjectActions.HasFlag(Actions.MoveBackward))
            MovementDirection += Vector3.back;
        if (ObjectActions.HasFlag(Actions.MoveLeft))
            MovementDirection += Vector3.left;
    }
}

[Flags]
public enum Actions
{
    None = 0,
    MoveForward = 1,
    MoveRight = 2,
    MoveBackward = 4,
    MoveLeft = 8
}
