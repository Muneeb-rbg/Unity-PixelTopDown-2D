using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static string previousScene = ""; // Speichert den Namen der vorherigen Szene

    public static void LoadScene(string sceneName)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // Speichere die aktuelle Szene als vorherige Szene, außer wenn die aktuelle Szene die Zielszene ist
        if (currentScene != sceneName)
        {
            previousScene = currentScene;
            Debug.Log("Previous scene set to: " + previousScene);
        }

        // Lade die neue Szene
        SceneManager.LoadScene(sceneName);
    }
}
