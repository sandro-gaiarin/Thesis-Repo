using UnityEngine;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalDocumentCount = 0;
    private int actOneDayOneDocumentCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want it to persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDocument()
    {
        totalDocumentCount++;
        Debug.Log($"Document picked up. Total: {totalDocumentCount}");
    }

    public int GetDocumentCount()
    {
        return totalDocumentCount;
    }

    // You can also add support for act/day-specific document tracking if needed:
    public void AddDayOneDocument()
    {
        actOneDayOneDocumentCount++;
        Debug.Log($"Day One Documents: {actOneDayOneDocumentCount}");
    }

    public int GetDayOneDocumentCount()
    {
        return actOneDayOneDocumentCount;
    }
}
