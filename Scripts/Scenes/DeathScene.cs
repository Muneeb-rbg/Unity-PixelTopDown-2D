using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    public void RetryGame()
    {
        SceneController.LoadScene("GameStart"); // Nutze SceneController statt SceneManager
    }

    public void QuitGame()
    {
        Application.Quit(); // Beendet das Spiel
    }

    public void OpenSettingsMenu()
    {
        SceneController.LoadScene("SettingsScene"); // Nutze SceneController
    }

    public void ReturnToHome()
    {
        SceneController.LoadScene("HomeScene"); // Nutze SceneController
    }
}
