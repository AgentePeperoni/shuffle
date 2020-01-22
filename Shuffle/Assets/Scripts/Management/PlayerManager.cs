using System;

using UnityEngine;

public class PlayerManager : PreparationObject
{
    public EventHandler<PlayerEventArgs> OnPlayerCheckpointReached;

    public Player Player { get; private set; }

    [Header("Основные Настройки")]
    [SerializeField]
    private LayerMask _obstacleMask;
    [SerializeField]
    private LayerMask _deathMask;

    [Space]
    [Header("Настройки Отображения")]
    [SerializeField]
    private GameObject _deathText;
    [SerializeField]
    private GameObject _blockedText;

    public void OnActionsChanged(object sender, SequencerEventArgs args)
    {
        if (args.changedFrame.IsOn)
            Player.InsertAction(args.changedFrame.Index, args.changedLine.AssignedAction);
        else
            Player.RemoveAction(args.changedFrame.Index, args.changedLine.AssignedAction);
    }

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        if (Player == null)
            throw new NullReferenceException("Скрипт игрока (Player) не найден!");

        Player.ObstacleMask = _obstacleMask;
        Player.DeathMask = _deathMask;

        Player.OnStateChanged += OnPlayerStateChanged;
        Player.OnCheckpointReached += OnCheckpointReached;

        IsReady = true;
    }

    private void OnPlayerStateChanged(object sender, PlayerEventArgs args)
    {
        if (args.hasDied)
            return;

        switch (args.state)
        {
            case PlayerState.None:
                _deathText.SetActive(false);
                _blockedText.SetActive(false);
                break;
            case PlayerState.Dead:
                _deathText.SetActive(true);
                _blockedText.SetActive(false);
                break;
            case PlayerState.Blocked:
                _deathText.SetActive(false);
                _blockedText.SetActive(true);
                break;
        }
    }

    private void OnCheckpointReached(object sender, PlayerEventArgs args) => OnPlayerCheckpointReached?.Invoke(sender, args);
}
