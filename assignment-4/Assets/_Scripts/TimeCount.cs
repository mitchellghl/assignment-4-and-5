using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    public  Text timecountText;
    public int TotalTime;
    void Start()
    {
        TotalTime = 180;
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public IEnumerator CountDown()
    {
        while (TotalTime >= 0)
        {
            timecountText.GetComponent<Text>().text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            TotalTime--;

        }
    }
}
