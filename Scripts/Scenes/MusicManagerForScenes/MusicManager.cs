/*
/--------------------------------------------------\
| Der MusicManager verwaltet das Abspielen von Musik |
| im Spiel und sorgt dafür, dass beim Wechseln von   |
| Szenen die passende Musik abgespielt wird.        |
|                                                   |
| Funktionen des MusicManager:                      |
|                                                   |
| 1. Singleton-Implementierung, um sicherzustellen,  |
|    dass nur eine Instanz des Managers existiert,   |
|    die beim Szenenwechsel nicht zerstört wird.    |
| 2. Verknüpft die Musikbibliothek und die Audio-    |
|    Quelle, um Musik abzuspielen.                  |
| 3. Registriert Methoden für das Abspielen von      |
|    Musik beim Laden neuer Szenen über den Event-   |
|    Handler von SceneManager.                       |
| 4. Bietet die Methode `PlayMusic`, um Musik mit    |
|    Crossfade-Effekten abzuspielen, sowie eine      |
|    Coroutine, die das Fade-Out und Fade-In der      |
|    Musik steuert.                                  |
\--------------------------------------------------/
*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // Singleton-Instanz

    [SerializeField]
    private MusicLibrary musicLibrary; // Deine Musikbibliothek
    [SerializeField]
    private AudioSource musicSource; // AudioSource für Musik

    private void Awake()
    {
        // Überprüfen, ob bereits eine Instanz existiert
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Verhindert Zerstörung beim Szenenwechsel
        }
        else
        {
            Destroy(gameObject); // Falls eine Instanz existiert, lösche dieses Objekt
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Szenenwechsel registrieren
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Deregistriere Szenenwechsel
    }


//  wenn du für eine bestimmte scene bestimmte dauer des fades möchtest kannst du das so machen
//  PlayMusic("DeathMusic", 1.0f); // Schnellere Fade-Dauer für DeathScene
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SettingsScene") // Musik für die SettingsScene
        {
            PlayMusic("SettingsMusic"); // Stelle sicher, dass dieser Name dem in der Musikbibliothek entspricht
        }
        else if (scene.name == "DeathScene") // Musik für die DeathScene
        {
            PlayMusic("DeathMusic"); // Stelle sicher, dass dieser Name dem in der Musikbibliothek entspricht
        }
        else if (scene.name == "HomeScene") // Musik für die HomeScene
        {
            PlayMusic("HomeMusic"); // Angenommene Musik für die HomeScene
        }
        else if (scene.name == "GameStart") // Musik für die HomeScene
        {
            PlayMusic("GameStartMusic"); // Angenommene Musik für die HomeScene
        }
    }

    // Methode zum Abspielen von Musik mit Crossfade
    public void PlayMusic(string trackName, float fadeDuration = 2.0f) // schnelligkeit des fade-effekt hier
    {
        AudioClip nextTrack = musicLibrary.GetClipFromName(trackName); // Holen Sie sich den Clip

        // Überprüfen, ob die aktuelle Musik die gleiche wie die neue Musik ist
        if (musicSource.clip == nextTrack)
        {
            // Die gleiche Musik wird bereits abgespielt, keine Aktion erforderlich
            return;
        }

        StartCoroutine(AnimateMusicCrossfade(nextTrack, fadeDuration));
    }

    // Coroutine für das Crossfade von Musik
    private IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration)
    {
        float percent = 0f;

        // Fade-Out der aktuellen Musik
        while (percent < 1f)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f, 0f, percent); // Reduziere die Lautstärke
            yield return null; // Warten auf den nächsten Frame
        }

        // Setze den nächsten Track und spiele ihn ab
        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0f; // Zurücksetzen für das Fade-In

        // Fade-In der neuen Musik
        while (percent < 1f)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(0f, 1f, percent); // Erhöhe die Lautstärke
            yield return null; // Warten auf den nächsten Frame
        }
    }
}
