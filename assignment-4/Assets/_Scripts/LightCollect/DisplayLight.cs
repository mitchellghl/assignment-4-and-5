using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DisplayLight : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.LightUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "Light:" + o.ToString();
        }

    }
}
