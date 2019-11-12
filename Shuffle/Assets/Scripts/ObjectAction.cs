using System;

using UnityEngine;

public class ObjectAction
{
    public Actions Actions { get; protected set; }
    public Vector3 MovementDirection { get; protected set; }

    public void Add(Actions toAdd)
    {
        Actions |= toAdd;
        CalculateDirection();
    }

    public void Remove(Actions toRemove)
    {
        Actions ^= toRemove;
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        // TODO: Calculate Direction
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
