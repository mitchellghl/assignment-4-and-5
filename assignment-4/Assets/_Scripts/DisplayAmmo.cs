using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAmmo : Observer
{
    public string maxAmmo;

    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.MaxAmmoUpdated)
        {
            maxAmmo = o.ToString();
        }

        if (n == NotificationType.AmmoUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = o.ToString() + " / " + maxAmmo;
        }

        if (n == NotificationType.Reloading)
        {
            GetComponent<TextMeshProUGUI>().text = "Reloading...";
        }
    }
}
