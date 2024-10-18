/*
/--------------------------------------------------\
| Die MusicLibrary verwaltet eine Sammlung von      |
| Musikstücken, die im Spiel verwendet werden.      |
|                                                   |
| Funktionen der MusicLibrary:                      |
|                                                   |
| 1. Definiert einen `MusicTrack`-Struct, der den   |
|    Namen und den AudioClip eines Musikstücks      |
|    speichert.                                     |
| 2. Bietet ein Array von Musikstücken, die im      |
|    Inspector angezeigt und bearbeitet werden      |
|    können.                                        |
| 3. Ermöglicht das Abrufen eines AudioClips durch   |
|    den Namen des Musikstücks über die Methode     |
|    `GetClipFromName`. Gibt `null` zurück, wenn    |
|    der Name nicht gefunden wird.                  |
\--------------------------------------------------/
*/

using UnityEngine;

[System.Serializable] // Um sicherzustellen, dass der Struct im Inspector angezeigt wird
public struct MusicTrack
{
    public string trackName; // Name des Musikstücks
    public AudioClip clip; // AudioClip des Musikstücks
}

public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] tracks; // Array von Musikstücken

    // Methode zum Abrufen des AudioClips anhand des Namens
    public AudioClip GetClipFromName(string name)
    {
        foreach (var track in tracks)
        {
            if (track.trackName == name) // Namen abgleichen
            {
                return track.clip; // Clip zurückgeben
            }
        }
        return null; // Rückgabe null, wenn nichts gefunden wurde
    }
}
