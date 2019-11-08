using UnityEngine;
using UnityEngine.UI;

public class FrameButton : MonoBehaviour
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

    public PlayerController PlayerController
    {
        get { return _playerController; }
        set
        {
            if (_playerController == null)
                _playerController = value;
        }
    }

    public int FrameIndex => _frameIndex;
    public ActionFrameButton ActionFrameButton { get; private set; }

    private UIManager _managerUI;
    private PlayerController _playerController;

    [SerializeField]
    private Button _assignedButton;

    [SerializeField]
    private int _frameIndex;

    [SerializeField]
    private ActionType _frameType;

    public void FillFrame(Move move, Rotate rotate, Act act)
    {
        ActionFrameButton.gameObject.SetActive(true);
        ActionFrameButton.ConfigureButton(move, rotate, act);
    }

    private void Start()
    {
        if (_assignedButton == null)
            _assignedButton = GetComponent<Button>();

        _assignedButton.onClick.AddListener(InvokeMenu);

        ActionFrameButton = GetComponentInChildren<ActionFrameButton>();
        ActionFrameButton.ManagerUI = ManagerUI;
        ActionFrameButton.PlayerController = PlayerController;
        ActionFrameButton.type = _frameType;
    }

    private void InvokeMenu()
    {
        switch (_frameType)
        {
            case ActionType.Move:
                _managerUI.ShowMoveRadialMenu(this);
                break;
        }
    }
}
