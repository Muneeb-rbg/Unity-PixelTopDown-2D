/*
/--------------------------------------------------\
| Der CharacterLeveling verwaltet das Levelsystem des    |
| Spiels und verfolgt den Fortschritt des Spielers. |
|                                                   |
| Funktionen des CharacterLevelings:                     |
|                                                   |
| 1. Verfolgt den aktuellen Spielerlevel und die    |
|    gesammelte Erfahrung (EXP) des Spielers.      |
| 2. Berechnet die EXP-Anforderungen für das nächste |
|    Level und erhöht den Spielerlevel, wenn        |
|    genügend EXP gesammelt wurde.                  |
| 3. Ermöglicht das Hinzufügen von EXP durch das    |
|    Besiegen von Gegnern, das Erfüllen von Quests   |
|    oder andere spielerische Aktionen.             |
| 4. Stellt sicher, dass der CharacterLeveling über      |
|    Szenen hinweg persistent bleibt, um den        |
|    Fortschritt des Spielers zu speichern.         |
| 5. Speichert und lädt den Fortschritt des         |
|    Spielers mithilfe von PlayerPrefs.             |
|                                                   |
| Hinweis: Der CharacterLeveling implementiert das        |
|    Singleton-Muster, um mehrere Instanzen zu       |
|    vermeiden und sicherzustellen, dass der         |
|    CharacterLeveling während des gesamten Spiels        |
|    verfügbar bleibt.                               |
\--------------------------------------------------/
*/

using UnityEngine;
using TMPro; // Für TextMeshPro

public class CharacterLeveling : MonoBehaviour
{
    public static CharacterLeveling instance; // Singleton-Instanz

    [SerializeField] public int currentLevel = 1; // Jetzt im Inspector veränderbar
    [SerializeField] public int currentEXP = 0; // Jetzt im Inspector veränderbar
    [SerializeField] public int expToNextLevel = 100; // Jetzt im Inspector veränderbar
    [SerializeField] private bool valuesFromInspector = true; // Flag für Werte aus dem Inspektor

    // Referenzen für UI-Elemente
    public TextMeshProUGUI levelText; // Text für Levelanzeige
    public TextMeshProUGUI expText; // Text für EXP-Anzeige

    private void Start()
    {
        LoadProgress(); // Laden des Fortschritts
        UpdateUI(); // Update UI beim Start
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // CharacterLeveling über Szenen hinweg erhalten
        }
        else
        {
            Destroy(gameObject); // Verhindere mehrere Instanzen
        }
    }

    public void GainEXP(int amount)
    {
        currentEXP += amount;

        while (currentEXP >= expToNextLevel)
        {
            LevelUp();
        }

        SaveProgress(); // Speichern des Fortschritts
        UpdateUI(); // Update die UI nach dem Gewinnen von EXP
    }

    private void LevelUp()
    {
        currentLevel++;
        currentEXP -= expToNextLevel;
        expToNextLevel = CalculateExpToNextLevel(currentLevel);
        Debug.Log($"Leveled Up! New Level: {currentLevel}, EXP to next level: {expToNextLevel}");
        UpdateUI(); // Update die UI nach Levelaufstieg
    }

    private int CalculateExpToNextLevel(int level)
    {
        return level * 100; // Beispielhafte EXP-Berechnung
    }

    // Speichern des Fortschritts
    private void SaveProgress()
    {
        PlayerPrefs.SetInt("PlayerLevel", currentLevel);
        PlayerPrefs.SetInt("PlayerEXP", currentEXP);
        PlayerPrefs.SetInt("EXPToNextLevel", expToNextLevel);
        PlayerPrefs.Save();
    }

    // Laden des Fortschritts
    public void LoadProgress()
    {
        if (!valuesFromInspector)
        {
            currentLevel = PlayerPrefs.GetInt("PlayerLevel", currentLevel); // Behalte den aktuellen Wert, wenn kein gespeicherter Wert vorhanden ist
            currentEXP = PlayerPrefs.GetInt("PlayerEXP", currentEXP);
            expToNextLevel = PlayerPrefs.GetInt("EXPToNextLevel", expToNextLevel); // Behalte den aktuellen Wert, wenn kein gespeicherter Wert vorhanden ist
        }
        UpdateUI(); // UI aktualisieren
    }

    // Methode zur Aktualisierung der UI
    private void UpdateUI()
    {
        if (levelText != null)
            levelText.text = "Lvl: " + currentLevel;

        if (expText != null)
            expText.text = "Exp: " + currentEXP + "/" + expToNextLevel; // EXP und Anforderungen
    }

    // Neue Methode zum Zurücksetzen der Werte
    public void ResetValues()
    {
        currentLevel = 1;
        currentEXP = 0;
        expToNextLevel = 100;
        valuesFromInspector = true; // Setze auf true, um Werte aus dem Inspektor zu verwenden
        UpdateUI();
    }
}
