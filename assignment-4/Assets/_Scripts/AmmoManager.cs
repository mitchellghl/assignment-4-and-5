using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : Subject
{
    public Observer displayAmmo;

    private void Start()
    {
        registerObserver(displayAmmo);
    }

    public void updateAmmo(int ammo)
    {
        Notify(ammo, NotificationType.AmmoUpdated);
    }

    public void updateMaxAmmo(int maxAmmo)
    {
        Notify(maxAmmo, NotificationType.MaxAmmoUpdated);
    }

    public void reloading(int ammo)
    {
        Notify(ammo, NotificationType.Reloading);
    }
}
