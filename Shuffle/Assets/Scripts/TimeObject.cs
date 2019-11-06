using System;
using System.Collections.Generic;

using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public List<FrameAction> FrameActions;
    public int CurrentFrame { get; protected set; }

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    public bool AddAction(int index, ActionType actions, Move move = Move.None, Rotate rotate = Rotate.None, Act act = Act.None)
    {
        if (FrameActions.Count > index)
        {
            FrameAction indexAction = FrameActions[index];
            if (actions.HasFlag(ActionType.Act))
                indexAction.SetAct(act);
            if (actions.HasFlag(ActionType.Move))
                indexAction.SetMove(move);
            if (actions.HasFlag(ActionType.Rotate))
                indexAction.SetRotate(rotate);

            return true;
        }
        else if (FrameActions.Count == index)
        {
            FrameActions.Add(new FrameAction(move, rotate, act));
            return true;
        }
        else
        {
            while (FrameActions.Count != index)
                FrameActions.Add(new FrameAction());

            FrameActions.Add(new FrameAction(move, rotate, act));
        }

        return false;
    }

    public bool RemoveAction(int index, ActionType actions)
    {
        if (FrameActions.Count > index)
        {
            FrameAction action = FrameActions[index];
            if (actions.HasFlag(ActionType.Act))
                action.SetAct(Act.None);
            if (actions.HasFlag(ActionType.Move))
                action.SetMove(Move.None);
            if (actions.HasFlag(ActionType.Rotate))
                action.SetRotate(Rotate.None);

            return true;
        }

        return false;
    }

    public void SetCurrentFrame(int frame)
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;

        for (int i = 0; i < frame; ++i)
            ResolveAction(FrameActions[i]);

        CurrentFrame = frame;
    }

    private void Awake()
    {
        if (FrameActions == null)
            FrameActions = new List<FrameAction>();

        for (int i = 0; i < 100; ++i)
            FrameActions.Add(new FrameAction());

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    private void ResolveAction(FrameAction action)
    {
        switch (action.Move)
        {
            case Move.None:
                break;
            case Move.Forward:
                transform.position += Vector3.right * 1.05f;
                break;
            case Move.Backward:
                transform.position -= Vector3.right * 1.05f;
                break;
            case Move.Left:
                transform.position += Vector3.forward * 1.05f;
                break;
            case Move.Right:
                transform.position -= Vector3.forward * 1.05f;
                break;
        }
        switch (action.Rotate)
        {
            case Rotate.None:
                break;
            case Rotate.Left:
                transform.Rotate(transform.up, -90f);
                break;
            case Rotate.Right:
                transform.Rotate(transform.up, 90f);
                break;
        }
        switch (action.Act)
        {
            case Act.None:
                break;
            case Act.Jump:
                transform.position += transform.forward;
                transform.position += transform.up;
                break;
            case Act.Attack:
                Debug.Log("Attack!");
                break;
        }
    }
}

[Flags]
public enum ActionType
{
    Move = 0,
    Rotate = 1,
    Act = 2
}
