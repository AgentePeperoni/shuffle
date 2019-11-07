using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UIManager ManagerUI
    {
        get { return _managerUI; }
        set
        {
            if (_managerUI == null)
                _managerUI = value;
        }
    }
    private UIManager _managerUI;

    [SerializeField]
    private TimeObject _target;

    public void MoveTo(int index, Move direction)
    {
        _target.AddAction(index, ActionType.Move, direction);
        _managerUI.HideMoveRadialMenu();
    }

    public void RotateTo(int index, Rotate rotation)
    {
        _target.AddAction(index, ActionType.Rotate, rotate: rotation);
        _managerUI.HideMoveRadialMenu();
    }

    public void ActAs(int index, Act act)
    {
        _target.AddAction(index, ActionType.Act, act: act);
        _managerUI.HideMoveRadialMenu();
    }

    private void Start()
    {
        if (_target == null)
            _target = GetComponent<TimeObject>() ?? GetComponentInParent<TimeObject>();
    }
}
