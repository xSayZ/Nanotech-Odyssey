using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.Log("There are more than one instance of GameManager in the scene");
        }

        _instance = this;
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }
}

