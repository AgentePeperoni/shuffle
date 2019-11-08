using UnityEngine;
using UnityEngine.UI;

public class ActionFrameButton : MonoBehaviour
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

    public UIManager ManagerUI
    {
        get { return _managerUI; }
        set
        {
            if (_managerUI == null)
                _managerUI = value;
        }
    }

    public int frameIndex;
    public ActionType type;
    private Move _move;
    private Rotate _rotate;
    private Act _act;

    [SerializeField]
    private Button _assignedButton;

    private PlayerController _playerController;
    private UIManager _managerUI;

    public void ConfigureButton(Move move, Rotate rotate, Act act)
    {
        _move = move;
        _rotate = rotate;
        _act = act;

        _assignedButton.targetGraphic.color = _managerUI.GetCorrectColor(type, move, rotate, act);
    }

    private void Start()
    {
        if (_assignedButton == null)
            _assignedButton = GetComponent<Button>();

        _assignedButton.onClick.AddListener(RemoveAction);

        gameObject.SetActive(false);
    }

    private void RemoveAction()
    {
        _playerController.RemoveAction(frameIndex, type);
        gameObject.SetActive(false);
    }
}
