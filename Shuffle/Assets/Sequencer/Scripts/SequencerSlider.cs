using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SequencerSlider : MonoBehaviour
{
    [SerializeField]
    protected Slider _slider;

    public void Setup(int frameCount)
    {
        _slider.minValue = 0;
        _slider.maxValue = frameCount - 1;
        _slider.value = _slider.minValue;
    }

    protected void Awake()
    {
        _slider.wholeNumbers = true;
    }
}
