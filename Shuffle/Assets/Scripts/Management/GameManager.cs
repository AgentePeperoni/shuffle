using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _delayBetweenFrames;

    private TimeManager _timeManager;
    private PlayerManager _playerManager;
    private Sequencer _sequencer;

    public void Play()
    {
        //StartCoroutine(PlayFramesWithDelay());
    }

    private void Start()
    {
        _timeManager = FindObjectOfType<TimeManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _sequencer = FindObjectOfType<Sequencer>();
        
        _sequencer.Timeline.OnFrameChanged += _timeManager.OnFrameChanged;
        _sequencer.OnStateChanged += _playerManager.OnActionsChanged;
    }

    //private IEnumerator PlayFramesWithDelay()
    //{
    //    while (_timeManager.CurrentFrame < 9)
    //    {
    //        _timeManager.SetCurrentFrame(_timeManager.CurrentFrame + 1);
    //        yield return new WaitForSeconds(_delayBetweenFrames);
    //    }

    //    yield return new WaitForSeconds(2);

    //    StopPlaying();
    //}
}
