using System;

using UnityEngine;

public class Player : TimeObject
{
    public EventHandler<PlayerEventArgs> OnStateChanged;

    public PlayerState State
    {
        get => _state;
        set
        {
            _state = value;
            OnStateChanged(this, new PlayerEventArgs(_state, _hasDied));
        }
    }

    public LayerMask DeathMask { get; set; }
    public LayerMask ObstacleMask { get; set; }

    [SerializeField]
    protected Transform _raycastParent;
    protected PlayerState _state;

    protected bool _hasDied;

    public override void SetCurrentFrame(int frame)
    {
        if (TimeObjectActions.Count < frame)
            while (TimeObjectActions.Count <= frame)
                TimeObjectActions.Add(new ObjectAction());

        ResetPlayer();
        for (int i = 0; i < frame; ++i)
            ResolveAction(TimeObjectActions[i]);
    }

    protected override void ResolveAction(ObjectAction action)
    {
        State = CheckCurrentState(action);
        
        switch (State)
        {
            case PlayerState.None:
                if (_hasDied)
                    return;

                base.ResolveAction(action);
                break;
            case PlayerState.Dead:
                if (!_hasDied)
                {
                    base.ResolveAction(action);
                    _hasDied = true;
                }
                break;
            default:
                break;
        }
    }

    protected void ResetPlayer()
    {
        ResetPosition();
        ResetState();

        _hasDied = false;
    }

    protected void ResetState() => State = PlayerState.None;

    protected PlayerState CheckCurrentState(ObjectAction action)
    {
        Vector3 direction = TranslateToLocal(action);
        bool obstacle = Physics.Raycast(_raycastParent.position, direction, _distanceMultiplier, ObstacleMask, QueryTriggerInteraction.Collide);
        bool death = Physics.CheckBox(_raycastParent.position + action.MovementDirection, (Vector3.one / 2.1f), Quaternion.identity, DeathMask);

        if (death)
            return PlayerState.Dead;
        else if (obstacle)
            return PlayerState.Blocked;
        else
            return PlayerState.None;
    }
}

public class PlayerEventArgs : EventArgs
{
    public readonly PlayerState state;
    public readonly bool hasDied;

    public PlayerEventArgs(PlayerState currentState, bool hasDied)
    {
        state = currentState;
        this.hasDied = hasDied;
    }
}

public enum PlayerState
{
    None,
    Blocked,
    Dead
}
