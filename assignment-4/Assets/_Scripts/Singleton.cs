using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    //put whatever things you want stored here:

    //to reference the things, type Singleton.Instance.[whatever you want to reference]

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = null;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
