using System;

using UnityEngine;
using UnityEngine.UI;

public class Timeline : PreparationObject
{
    public EventHandler<TimelineEventArgs> OnFrameChanged;

    private Slider _timelineSlider;

    public void SetSliderValue(float value) => _timelineSlider.value = value;

    private void Awake()
    {
        _timelineSlider = GetComponent<Slider>();
        _timelineSlider.onValueChanged.AddListener(OnValueChanged);

        IsReady = true;
    }

    private void OnValueChanged(float newValue)
    {
        int frame = Mathf.RoundToInt(newValue);
        OnFrameChanged?.Invoke(this, new TimelineEventArgs(frame));
    }
}

public class TimelineEventArgs : EventArgs
{
    public readonly int frame;

    public TimelineEventArgs(int frame)
    {
        this.frame = frame;
    }
}