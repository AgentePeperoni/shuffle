using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TimeObject _testObj;

    private void Start()
    {
        _testObj.AddAction(0, ActionType.Move, Move.Forward);
        _testObj.AddAction(1, ActionType.Move, Move.Forward);
        _testObj.AddAction(2, ActionType.Move, Move.Forward);
        _testObj.AddAction(3, ActionType.Move, Move.Forward);
        _testObj.AddAction(4, ActionType.Move | ActionType.Rotate, Move.Right, Rotate.Right);
        _testObj.AddAction(5, ActionType.Move | ActionType.Rotate, Move.Forward, Rotate.Left);

#if UNITY_EDITOR
        for (int i = 0; i <= 5; ++i)
            print($"Action #{i} - {_testObj.FrameActions[i]}");
#endif
    }
}
