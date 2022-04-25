using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = FILE_NAME + "Play", menuName = MENU_NAME + "Play")]
public class PlayEventChannelSO : EventChannelSO
{
    public event UnityAction OnPlayStarted;
    public event UnityAction OnPlayFinished;
    public event UnityAction OnWaveStarted;
    public event UnityAction OnWaveFinished;
    public event UnityAction OnPaused;
    public event UnityAction OnResumed;

    public void StartPlay()
    {
        if (_log) Debug.Log("Play Started");
        OnPlayStarted?.Invoke();
    }

    public void FinishPlay()
    {
        if (_log) Debug.Log("Play Finished");
        OnPlayFinished?.Invoke();
    }

    public void StartWave()
    {
        if (_log) Debug.Log("Wave Started");
        OnWaveStarted?.Invoke();
    }

    public void FinishWave()
    {
        if (_log) Debug.Log("Wave Finished");
        OnWaveFinished?.Invoke();
    }

    public void Pause()
    {
        if (_log) Debug.Log("Pause");
        OnPaused?.Invoke();
    }

    public void Resume()
    {
        if (_log) Debug.Log("Resume");
        OnResumed?.Invoke();
    }
}
