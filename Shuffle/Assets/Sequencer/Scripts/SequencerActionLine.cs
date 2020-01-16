using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SequencerActionLine : MonoBehaviour
{
    public EventHandler<SequencerFrame> onFrameStateChanged;
    public EventHandler<SequencerActionLineEventArgs> onFramesDisabled;
    public EventHandler<SequencerActionLineEventArgs> onFramesEnabled;
    public EventHandler<SequencerActionLineEventArgs> onFramesMuted;
    public EventHandler<SequencerActionLineEventArgs> onFramesUnmuted;
    
    public List<SequencerFrame> Frames { get; protected set; }
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            _nameField.text = _name;
        }
    }
    protected string _name;

    protected Action _assignedAction;

    [SerializeField]
    protected Text _nameField;
    [SerializeField]
    protected Transform _framesContainer;

    [Space]
    [SerializeField]
    protected GameObject _framePrefab;

    public void Build(Action info, int frameCount)
    {
        if (Frames.Count > 0)
        {
            for (int i = Frames.Count - 1; i >= 0; --i)
                Destroy(Frames[i].gameObject);

            Frames.Clear();
        }

        Name = info.displayName;
        for (int i = 0; i < frameCount; ++i)
        {
            GameObject frameObj = Instantiate(_framePrefab, _framesContainer);
            SequencerFrame frame = frameObj.GetComponent<SequencerFrame>();

            frame.Index = i;
            frame.onStateChanged += OnFrameStateChanged;

            Frames.Add(frame);
        }
    }

    public void DisableFrames(params int[] indexes)
    {
        List<SequencerFrame> affectedFrames = new List<SequencerFrame>();
        for (int i = 0; i < indexes.Length; ++i)
        {
            SequencerFrame frame = Frames[indexes[i]];
            if (frame.gameObject.activeSelf)
            {
                frame.SetActive(false);
                affectedFrames.Add(frame);
            }
        }

        onFramesDisabled?.Invoke(this, new SequencerActionLineEventArgs(affectedFrames.ToArray()));
    }
    public void DisableFrames(int startIndex, int count)
    {
        List<SequencerFrame> affectedFrames = new List<SequencerFrame>();
        for (int i = startIndex; i < (startIndex + count) && i < Frames.Count; ++i)
        {
            SequencerFrame frame = Frames[i];
            if (frame.gameObject.activeSelf)
            {
                frame.SetActive(false);
                affectedFrames.Add(frame);
            }
        }

        onFramesDisabled?.Invoke(this, new SequencerActionLineEventArgs(affectedFrames.ToArray()));
    }

    public void EnableFrames(params int[] indexes)
    {
        List<SequencerFrame> affectedFrames = new List<SequencerFrame>();
        for (int i = 0; i < indexes.Length; ++i)
        {
            SequencerFrame frame = Frames[indexes[i]];
            if (frame.gameObject.activeSelf)
            {
                frame.SetActive(true);
                affectedFrames.Add(frame);
            }
        }

        onFramesEnabled?.Invoke(this, new SequencerActionLineEventArgs(affectedFrames.ToArray()));
    }
    public void EnableFrames(int startIndex, int count)
    {
        List<SequencerFrame> affectedFrames = new List<SequencerFrame>();
        for (int i = startIndex; i < (startIndex + count) && i < Frames.Count; ++i)
        {
            SequencerFrame frame = Frames[i];
            if (frame.gameObject.activeSelf)
            {
                frame.SetActive(true);
                affectedFrames.Add(frame);
            }
        }

        onFramesEnabled?.Invoke(this, new SequencerActionLineEventArgs(affectedFrames.ToArray()));
    }

    public void MuteFrames(params int[] indexes)
    {
        List<SequencerFrame> affectedFrames = new List<SequencerFrame>();
        for (int i = 0; i < indexes.Length; ++i)
        {
            SequencerFrame frame = Frames[indexes[i]];
            if (frame.gameObject.activeSelf && frame.IsActive)
            {
                frame.SetMute(true);
                affectedFrames.Add(frame);
            }
        }

        onFramesMuted?.Invoke(this, new SequencerActionLineEventArgs(affectedFrames.ToArray()));
    }
    public void MuteFrames(int startIndex, int count)
    {
        List<SequencerFrame> affectedFrames = new List<SequencerFrame>();
        for (int i = startIndex; i < (startIndex + count) && i < Frames.Count; ++i)
        {
            SequencerFrame frame = Frames[i];
            if (frame.gameObject.activeSelf && frame.IsActive)
            {
                frame.SetMute(true);
                affectedFrames.Add(frame);
            }
        }

        onFramesMuted?.Invoke(this, new SequencerActionLineEventArgs(affectedFrames.ToArray()));
    }

    public void UnmuteFrames()
    {
        List<SequencerFrame> affectedFrames = new List<SequencerFrame>();
        foreach (var frame in Frames)
        {
            if (frame.gameObject.activeSelf && frame.IsActive)
            {
                frame.SetMute(false);
                affectedFrames.Add(frame);
            }
        }

        onFramesUnmuted?.Invoke(this, new SequencerActionLineEventArgs(affectedFrames.ToArray()));
    }
    public void UnmuteFrames(params int[] indexes)
    {
        List<SequencerFrame> affectedFrames = new List<SequencerFrame>();
        for (int i = 0; i < indexes.Length; ++i)
        {
            SequencerFrame frame = Frames[indexes[i]];
            if (frame.gameObject.activeSelf && frame.IsActive)
            {
                frame.SetMute(false);
                affectedFrames.Add(frame);
            }
        }

        onFramesUnmuted?.Invoke(this, new SequencerActionLineEventArgs(affectedFrames.ToArray()));
    }

    protected void Awake()
    {
        Frames = new List<SequencerFrame>();
    }

    protected void OnFrameStateChanged(object sender, EventArgs args)
    {
        onFrameStateChanged?.Invoke(this, sender as SequencerFrame);
    }
}

public class SequencerActionLineEventArgs : EventArgs
{
    public readonly SequencerFrame[] affectedFrames;

    public SequencerActionLineEventArgs(params SequencerFrame[] affectedFrames)
    {
        this.affectedFrames = affectedFrames;
    }
}
