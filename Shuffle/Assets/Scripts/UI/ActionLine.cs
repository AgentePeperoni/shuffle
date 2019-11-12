using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionLine : MonoBehaviour
{
    public EventHandler<ActionLineEventArgs> OnStateChanged;

    public Actions LineAction
    {
        get => _lineAction;
        set
        {
            if (value == Actions.None)
                throw new NotSupportedException("ActionLine должна отвечать за какое-либо действие! Значение Actions.None недопустимо!");
            else
                _lineAction = value;
        }
    }

    [Header("UI Элементы")]
    [SerializeField]
    private TextMeshProUGUI _labelText;
    [SerializeField]
    private Image _backgroundImage;

    [Space]
    [Header("Основные Настройки")]
    [SerializeField]
    private Actions _lineAction;

    private Frame[] _frames;

    private void Awake()
    {
        if (_lineAction == Actions.None)
            throw new NotSupportedException("ActionLine должна отвечать за какое-либо действие! Значение Actions.None недопустимо!");

        _frames = GetComponentsInChildren<Frame>();

        for (int i = 0; i < _frames.Length; ++i)
        {
            _frames[i].FrameIndex = i;
            _frames[i].OnStateChanged += OnFrameValueChanged;
        }
    }

    private void OnFrameValueChanged(object sender, FrameEventArgs args)
    {
        OnStateChanged?.Invoke(this, new ActionLineEventArgs(sender as Frame, args));
    }
}

public class ActionLineEventArgs : EventArgs
{
    public readonly Frame changedFrame;
    public readonly FrameEventArgs frameEventArgs;

    public ActionLineEventArgs(Frame changedFrame, FrameEventArgs frameEventArgs)
    {
        this.changedFrame = changedFrame;
        this.frameEventArgs = frameEventArgs;
    }
}
