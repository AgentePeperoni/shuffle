using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class TimeManager : PreparationObject
{
    public int CurrentFrame { get; private set; }

    private List<TimeObject> _timeObjects;
    
    public void OnFrameChanged(object sender, TimelineEventArgs args)
    {
        SetCurrentFrame(args.frame);
    }

    private void Awake()
    {
        _timeObjects = FindObjectsOfType<TimeObject>().ToList();

        IsReady = true;
    }

    private void SetCurrentFrame(int frame)
    {
        foreach (var timeObj in _timeObjects)
            timeObj.SetCurrentFrame(frame);

        CurrentFrame = frame;
    }
}
