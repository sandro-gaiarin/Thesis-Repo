using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationUnitController : MonoBehaviour
{

    public GameObject selectedUnit;
    bool unitSelected = false;
    GridManager gridManager;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyboardInput();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.Log($"Hit: {hit.transform.name}, Tag: {hit.transform.tag}");

            if (hit.transform.CompareTag("PlayerUnit"))
            {
                //ClearHighlightedTiles(); // Clear old highlights

                selectedUnit = hit.transform.gameObject;
                unitSelected = true;

                // Directly call SnapToUnit to ensure camera updates immediately after selection
                //cameraController.GetComponent<CameraScript>().SnapToUnit(selectedUnit);  // Pass selectedUnit directly

                //selectedUnitCurrentMovementPoints = selectedUnit.GetComponent<Unit>().currentMovementPoints;
                Debug.Log("Unit Selected");

                //HighlightReachableTiles(); // Highlight reachable tiles
                return;
            }
        }
    }

void HandleKeyboardInput()
    {
        if (selectedUnit != null)
        {
            if (unitSelected)
            {
               

                bool moved = false;
                Vector2Int targetGridPosition = new Vector2Int(
                    Mathf.RoundToInt(selectedUnit.transform.position.x / gridManager.UnityGridSize),
                    Mathf.RoundToInt(selectedUnit.transform.position.z / gridManager.UnityGridSize)
                );

                //selectedUnitCurrentMovementPoints = selectedUnit.GetComponent<Unit>().currentMovementPoints;
               // if (selectedUnitCurrentMovementPoints <= 0) return;

                if (Input.GetKeyDown(KeyCode.W)) { targetGridPosition += new Vector2Int(0, 1); moved = true; }
                else if (Input.GetKeyDown(KeyCode.S)) { targetGridPosition += new Vector2Int(0, -1); moved = true; }
                else if (Input.GetKeyDown(KeyCode.A)) { targetGridPosition += new Vector2Int(-1, 0); moved = true; }
                else if (Input.GetKeyDown(KeyCode.D)) { targetGridPosition += new Vector2Int(1, 0); moved = true; }

                if (moved)
                {
                    GameObject targetTile = gridManager.GetTileGameObjectAtPosition(targetGridPosition);
                    if (targetTile != null && !targetTile.CompareTag("Unwalkable"))
                    {
                        selectedUnit.transform.position = new Vector3(
                            targetGridPosition.x * gridManager.UnityGridSize,
                            selectedUnit.transform.position.y,
                            targetGridPosition.y * gridManager.UnityGridSize
                        );
                        //selectedUnit.GetComponent<Unit>().currentMovementPoints--;
                        Debug.Log($"Unit moved to: {targetGridPosition}, Remaining movement points: {selectedUnit.GetComponent<Unit>().currentMovementPoints}");
                    }
                }
            }
        }
    }
}
