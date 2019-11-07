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

    public int FrameIndex => _frameIndex;

    private UIManager _managerUI;

    [SerializeField]
    private Button _assignedButton;

    [SerializeField]
    private int _frameIndex;

    [SerializeField]
    private ActionType _frameType;

    private void Start()
    {
        if (_assignedButton == null)
            _assignedButton = GetComponent<Button>();

        _assignedButton.onClick.AddListener(InvokeMenu);
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
