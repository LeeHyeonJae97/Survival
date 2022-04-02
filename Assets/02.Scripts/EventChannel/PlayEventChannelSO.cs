using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = FILE_NAME + "Play", menuName = MENU_NAME + "Play")]
public class PlayEventChannelSO : EventChannelSO
{
    public event UnityAction onPlayStarted;
    public event UnityAction onWaveStarted;
    public event UnityAction onWaveFinished;

    public void OnPlayStarted()
    {
        Debug.Log("Play Start");
        onPlayStarted?.Invoke();
    }

    public void OnWaveStarted()
    {
        Debug.Log("Wave Started");
        onWaveStarted?.Invoke();
    }

    public void OnWaveFinished()
    {
        Debug.Log("Wave Finished");
        onWaveFinished?.Invoke();
    }
}
