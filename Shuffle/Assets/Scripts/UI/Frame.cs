using System;

using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
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

    private void Awake()
    {
        _frameToggle = GetComponent<Toggle>();
        _frameToggle.onValueChanged.AddListener(ToggleValueChanged);
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
