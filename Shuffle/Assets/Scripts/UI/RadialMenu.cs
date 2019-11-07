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

    [SerializeField]
    private RadialButton[] _buttons;

    private PlayerController _playerController;
    private int _currentFrame;

    public void ConfigureMenu(int frameIndex)
    {
        _currentFrame = frameIndex;
    }

    public void SendActionToController(ActionType type, Move move, Rotate rotate, Act act)
    {
        switch (type)
        {
            case ActionType.Move:
                _playerController.MoveTo(_currentFrame, move);
                break;
            case ActionType.Rotate:
                _playerController.RotateTo(_currentFrame, rotate);
                break;
            case ActionType.Act:
                _playerController.ActAs(_currentFrame, act);
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
