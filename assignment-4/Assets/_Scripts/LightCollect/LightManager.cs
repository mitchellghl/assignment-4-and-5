using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightManager : Subject
{
    public int light = 20;
    public Observer DisplayLight;

    private void Start()
    {
        registerObserver(DisplayLight);
    }


    public void updateLight(int energy)
    {
        light += energy;
        Notify(light, NotificationType.LightUpdated);

    }

}
