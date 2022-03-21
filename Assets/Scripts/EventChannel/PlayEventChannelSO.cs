using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = FILE_NAME + "Play", menuName = MENU_NAME + "Play")]
public class PlayEventChannelSO : EventChannelSO
{
    public event UnityAction onPlayStarted;

    public void OnPlayStarted()
    {
        onPlayStarted?.Invoke();
    }
}
