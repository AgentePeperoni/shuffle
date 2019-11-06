using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private List<TimeObject> _timeObjects;

    public int CurrentFrame { get; protected set; }

    public void SetCurrentFrame(float frame)
    {
        foreach (var obj in _timeObjects)
            obj.SetCurrentFrame(Mathf.RoundToInt(frame));

        CurrentFrame = Mathf.RoundToInt(frame);
    }

    private void Start()
    {
        _timeObjects = FindObjectsOfType<TimeObject>().ToList();
    }
}
