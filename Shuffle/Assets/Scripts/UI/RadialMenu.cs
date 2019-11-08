using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public PlayerController PlayerController
    {
        get { return _playerController; }
        set
        {
            if (_playerController == null)
                _playerController = value;
        }
    }

    public int CurrentFrame { get; private set; }

    [SerializeField]
    private RadialButton[] _buttons;

    private PlayerController _playerController;

    public void ConfigureMenu(int frameIndex)
    {
        CurrentFrame = frameIndex;
    }

    public void SendActionToController(ActionType type, Move move, Rotate rotate, Act act)
    {
        switch (type)
        {
            case ActionType.Move:
                _playerController.MoveTo(CurrentFrame, move);
                break;
            case ActionType.Rotate:
                _playerController.RotateTo(CurrentFrame, rotate);
                break;
            case ActionType.Act:
                _playerController.ActAs(CurrentFrame, act);
                break;
        }
    }

    private void Start()
    {
        if (_buttons == null || _buttons.Length <= 0)
            _buttons = GetComponentsInChildren<RadialButton>();

        foreach (var btn in _buttons)
            btn.Parent = this;
    }
}
