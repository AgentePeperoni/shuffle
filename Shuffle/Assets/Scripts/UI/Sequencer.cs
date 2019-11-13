using System;

using UnityEngine;

public class Sequencer : PreparationObject
{
    public EventHandler<SequencerEventArgs> OnStateChanged;

    public Timeline Timeline { get; private set; }
    public ActionLine[] ActionLines { get; private set; }

    [SerializeField]
    private GameObject _graphicsBlock;

    public void ResetActionLines()
    {
        foreach (var line in ActionLines)
            line.ResetFrames();
    }

    public void LockSequencer(bool value) => _graphicsBlock.SetActive(value);

    private void Awake()
    {
        ActionLines = GetComponentsInChildren<ActionLine>();
        foreach (var actionLine in ActionLines)
            actionLine.OnStateChanged += OnActionLineStateChanged;

        Timeline = GetComponentInChildren<Timeline>();

        IsReady = true;
    }

    private void OnActionLineStateChanged(object sender, ActionLineEventArgs args)
    {
        OnStateChanged?.Invoke(this, new SequencerEventArgs(sender as ActionLine, args));
    }
}

public class SequencerEventArgs : EventArgs
{
    public readonly ActionLine changedActionLine;
    public readonly ActionLineEventArgs actionLineEventArgs;

    public SequencerEventArgs(ActionLine changedActionLine, ActionLineEventArgs actionLineEventArgs)
    {
        this.changedActionLine = changedActionLine;
        this.actionLineEventArgs = actionLineEventArgs;
    }
}
