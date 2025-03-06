using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneManager : MonoBehaviour
{
    // Singleton instance to ensure only one SceneManager exists
    public static SceneManager Instance;

    void Awake()
    {
        // Ensure only one instance of SceneManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Method to load a scene
    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    // Method to reload the current scene
    public void ReloadCurrentScene()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log("Reloading scene: " + currentScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }

    // Method to load the next scene (based on build index)
    public void LoadNextScene()
    {
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }
}
