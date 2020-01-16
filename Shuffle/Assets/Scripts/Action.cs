using System;

using UnityEngine;

[CreateAssetMenu(fileName = "SequencerAction", menuName = "Shuffle/Sequencer Action", order = 1)]
public class Action : ScriptableObject
{
    public string displayName;
    public ActionType type;

    public Vector3 moveDirection;
}

public enum ActionType
{
    Move,
    Attack
}
