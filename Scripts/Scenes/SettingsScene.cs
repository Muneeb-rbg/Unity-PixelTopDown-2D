using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScene : MonoBehaviour
{
    public void SetVolume(float volume)
    {
        // Logik für die Lautstärkeregelung
        Debug.Log("Volume set to: " + volume);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        // Logik für die Grafikqualität
        Debug.Log("Graphics quality set to: " + qualityIndex);
    }

    public void GoBack()
    {
        // Gehe zurück zur vorherigen Szene
        if (!string.IsNullOrEmpty(SceneController.previousScene))
        {
            SceneManager.LoadScene(SceneController.previousScene); // Lade die vorherige Szene
        }
        else
        {
            Debug.LogWarning("Keine vorherige Szene gefunden!");
        }
    }
}
