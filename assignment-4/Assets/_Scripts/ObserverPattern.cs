using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType {LightUpdated, AudioUpdated, AmmoUpdated, MaxAmmoUpdated, Reloading}; //Put the notification types here

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(object o, NotificationType n);
}

public abstract class Subject : MonoBehaviour
{
    protected List<Observer> observers = new List<Observer>();

    public void registerObserver(Observer o)
    {
        observers.Add(o);
    }

    public void unregisterObserver(Observer o)
    {
        observers.Remove(o);
    }

    public void Notify(object o, NotificationType n)
    {
        foreach(Observer ob in observers)
        {
            ob.OnNotify(o, n);
        }
    }
}
