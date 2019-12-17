using System;
using System.Collections;

using UnityEngine;

public class PlayerManager : PreparationObject
{
    public Player Player { get; private set; }
    public GameManager GameManager { get; set; }

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

    private Coroutine _checkpointCoroutine;

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
        Player.OnTriggerEntered += OnPlayerTriggerEntered;

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

    private void OnPlayerTriggerEntered(Collider trigger)
    {
        if (trigger.tag.Equals("Respawn") && GameManager.IsPlaying)
        {
            if (_checkpointCoroutine != null)
                StopCoroutine(_checkpointCoroutine);

            _checkpointCoroutine = StartCoroutine(CheckpointSet(trigger.transform.position));
        }
    }

    private IEnumerator CheckpointSet(Vector3 newPosition)
    {
        yield return new WaitUntil(() => !GameManager.IsPlaying);
        Player.CheckpointPassed(newPosition);
        GameManager.ResetSequencer();
    }
}
