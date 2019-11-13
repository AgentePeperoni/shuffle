using System;

using UnityEngine;

public class PlayerManager : PreparationObject
{
    public Player Player { get; private set; }

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

        IsReady = true;
    }
}
