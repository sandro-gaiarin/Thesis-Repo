using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    GridManager gridManager;

    public GameObject selectedUnit;
    bool unitSelected = false;
    private int selectedUnitCurrentMovementPoints;
    [SerializeField] public bool isAttacking = false;
    public GameObject attackTarget;
    [SerializeField] LayerMask tileLayerMask;
    public GameObject cameraController;
    public InventoryManager inventoryManager;

    public Material highlightMaterial;
    public Material defaultMaterial;

    private List<GameObject> highlightedTiles = new List<GameObject>();

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        cameraController = GameObject.Find("Main Camera");
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    void Update()
    {
        HandleMouseInput();
        //HandleKeyboardInput();
    }
    void HandleMouseInput()
    {

          if (Input.GetMouseButtonDown(0))
            {
            if (UnityEngine.EventSystems.EventSystem.current != null &&
                UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
             {
                Debug.Log("Clicked on UI, ignoring.");
                return;
             }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayerMask))
            {
                Debug.Log($"Hit: {hit.transform.name}, Tag: {hit.transform.tag}");

                if (!isAttacking && hit.transform.CompareTag("PlayerUnit"))
                {
                    ClearHighlightedTiles(); // Clear old highlights

                    selectedUnit = hit.transform.gameObject;
                    unitSelected = true;

                    // Directly call SnapToUnit to ensure camera updates immediately after selection
                    //cameraController.GetComponent<CameraScript>().SnapToUnit(selectedUnit);  // Pass selectedUnit directly

                    selectedUnitCurrentMovementPoints = selectedUnit.GetComponent<Unit>().currentMovementPoints;
                    Debug.Log("Unit Selected");

                    HighlightReachableTiles(); // Highlight reachable tiles
                    return;
                }

                if (unitSelected && selectedUnit != null && selectedUnitCurrentMovementPoints > 0 && hit.transform.CompareTag("Tile"))
                {
                    Labeller tileLabeller = hit.transform.GetComponent<Labeller>();
                    if (tileLabeller == null) return;

                    Vector2Int targetCords = tileLabeller.cords;
                    Vector2Int startCords = new Vector2Int(
                        Mathf.RoundToInt(selectedUnit.transform.position.x / gridManager.UnityGridSize),
                        Mathf.RoundToInt(selectedUnit.transform.position.z / gridManager.UnityGridSize)
                    );

                    int manhattanDistance = Mathf.Abs(targetCords.x - startCords.x) + Mathf.Abs(targetCords.y - startCords.y);

                    if (highlightedTiles.Contains(hit.transform.gameObject))
                    {
                        ClearHighlightedTiles();
                        if (manhattanDistance <= selectedUnitCurrentMovementPoints)
                        {
                            selectedUnit.transform.position = new Vector3(
                                targetCords.x * gridManager.UnityGridSize,
                                selectedUnit.transform.position.y,
                                targetCords.y * gridManager.UnityGridSize
                            );

                            Debug.Log("Unit Moved");

                            selectedUnit.GetComponent<Unit>().MoveUnit(targetCords);
                            selectedUnitCurrentMovementPoints -= manhattanDistance;
                            selectedUnit.GetComponent<Unit>().currentMovementPoints = selectedUnitCurrentMovementPoints;

                            Debug.Log($"Remaining movement points: {selectedUnitCurrentMovementPoints}");
                            HighlightReachableTiles();
                        }
                        else
                        {
                            Debug.Log("Target out of movement range!");
                        }
                    }
                }

                if (isAttacking && (hit.transform.CompareTag("PlayerUnit") || hit.transform.CompareTag("EnemyUnit")))
                {
                    selectedUnit?.GetComponent<Unit>().Attack(hit.transform.GetComponent<Unit>());
                    isAttacking = false;
                }
            }
        }
    
        
    }





    public void EnterAttackMode() { 
     if (selectedUnit.GetComponent<Unit>().numberOfActions > 0)
                    {
                        isAttacking = true;
                        Debug.Log("Attack Mode");
                    }
                    else
                        {
                            Debug.Log("Out of Actions");
                        }
    }

    /*void HandleKeyboardInput()
        {
            if (selectedUnit != null)
            {
                if (unitSelected)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (selectedUnit.GetComponent<Unit>().numberOfActions > 0)
                        {
                            isAttacking = true;
                            Debug.Log("Attack Mode");
                        }
                        else
                        {
                            Debug.Log("Out of Actions");
                        }
                    }

                    bool moved = false;
                    Vector2Int targetGridPosition = new Vector2Int(
                        Mathf.RoundToInt(selectedUnit.transform.position.x / gridManager.UnityGridSize),
                        Mathf.RoundToInt(selectedUnit.transform.position.z / gridManager.UnityGridSize)
                    );

                    selectedUnitCurrentMovementPoints = selectedUnit.GetComponent<Unit>().currentMovementPoints;
                    if (selectedUnitCurrentMovementPoints <= 0) return;

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
                            selectedUnit.GetComponent<Unit>().currentMovementPoints--;
                            Debug.Log($"Unit moved to: {targetGridPosition}, Remaining movement points: {selectedUnit.GetComponent<Unit>().currentMovementPoints}");
                        }
                    }
                }
            }
        }*/

    void HighlightReachableTiles()
    {
        if (selectedUnit == null) return; // Prevent null reference on camera movement

        ClearHighlightedTiles(); // Clear old highlights

        Vector2Int startCords = new Vector2Int(
            Mathf.RoundToInt(selectedUnit.transform.position.x / gridManager.UnityGridSize),
            Mathf.RoundToInt(selectedUnit.transform.position.z / gridManager.UnityGridSize)
        );

        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();

        frontier.Enqueue(startCords);
        distances[startCords] = 0;

        while (frontier.Count > 0)
        {
            Vector2Int current = frontier.Dequeue();
            int currentDistance = distances[current];

            if (currentDistance < selectedUnitCurrentMovementPoints)
            {
                foreach (Vector2Int direction in new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right })
                {
                    Vector2Int neighbor = current + direction;

                    if (!distances.ContainsKey(neighbor))
                    {
                        GameObject tile = gridManager.GetTileGameObjectAtPosition(neighbor);
                        if (tile != null && !tile.CompareTag("Unwalkable"))
                        {
                            frontier.Enqueue(neighbor);
                            distances[neighbor] = currentDistance + 1;

                            // Highlight the tile
                            MeshRenderer renderer = tile.GetComponentInChildren<MeshRenderer>();
                            if (renderer != null)
                            {
                                renderer.material = highlightMaterial;
                                highlightedTiles.Add(tile);
                            }
                        }
                    }
                }
            }
        }
    }


    void ClearHighlightedTiles()
    {
        foreach (GameObject tile in highlightedTiles)
        {
            MeshRenderer renderer = tile.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = defaultMaterial;
            }
        }
        highlightedTiles.Clear();
    }
}
