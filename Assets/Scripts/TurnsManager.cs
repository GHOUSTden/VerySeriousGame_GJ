using TMPro;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    public enum TurnState
    {
        PlayerTurn,
        EnemyTurn,
    }
    public TurnState currentState;

    [SerializeField] private DynamicWheel playerWheel;
    [SerializeField] private EnemyWheel1 enemyWheel1;

    [SerializeField] private int totalTurns = 5;
    public int currentTurns = 0;
    
    [SerializeField] private TextMeshProUGUI turnsCounterUI;

    public void SetState(TurnState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case TurnState.PlayerTurn:
                Debug.Log("Player Turn");
                break;

            case TurnState.EnemyTurn:
                Debug.Log("Enemy Turn");
                enemyWheel1.Spin();
                break;
        }
    }

    public void OnEnemyTurnEnd()
    {
        if (currentTurns >= totalTurns)
        {
            if (playerWheel.playerPoints >= enemyWheel1.enemyPoints)
            {
                Debug.Log("You Win!");
            }
            else
            {
                Debug.Log("Loser");
            }
        }
        else
        {
            SetState(TurnState.PlayerTurn);
        }
    }

    public void UpdateTurnsCounter()
    {
        turnsCounterUI.text = $"{currentTurns}/{totalTurns}";
    }
}
