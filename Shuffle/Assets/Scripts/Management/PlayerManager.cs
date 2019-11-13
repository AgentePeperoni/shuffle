using System;

using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player _player;

    public void OnActionsChanged(object sender, SequencerEventArgs args)
    {
        if (args.actionLineEventArgs.frameEventArgs.value)
            _player.InsertAction(args.actionLineEventArgs.changedFrame.FrameIndex, args.changedActionLine.LineAction);
        else
            _player.RemoveAction(args.actionLineEventArgs.changedFrame.FrameIndex, args.changedActionLine.LineAction);
    }

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        if (_player == null)
            throw new NullReferenceException("Скрипт игрока (Player) не найден!");
    }
}
