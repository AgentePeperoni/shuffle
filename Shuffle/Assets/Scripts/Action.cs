using System;

using UnityEngine;

[CreateAssetMenu(fileName = "SequencerAction", menuName = "Shuffle/Sequencer Action", order = 1)]
public class Action : ScriptableObject
{
    public string displayName;
    public Actions actionType;
    public AudioClip sfx;
}
