using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Sequencer : MonoBehaviour
{
    public EventHandler<SequencerEventArgs> onActionChanged;

    public List<SequencerActionLine> ActionLines { get; protected set; }
    public SequencerSlider SequencerSlider { get; protected set; }
    public Action[] CurrentActions { get; protected set; }
    public int CurrentFrameCount { get; protected set; }

    [SerializeField]
    protected Action[] _initialActions;
    [SerializeField]
    protected int _initialFrameCount;
    [SerializeField]
    protected Transform _actionLinesContainer;

    [Space]
    [SerializeField]
    protected GameObject _actionLinePrefab;

    public void Build(Action[] newActions, int newFrameCount)
    {
        CurrentActions = newActions;
        CurrentFrameCount = newFrameCount;

        if (ActionLines.Count > 0)
        {
            for (int i = ActionLines.Count - 1; i >= 0; --i)
                Destroy(ActionLines[i].gameObject);

            ActionLines.Clear();
        }

        foreach (var action in CurrentActions)
        {
            GameObject actionLineObj = Instantiate(_actionLinePrefab, _actionLinesContainer);
            SequencerActionLine actionLine = actionLineObj.GetComponent<SequencerActionLine>();
            
            actionLine.Build(action, newFrameCount);
            actionLine.onFrameStateChanged += OnFrameStateChanged;

            ActionLines.Add(actionLine);
        }
        
        SequencerSlider.Setup(CurrentFrameCount);
    }
    
    protected void Awake()
    {
        ActionLines = new List<SequencerActionLine>();
    }

    protected void Start()
    {
        SequencerSlider = GetComponentInChildren<SequencerSlider>();

        Build(_initialActions, _initialFrameCount);
    }

    protected void OnFrameStateChanged(object sender, SequencerFrame changedFrame)
    {
        onActionChanged?.Invoke(this, new SequencerEventArgs(sender as SequencerActionLine, changedFrame));
    }
}

public class SequencerEventArgs : EventArgs
{
    public readonly SequencerActionLine changedLine;
    public readonly SequencerFrame changedFrame;

    public SequencerEventArgs(SequencerActionLine actionLine, SequencerFrame frame)
    {
        changedLine = actionLine;
        changedFrame = frame;
    }
}
