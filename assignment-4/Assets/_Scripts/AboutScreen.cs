using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutScreen : MonoBehaviour
{
    public void BackMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void AboutScene()
    {
        SceneManager.LoadScene("AboutTeamScene");
    }
}
