using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTurnOn : MonoBehaviour
{
    public Light LampLight;
    void Start()
    {
        LampLight.intensity = 12;
    }

    void Update()
    {
        LampLightRecover();
    }

    private void LampLightRecover()
    {
        if (gameObject.tag == "Finish")
        {
            LampLight.intensity = 1;
            gameObject.tag = "Recovering";
            StartCoroutine(LightRecover());
        }
    }
    IEnumerator LightRecover()
    {
        while (LampLight.intensity<12)
        {
            yield return new WaitForSeconds(2);
            yield return new WaitForSeconds(1);
            LampLight.intensity++;
            yield return null;
        }
        if (LampLight.intensity==12)
        {
            gameObject.tag = "Lamp";
        }
    }
}