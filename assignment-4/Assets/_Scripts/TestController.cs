using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class TestController : MonoBehaviour
{
    //public int speed;
    Rigidbody rb;

    public LightManager LightManager;
    public Light PumpkinLight;
    public Image Pumkin;
    public TimeCount TimeCount;

    public GameObject TutorialPanel;
    public Text TutorailText;
    Image im;
    Text te;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        im = TutorialPanel.GetComponent<Image>();
        te = TutorailText.GetComponent<Text>();
        //rb.freezeRotation = true;
        //speed = 8;
        LightManager.light = 20;
        PumpkinLight.intensity = 4;
    }

    void Update()
    {
        //transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        Pumkin.fillAmount = LightManager.light / 100f;
        StartCoroutine(fadeIn());
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lamp")
        {
            if (LightManager.light < 100)
            {
                LightManager.updateLight(10);
                Debug.Log("hit");
                if (PumpkinLight.intensity < 16)
                {
                    PumpkinLight.intensity+=2;
                    other.gameObject.tag = "Finish";
                }
            }           
        }
        if (other.gameObject.tag == "Enemy")
        {
            if (LightManager.light > 0)
            {
                LightManager.updateLight(-10);
                if (PumpkinLight.intensity >0)
                {
                    PumpkinLight.intensity --;
                }
            }
        }
    }

    private void Die() //die state
    {
        if(LightManager.light == 0||TimeCount.TotalTime==0)
        {
            gameObject.SetActive(false);
        }

    }
    IEnumerator fadeIn() //tutorial fadein function
    {
        while (im.color.a > 0)
        {
            float newAlpha = im.color.a - 0.5f * Time.deltaTime;
            im.color = new Color(0, 0, 0, newAlpha);
            yield return new WaitForSeconds(10);
            float newteAlpha = te.color.a - 0.5f * Time.deltaTime;
            te.color = new Color(0, 0, 0, newAlpha);
            yield return null;

        }
    }
}
*/