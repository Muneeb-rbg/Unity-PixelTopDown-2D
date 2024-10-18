using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneController.LoadScene("GameStart"); // Name deiner Spielszene hier einsetzen
    }

    public void OpenSettingsMenu()
    {
        SceneController.LoadScene("SettingsScene"); // Wechsle zur Settings-Szene
    }

    public void Achievements()
    {
        SceneController.LoadScene("SettingsScene"); // Nutze SceneController
    }

    public void Extras()
    {
        SceneController.LoadScene("DeathScene"); // Nutze SceneController
    }

    public void QuitGame()
    {
        Application.Quit(); // Beendet das Spiel
    }
}
