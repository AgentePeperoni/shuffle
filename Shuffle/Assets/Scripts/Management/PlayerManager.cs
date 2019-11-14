using System;

using UnityEngine;

public class PlayerManager : PreparationObject
{
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
        if (args.actionLineEventArgs.frameEventArgs.value)
            Player.InsertAction(args.actionLineEventArgs.changedFrame.FrameIndex, args.changedActionLine.LineAction);
        else
            Player.RemoveAction(args.actionLineEventArgs.changedFrame.FrameIndex, args.changedActionLine.LineAction);
    }

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        if (Player == null)
            throw new NullReferenceException("Скрипт игрока (Player) не найден!");

        Player.ObstacleMask = _obstacleMask;
        Player.DeathMask = _deathMask;

        Player.OnStateChanged += OnPlayerStateChanged;

        IsReady = true;
    }

    private void OnPlayerStateChanged(object sender, PlayerEventArgs args)
    {
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
}
