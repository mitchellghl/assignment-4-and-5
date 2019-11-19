using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Subject
{
    public Observer AudioClipPlayer;

    private void Start()
    {
        registerObserver(AudioClipPlayer);
    }


    public void updateAudio(string clip)
    {
        Notify(clip, NotificationType.AudioUpdated);
    }

}
