using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TimeManager _timeManager;
    private UIManager _managerUI;
    private PlayerController _playerController;

    private void Start()
    {
        _timeManager = FindObjectOfType<TimeManager>();
        _managerUI = FindObjectOfType<UIManager>();
        _playerController = FindObjectOfType<PlayerController>();

        _playerController.ManagerUI = _managerUI;
        _managerUI.PlayerController = _playerController;
    }
}
