using System;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    private static GameEntryPoint _instance;
    public static GameEntryPoint Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameEntryPoint>();
                if (_instance == null)
                {
                    GameObject GameObject = new GameObject("GameManager");
                    _instance = GameObject.AddComponent<GameEntryPoint>();
                }
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        var start = GameEntryPoint.Instance;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        StartGame();
    }

    private void StartGame()
    {
        var dynamicWheel = GameObject.Find("Canvas/Wheel").GetComponent<DynamicWheel>();

        dynamicWheel.GenerateWheel();
    }
}
