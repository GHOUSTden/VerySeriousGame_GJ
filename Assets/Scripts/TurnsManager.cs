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

    [SerializeField] private int totalTurns = 10;
    private int currentTurns = 0;
    
    [SerializeField] private TextMeshProUGUI turnsCounterUI;

    public void SetState(TurnState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case TurnState.PlayerTurn:
                break;

            case TurnState.EnemyTurn:
                OnEnemyTurn();
                enemyWheel1.Spin();
                break;
        }
    }

    public void OnEnemyTurn()
    {
        currentTurns++;

        UpdateTurnsCounter();

        if (currentTurns >= totalTurns)
        {
            return; // Do something or idk
        }
        else
        {
            SetState(TurnState.PlayerTurn);
        }
    }

    private void UpdateTurnsCounter()
    {
        turnsCounterUI.text = $"{currentTurns}/{totalTurns}";
    }
}
