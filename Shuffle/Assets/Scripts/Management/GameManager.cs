using System.Collections;

using UnityEngine;

public class GameManager : PreparationObject
{
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
        _sequencer.SequencerSlider.SetSliderValue(0);
    }

    private void Start()
    {
        _timeManager = FindObjectOfType<TimeManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _sequencer = FindObjectOfType<Sequencer>();
        
        _sequencer.SequencerSlider.onFrameChanged += _timeManager.OnFrameChanged;
        _sequencer.onActionChanged += _playerManager.OnActionsChanged;
    }

    private IEnumerator PlayFramesWithDelay()
    {
        _sequencer.SequencerSlider.SetSliderValue(0);

        for (int i = 0; i < _playerManager.Player.TimeObjectActions.Count + 1; ++i)
        {
            _sequencer.SequencerSlider.SetSliderValue(i);
            yield return new WaitForSeconds(_delayBetweenFrames);
        }
        
        _sequencer.LockSequencer(false);
    }
}
