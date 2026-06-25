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
    [SerializeField] private EnemyWheel2 enemyWheel2;
    [SerializeField] private EnemyWheel2 enemyWheel3;
    [SerializeField] private Endings endingsScript;

    [SerializeField] private int totalTurns = 5;
    public int currentTurns = 0;

    private int round = 1;

    public int droolingCatEffect = 0;

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
                if (currentTurns >= totalTurns)
                {
                    OnEnemyTurnEnd();
                    return;
                }

                if (droolingCatEffect > 0)
                {
                    droolingCatEffect--;
                    UpdateTurnsCounter();
                    SetState(TurnState.PlayerTurn);
                }
                else
                {
                    if (round == 1)
                    {
                        enemyWheel1.Spin();
                    }
                    else if (round == 2)
                    {
                        enemyWheel2.Spin();
                    }
                    else
                    {
                        enemyWheel3.Spin();
                    }
                }
                break;
        }
    }

    public void OnEnemyTurnEnd()
    {
        if (currentTurns >= totalTurns)
        {
            if (playerWheel.playerPoints <= enemyWheel1.enemyPoints && enemyWheel1 != null)
            {
                enemyWheel1.MoveAwayAndDelete();
                droolingCatEffect = 0;
                UpdateTurnsCounter();
                endingsScript.ActivateLoseScreen();
                return;
            }
            else if (playerWheel.playerPoints <= enemyWheel2.enemyPoints && enemyWheel2 != null)
            {
                enemyWheel2.MoveAwayAndDelete();
                droolingCatEffect = 0;
                UpdateTurnsCounter();
                endingsScript.ActivateLoseScreen();
                return;
            }
            else if (playerWheel.playerPoints <= enemyWheel3.enemyPoints && enemyWheel3 != null)
            {
                enemyWheel3.MoveAwayAndDelete();
                droolingCatEffect = 0;
                UpdateTurnsCounter();
                endingsScript.ActivateLoseScreen();
                return;
            }

            if (playerWheel.playerPoints >= enemyWheel1.enemyPoints && enemyWheel1 != null)
            {
                enemyWheel1.MoveAwayAndDelete();
                currentTurns = 0;
                enemyWheel2.GenerateWheel();
                round++;
                playerWheel.ResetPoints();
                droolingCatEffect = 0;
                UpdateTurnsCounter();
                SetState(TurnState.PlayerTurn);
            }
            else if (playerWheel.playerPoints >= enemyWheel2.enemyPoints && enemyWheel2 != null)
            {
                enemyWheel2.MoveAwayAndDelete();
                currentTurns = 0;
                enemyWheel3.GenerateWheel();
                round++;
                playerWheel.ResetPoints();
                droolingCatEffect = 0;
                UpdateTurnsCounter();
                SetState(TurnState.PlayerTurn);
            }
            else if (playerWheel.playerPoints >= enemyWheel3.enemyPoints && enemyWheel3 != null)
            {
                enemyWheel3.MoveAwayAndDelete();
                droolingCatEffect = 0;
                UpdateTurnsCounter();
                endingsScript.ActivateWinningScreen();
            }
            else
            {
                endingsScript.ActivateLoseScreen();
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
