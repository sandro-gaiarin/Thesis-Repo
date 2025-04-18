using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your Enemy prefab
    public GridManager gridManager;

    void Start()
    {
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
        }

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        List<Vector2Int> walkablePositions = new List<Vector2Int>();

        // Build list of walkable tiles
        foreach (Transform tile in gridManager.transform)
        {
            Labeller labeller = tile.GetComponent<Labeller>();
            if (labeller != null && tile.CompareTag("Tile"))
            {
                walkablePositions.Add(labeller.cords);
            }
        }

        int enemyCount = Random.Range(1, 6); // 1 to 5 enemies

        for (int i = 0; i < enemyCount && walkablePositions.Count > 0; i++)
        {
            int index = Random.Range(0, walkablePositions.Count);
            Vector2Int spawnPosition = walkablePositions[index];
            walkablePositions.RemoveAt(index); // avoid duplicates

            Vector3 worldPos = new Vector3(
                spawnPosition.x * gridManager.UnityGridSize,
                .5f,
                spawnPosition.y * gridManager.UnityGridSize
            );

            GameObject enemy = Instantiate(enemyPrefab, worldPos, Quaternion.identity);

            // ✅ Assign numbered name to unit
            Unit unit = enemy.GetComponent<Unit>();
            if (unit != null)
            {
                unit.unitName = $"Enemy {i + 1}";
                enemy.name = unit.unitName; // optional: rename GameObject too
            }
        }
    }
}
