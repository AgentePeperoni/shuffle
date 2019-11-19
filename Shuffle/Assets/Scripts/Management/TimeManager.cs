using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class TimeManager : PreparationObject
{
    public int CurrentFrame { get; private set; }
    public int FrameOffset { get; set; }
    
    [Header("Основные Настройки")]
    [SerializeField]
    private int _maxFrames = 400;

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
        int realFrame = frame + FrameOffset;

        foreach (var timeObj in _timeObjects)
            timeObj.SetCurrentFrame(realFrame);

        CurrentFrame = realFrame;
    }
}
