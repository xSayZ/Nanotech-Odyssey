using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static UIManager _ui;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.Log("There are more than one instance of GameManager in the scene");
        }

        _instance = this;

        _ui = GetComponent<UIManager>();
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        _ui.UpdateHP(3);
    }
}

