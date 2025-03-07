using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using PixelCrushers.DialogueSystem;

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

    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        Lua.RegisterFunction("LoadScene", this, SymbolExtensions.GetMethodInfo(() => LoadScene(string.Empty)));
        //Lua.RegisterFunction(nameof(AddOne), this, SymbolExtensions.GetMethodInfo(() => AddOne((double)0)));
    }



    // Method to load a scene
    public static void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneFromLua()
    {
        string sceneName = DialogueLua.GetVariable("NextScene").AsString; // Get scene from Lua
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log("Loading scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty or invalid!");
        }
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
