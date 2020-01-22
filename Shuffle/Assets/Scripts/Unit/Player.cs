using System;

using UnityEngine;

public class Player : TimeObject
{
    public EventHandler<PlayerEventArgs> OnStateChanged;
    public EventHandler<PlayerEventArgs> OnCheckpointReached;

    public PlayerState State
    {
        get => _state;
        set
        {
            _state = value;
            OnStateChanged(this, new PlayerEventArgs(_state, _hasDied, null));
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
        if (TimeObjectActions.Count <= frame)
            while (TimeObjectActions.Count <= frame)
                TimeObjectActions.Add(new ObjectAction());

        ResetPlayer();
        for (int i = 0; i <= frame; ++i)
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
                State = CheckCurrentState(null);
                break;
            case PlayerState.Dead:
                if (!_hasDied)
                {
                    //base.ResolveAction(action);
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
        bool obstacle = false;
        if (action != null)
        {
            Vector3 direction = TranslateToLocal(action);
            obstacle = Physics.Raycast(_raycastParent.position, direction, _distanceMultiplier, ObstacleMask, QueryTriggerInteraction.Collide);
        }

        bool death = Physics.CheckBox(_raycastParent.position, (Vector3.one / 2.1f), Quaternion.identity, DeathMask, QueryTriggerInteraction.Collide);

        if (death)
            return PlayerState.Dead;
        else if (obstacle)
            return PlayerState.Blocked;
        else
            return PlayerState.None;
    }

    protected void OnTriggerEnter(Collider other)
    {
        Checkpoint checkpoint = other.GetComponent<Checkpoint>();
        if (checkpoint != null && !checkpoint.CheckpointReached)
            OnCheckpointReached?.Invoke(this, new PlayerEventArgs(State, _hasDied, checkpoint));
    }
}

public class PlayerEventArgs : EventArgs
{
    public readonly PlayerState state;
    public readonly bool hasDied;
    public readonly Checkpoint reachedCheckpoint;

    public PlayerEventArgs(PlayerState currentState, bool hasDied, Checkpoint reachedCheckpoint)
    {
        state = currentState;
        this.hasDied = hasDied;
        this.reachedCheckpoint = reachedCheckpoint;
    }
}

public enum PlayerState
{
    None,
    Blocked,
    Dead
}
