using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : Observer
{
    public AudioClip gurgle;
    public AudioClip playerBloop;
    public AudioClip enemyBloop;
    public AudioClip reloadBloop;
    public AudioClip splash;
    public AudioClip boom;
    public AudioClip ready;
    public AudioClip attack;
    public AudioClip hurt;
    public AudioSource channel1; //gun
    public AudioSource channel2; //enemy
    public AudioSource channel3; //player

    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.AudioUpdated)
        {
            if(o.ToString() == "gurgle")
            {
                channel1.clip = gurgle;
                channel1.Play();
            }

            if (o.ToString() == "hurt")
            {
                channel2.clip = hurt;
                channel2.Play();
            }

            if (o.ToString() == "attack")
            {
                channel2.clip = attack;
                channel2.Play();
            }

            if (o.ToString() == "bloop")
            {
                channel3.clip = playerBloop;
                channel3.Play();
            }

            if(o.ToString() == "splash")
            {
                channel1.clip = splash;
                channel1.Play();
            }

            if(o.ToString() == "enemy")
            {
                channel2.clip = enemyBloop;
                channel2.Play();
            }

            if (o.ToString() == "boom")
            {
                channel2.clip = boom;
                channel2.Play();
            }

            if (o.ToString() == "reload")
            {
                channel3.clip = reloadBloop;
                channel3.Play();
            }

            if (o.ToString() == "ready")
            {
                channel3.clip = ready;
                channel3.Play();
            }
        }

    }
}
