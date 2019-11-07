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

    private void Start()
    {
        _frameButtons = FindObjectsOfType<FrameButton>();
        foreach (var btn in _frameButtons)
            btn.ManagerUI = this;

        _radialMenuMove.PlayerController = _playerController;

        _radialMenuMove.gameObject.SetActive(false);
    }
}
