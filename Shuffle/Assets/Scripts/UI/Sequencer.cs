using System;

using UnityEngine;

public class Sequencer : MonoBehaviour
{
    public EventHandler<SequencerEventArgs> OnStateChanged;

    public Timeline Timeline { get; private set; }

    private ActionLine[] _actionLines;

    private void Awake()
    {
        _actionLines = GetComponentsInChildren<ActionLine>();
        foreach (var actionLine in _actionLines)
            actionLine.OnStateChanged += OnActionLineStateChanged;

        Timeline = GetComponentInChildren<Timeline>();
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
