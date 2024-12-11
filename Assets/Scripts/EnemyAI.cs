using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GridManager gridManager;
    private Unit unit;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        unit = GetComponent<Unit>();
    }

    public IEnumerator EnemyTurn()
    {
        // Continue moving while the unit has movement points
        while (unit.HasMovementPoints())
        {
            // Move towards the player, but only if not adjacent
            MoveTowardsNearestPlayer();

            // Check if the enemy is adjacent to a player unit and attack
            if (AttackNearestPlayerUnit())
            {
                // End the enemy turn after attacking
                break;
            }

            // Wait for a short time before making the next move
            yield return new WaitForSeconds(1f); // Adjust delay as needed
        }
    }

    private void MoveTowardsNearestPlayer()
    {
        Debug.Log("Enemy is moving");
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        GameObject nearestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        // Find the nearest player unit
        foreach (GameObject player in playerUnits)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null)
        {
            Vector2Int currentGridPosition = new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize)
            );

            Vector2Int targetGridPosition = new Vector2Int(
                Mathf.RoundToInt(nearestPlayer.transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(nearestPlayer.transform.position.z / gridManager.UnityGridSize)
            );

            // Check for cardinal adjacency
            int xDifference = Mathf.Abs(targetGridPosition.x - currentGridPosition.x);
            int yDifference = Mathf.Abs(targetGridPosition.y - currentGridPosition.y);

            // If the enemy is adjacent in cardinal directions, stop moving
            if ((xDifference == 1 && yDifference == 0) || (xDifference == 0 && yDifference == 1))
            {
                Debug.Log("Enemy is adjacent to the player, stopping.");
                unit.currentMovementPoints = 0; // Stop moving
                return; // Stop movement
            }

            // Calculate the direction towards the player, but restrict to cardinal movement
            Vector2Int direction = new Vector2Int(
                (targetGridPosition.x > currentGridPosition.x) ? 1 :
                (targetGridPosition.x < currentGridPosition.x) ? -1 : 0,
                (targetGridPosition.y > currentGridPosition.y) ? 1 :
                (targetGridPosition.y < currentGridPosition.y) ? -1 : 0
            );

            // Make sure the movement is only in cardinal directions
            if (direction.x != 0 && direction.y != 0)
            {
                // Prefer horizontal movement if both directions are available
                direction.y = 0; // Set vertical direction to 0
            }

            // Calculate the next position based on direction
            Vector2Int nextPosition = currentGridPosition + direction;

            // Check if the next tile is walkable
            GameObject targetTile = gridManager.GetTileGameObjectAtPosition(nextPosition);
            if (targetTile != null && targetTile.CompareTag("Tile") && unit.HasMovementPoints() && !IsPlayerOccupied(nextPosition))
            {
                // Move the unit towards the nearest player
                unit.MoveUnit(nextPosition); // Deduct movement points
                transform.position = new Vector3(
                    nextPosition.x * gridManager.UnityGridSize,
                    transform.position.y,
                    nextPosition.y * gridManager.UnityGridSize
                );

                Debug.Log($"Enemy moved towards player at: {nextPosition}");
            }
            else
            {
                unit.currentMovementPoints = 0;
                Debug.Log($"Next position {nextPosition} is not walkable or is occupied by a player unit, stopping movement.");
            }
        }
        else
        {
            Debug.Log("No player units found!");
        }
    }

    private bool IsPlayerOccupied(Vector2Int position)
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject player in playerUnits)
        {
            Vector2Int playerGridPosition = new Vector2Int(
                Mathf.RoundToInt(player.transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(player.transform.position.z / gridManager.UnityGridSize)
            );

            // Check if the player unit is occupying the position or any adjacent positions
            if (playerGridPosition == position)
            {
                Debug.Log("Player unit is occupying the position.");
                return true; // Position is occupied by a player unit
            }
        }
        return false; // Position is not occupied
    }

    private bool AttackNearestPlayerUnit()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        GameObject nearestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        // Find the nearest player unit
        foreach (GameObject player in playerUnits)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null)
        {
            Vector2Int currentGridPosition = new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize)
            );

            Vector2Int targetGridPosition = new Vector2Int(
                Mathf.RoundToInt(nearestPlayer.transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(nearestPlayer.transform.position.z / gridManager.UnityGridSize)
            );

            // Check for cardinal adjacency
            int xDifference = Mathf.Abs(targetGridPosition.x - currentGridPosition.x);
            int yDifference = Mathf.Abs(targetGridPosition.y - currentGridPosition.y);

            // Attack if adjacent
            if ((xDifference == 1 && yDifference == 0) || (xDifference == 0 && yDifference == 1))
            {
                Unit playerUnit = nearestPlayer.GetComponent<Unit>();
                if (playerUnit != null)
                {
                    unit.Attack(playerUnit);
                    Debug.Log($"Enemy attacked {playerUnit.gameObject.name} for {unit.attackDamage} damage.");
                    return true; // Indicate that an attack occurred
                }
            }
        }

        return false; // No attack occurred
    }
}
