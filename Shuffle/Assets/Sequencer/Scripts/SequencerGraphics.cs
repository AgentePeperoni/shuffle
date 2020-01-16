using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SequencerGraphics : MonoBehaviour, IPointerDownHandler
{
    public EventHandler<PointerEventData> onPointerDown;

    [SerializeField]
    protected Graphic[] _colorDynamicGraphics;
    [SerializeField]
    protected Graphic[] _activatedGraphics;

    public Color CurrentColor { get; protected set; }
    public bool IsOn { get; protected set; }

    public void SetColor(Color color)
    {
        foreach (Graphic g in _colorDynamicGraphics)
            g.color = color;

        CurrentColor = color;
    }

    public void SetState(bool isOn)
    {
        foreach (Graphic g in _activatedGraphics)
            g.enabled = isOn;

        IsOn = isOn;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke(this, eventData);
    }
}
