using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _delayBetweenFrames;

    private TimeManager _timeManager;
    private UIManager _managerUI;
    private PlayerController _playerController;

    public void Play()
    {
        _managerUI.LockUI();
        _timeManager.SetCurrentFrame(0);
        StartCoroutine(PlayFramesWithDelay());
    }

    public void StopPlaying()
    {
        _managerUI.UnlockUI();
        _timeManager.SetCurrentFrame(0);
    }

    private void Start()
    {
        _timeManager = FindObjectOfType<TimeManager>();
        _managerUI = FindObjectOfType<UIManager>();
        _playerController = FindObjectOfType<PlayerController>();

        _playerController.ManagerUI = _managerUI;
        _managerUI.PlayerController = _playerController;
    }

    private IEnumerator PlayFramesWithDelay()
    {
        while (_timeManager.CurrentFrame < 9)
        {
            _timeManager.SetCurrentFrame(_timeManager.CurrentFrame + 1);
            yield return new WaitForSeconds(_delayBetweenFrames);
        }

        yield return new WaitForSeconds(2);

        StopPlaying();
    }
}
