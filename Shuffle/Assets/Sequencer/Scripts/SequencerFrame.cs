using System;

using UnityEngine;
using UnityEngine.EventSystems;

public class SequencerFrame : MonoBehaviour
{
    public EventHandler onStateChanged;

    public int Index { get; set; }
    public bool IsActive { get; protected set; }
    public bool IsMuted { get; protected set; }
    public bool IsOn { get; protected set; }

    public SequencerGraphics Graphics { get; protected set; }

    public virtual void SetActive(bool value)
    {
        Graphics.gameObject.SetActive(value);
        IsActive = value;
    }

    public virtual void SetMute(bool value)
    {
        if (value && IsOn)
            SetState(false);

        IsMuted = value;
    }

    public virtual void SetState(bool isOn)
    {
        if (IsOn == isOn || IsMuted)
            return;

        IsOn = isOn;
        Graphics.SetState(IsOn);

        onStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetGraphicsColor(Color color)
    {
        if (Graphics.gameObject.activeSelf)
            Graphics.SetColor(color);
    }

    protected void Awake()
    {
        Graphics = GetComponentInChildren<SequencerGraphics>() ?? GetComponent<SequencerGraphics>();
        Graphics.onPointerDown += OnGraphicsPointerDown;

        IsActive = Graphics.gameObject.activeSelf;
        if (IsActive)
            Graphics.SetState(IsOn);
    }

    protected virtual void OnGraphicsPointerDown(object sender, PointerEventData eventData)
    {
        SetState(!IsOn);
    }
}
