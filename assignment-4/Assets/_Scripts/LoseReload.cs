using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseReload : MonoBehaviour
{
    public void resetscreen()
    {
        SceneManager.LoadScene("MazeGeneratorTEST");
    }
}