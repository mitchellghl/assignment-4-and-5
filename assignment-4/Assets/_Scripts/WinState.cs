using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour
{
    private bool win = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Loadout());

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

            win = true;
        }

    }
    private IEnumerator Loadout()
    {
        if (win==true)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("AboutTeamScene");
        }
    }
    
}
