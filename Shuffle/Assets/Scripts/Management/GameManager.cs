using System.Collections;

using UnityEngine;

public class GameManager : PreparationObject
{
    public bool IsPlaying { get; private set; }

    [Header("Основные Настройки")]
    [SerializeField]
    private float _delayBetweenFrames;

    [Space]
    [Header("Настройки Отображения")]
    [SerializeField]
    private Color _moveForwardColor = Color.green;
    [SerializeField]
    private Color _moveRightColor = Color.cyan;
    [SerializeField]
    private Color _moveBackwardColor = Color.red;
    [SerializeField]
    private Color _moveLeftColor = Color.magenta;

    private TimeManager _timeManager;
    private PlayerManager _playerManager;
    private Sequencer _sequencer;

    public void Play()
    {
        _sequencer.LockSequencer(true);
        StartCoroutine(PlayFramesWithDelay());
    }

    public void ResetSequencer()
    {
        _sequencer.ResetActionLines();
        _sequencer.Timeline.SetSliderValue(0);
    }

    private IEnumerator Start()
    {
        _timeManager = FindObjectOfType<TimeManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _sequencer = FindObjectOfType<Sequencer>();
        
        _sequencer.Timeline.OnFrameChanged += _timeManager.OnFrameChanged;
        _sequencer.OnStateChanged += _playerManager.OnActionsChanged;

        _playerManager.GameManager = this;

        for (int i = 0; i < _sequencer.ActionLines.Length; ++i)
        {
            ActionLine line = _sequencer.ActionLines[i];
            yield return new WaitUntil(() => line.IsReady);

            switch (line.LineAction)
            {
                case Actions.MoveForward:
                    line.SetColorToFrames(_moveForwardColor);
                    break;
                case Actions.MoveRight:
                    line.SetColorToFrames(_moveRightColor);
                    break;
                case Actions.MoveBackward:
                    line.SetColorToFrames(_moveBackwardColor);
                    break;
                case Actions.MoveLeft:
                    line.SetColorToFrames(_moveLeftColor);
                    break;
                default:
                    line.SetColorToFrames(Color.black);
                    break;
            }
        }
    }

    private IEnumerator PlayFramesWithDelay()
    {
        IsPlaying = true;

        _sequencer.Timeline.SetSliderValue(0);

        for (int i = 0; i < _playerManager.Player.TimeObjectActions.Count + 1; ++i)
        {
            _sequencer.Timeline.SetSliderValue(i);
            yield return new WaitForSeconds(_delayBetweenFrames);
        }

        IsPlaying = false;
        _sequencer.LockSequencer(false);
    }
}
