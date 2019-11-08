using System.Linq;

using UnityEngine;

public class UIManager : MonoBehaviour
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
    
    public Color forwardColor;
    public Color backwardColor;
    public Color leftColor;
    public Color rightColor;
    public Color jumpColor;
    public Color attackColor;

    [SerializeField]
    private RadialMenu _radialMenuMove;

    private FrameButton[] _frameButtons;
    private PlayerController _playerController;

    public void ShowMoveRadialMenu(FrameButton invoker)
    {
        _radialMenuMove.gameObject.SetActive(true);
        _radialMenuMove.ConfigureMenu(invoker.FrameIndex);
        _radialMenuMove.transform.position = invoker.transform.position;
    }

    public void HideMoveRadialMenu()
    {
        _radialMenuMove.gameObject.SetActive(false);
    }

    public void FillFrame(int frameIndex, Move move, Rotate rotate, Act act)
    {
        _frameButtons.First((x) => x.FrameIndex == frameIndex).FillFrame(move, rotate, act);
    }

    public Color GetCorrectColor(ActionType type, Move move, Rotate rotate, Act act)
    {
        switch (type)
        {
            case ActionType.Move:
                switch (move)
                {
                    case Move.Forward:
                        return forwardColor;
                    case Move.Backward:
                        return backwardColor;
                    case Move.Left:
                        return leftColor;
                    case Move.Right:
                        return rightColor;
                    default:
                        return Color.grey;
                }
            case ActionType.Rotate:
                switch (rotate)
                {
                    case Rotate.Left:
                        return leftColor;
                    case Rotate.Right:
                        return rightColor;
                    default:
                        return Color.grey;
                }
            case ActionType.Act:
                switch (act)
                {
                    case Act.Jump:
                        return jumpColor;
                    case Act.Attack:
                        return attackColor;
                    default:
                        return Color.grey;
                }
            default:
                return Color.grey;
        }
    }

    private void Start()
    {
        _frameButtons = FindObjectsOfType<FrameButton>();
        foreach (var btn in _frameButtons)
        {
            btn.ManagerUI = this;
            btn.PlayerController = PlayerController;
        }

        _radialMenuMove.PlayerController = _playerController;

        _radialMenuMove.gameObject.SetActive(false);
    }
}
