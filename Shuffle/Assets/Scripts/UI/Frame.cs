using System;

using UnityEngine;
using UnityEngine.UI;

public class Frame : PreparationObject
{
    public EventHandler<FrameEventArgs> OnStateChanged;
    
    public int FrameIndex
    {
        get => _frameIndex;
        set
        {
            if (_frameIndex < 0)
                throw new IndexOutOfRangeException("FrameIndex не может быть отрицательным!");
            else
                _frameIndex = value;
        }
    }
    
    private Toggle _frameToggle;
    private int _frameIndex;

    public void SetContentColor(Color color) => _frameToggle.graphic.color = color;

    public void Reset() => _frameToggle.isOn = false;

    private void Awake()
    {
        _frameToggle = GetComponent<Toggle>();
        _frameToggle.isOn = false;

        _frameToggle.onValueChanged.AddListener(ToggleValueChanged);

        IsReady = true;
    }

    private void ToggleValueChanged(bool newValue)
    {
        OnStateChanged?.Invoke(this, new FrameEventArgs(newValue));
    }
}

public class FrameEventArgs : EventArgs
{
    public readonly bool value;

    public FrameEventArgs(bool newValue)
    {
        value = newValue;
    }
}
