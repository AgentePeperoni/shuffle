using System;

using UnityEngine;
using UnityEngine.UI;

public class SequencerSlider : MonoBehaviour
{
    public EventHandler<int> onFrameChanged;

    public int CurrentFrame => (int)_slider.value;

    [SerializeField]
    protected Slider _slider;

    public void Setup(int frameCount)
    {
        _slider.minValue = 0;
        _slider.maxValue = frameCount - 1;
        _slider.value = _slider.minValue;
    }

    public void SetSliderValue(int value)
    {
        _slider.value = Mathf.Clamp(value, _slider.minValue, _slider.maxValue);
    }

    protected void Awake()
    {
        _slider.wholeNumbers = true;
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    protected void OnSliderValueChanged(float value)
    {
        int frame = Mathf.RoundToInt(value);
        onFrameChanged?.Invoke(this, frame);
    }
}
