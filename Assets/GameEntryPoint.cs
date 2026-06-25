using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        var dynamicWheel = GameObject.Find("Canvas/Circle").GetComponent<DynamicWheel>();
        dynamicWheel.GenerateWheel();

        var enemyDynamicWheel1 = GameObject.Find("Canvas/EnemyCircle").GetComponent<EnemyWheel1>();
        enemyDynamicWheel1.GenerateWheel();

        TextMeshProUGUI playerPoints = GameObject.Find("Canvas/PointsUI/PlayerPoints").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI enemyPoints = GameObject.Find("Canvas/PointsUI/EnemyPoints").GetComponent<TextMeshProUGUI>();
        playerPoints.text = "0";
        enemyPoints.text = "0";

        var turnsManager = FindAnyObjectByType<TurnsManager>().GetComponent<TurnsManager>();
        turnsManager.UpdateTurnsCounter();
    }
}
