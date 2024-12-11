using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum TurnState { PlayerTurn, EnemyTurn }
    public TurnState currentState;

    [SerializeField] private List<Unit> playerUnits; // Reference to player units
    [SerializeField] private List<Unit> enemyUnits;  // Reference to enemy units
    [SerializeField] public int roundCounter = 20;

    void Start()
    {
        playerUnits = new List<Unit>();

        GameObject[] playerUnitsGameObjects = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject unitObj in playerUnitsGameObjects)
        {
            Unit unit = unitObj.GetComponent<Unit>();
            if (unit != null)
            {
                playerUnits.Add(unit);
            }
        }

        enemyUnits = new List<Unit>();

        GameObject[] enemyUnitsGameObjects = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject unitObj in enemyUnitsGameObjects)
        {
            Unit unit = unitObj.GetComponent<Unit>();
            if (unit != null)
            {
                enemyUnits.Add(unit);
            }
        }

        // Start the game with the player's turn
        currentState = TurnState.PlayerTurn;
        ResetPlayerUnits();
        StartPlayerTurn();
    }

    void Update()
    {
        if (currentState == TurnState.PlayerTurn && AllPlayerUnitsOutOfMovement())
        {
            EndPlayerTurn();
        }
    }

    private bool AllPlayerUnitsOutOfMovement()
    {
        foreach (Unit unit in playerUnits)
        {
            if (unit.HasMovementPoints())
            {
                return false;
            }
        }
        return true; // All player units are out of movement points
    }

    public void EndPlayerTurn()
    {
        Debug.Log("Player turn has ended.");
        currentState = TurnState.EnemyTurn;
        ResetEnemyUnits();
        StartCoroutine(StartEnemyTurn());
    }

    private IEnumerator StartEnemyTurn()
    {
        Debug.Log("Enemy turn has started.");

        // Create a list of enemy AI coroutines to run
        List<Coroutine> enemyCoroutines = new List<Coroutine>();

        foreach (Unit enemy in enemyUnits)
        {
            if (enemy != null)  // Check if the enemy unit is not destroyed
            {
                EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyCoroutines.Add(StartCoroutine(enemyAI.EnemyTurn()));
                }
                else
                {
                    Debug.LogWarning($"EnemyAI component not found on {enemy.name}");
                }
            }
            else
            {
                Debug.LogWarning("An enemy unit has been destroyed, skipping.");
            }
        }

        // Wait for all enemy coroutines to finish
        foreach (Coroutine coroutine in enemyCoroutines)
        {
            yield return coroutine; // This will yield until the coroutine is done
        }

        EndEnemyTurn();
    }


    public void EndEnemyTurn()
    {
        Debug.Log("Enemy turn has ended.");

        roundCounter--;
        Debug.Log($"Round {10 - roundCounter} has ended. {roundCounter} rounds left.");

        if (roundCounter <= 0)
        {
            Debug.Log("Game Over!");
            return;
        }

        else
        {
            currentState = TurnState.PlayerTurn;
            ResetPlayerUnits();
            StartPlayerTurn();
        }
    }

    private void StartPlayerTurn()
    {
        Debug.Log("Player turn has started.");
        // Enable player controls, UI updates, etc.
    }

    private void ResetPlayerUnits()
    {
        foreach (Unit unit in playerUnits)
        {
            unit.ResetMovementPoints();
            unit.ResetActions();
        }
    }

    private void ResetEnemyUnits()
    {
        foreach (Unit unit in enemyUnits)
        {
            unit.ResetMovementPoints();
        }
    }
}