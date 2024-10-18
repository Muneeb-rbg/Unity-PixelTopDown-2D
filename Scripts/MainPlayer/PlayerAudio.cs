using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    #region Audio
    [SerializeField]
    private AudioClip attackSound; // Der Soundclip, der beim Angriff abgespielt werden soll
    [SerializeField]
    private AudioClip walkSound; // Der Soundclip, der beim Laufen abgespielt werden soll
    [SerializeField]
    private AudioClip runSound; // Der Soundclip, der beim Rennen abgespielt werden soll
    [SerializeField]
    private AudioClip deathSound; // Der Soundclip, der beim Sterben abgespielt werden soll
    
    private AudioSource audioSource; // AudioSource für das Abspielen des Sounds
    #endregion

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource hinzufügen
    }

    public void PlayAttackSound()
    {
        if (attackSound != null) // Überprüfen, ob der Clip zugewiesen ist
        {
            audioSource.PlayOneShot(attackSound); // Abspielen des Angriffssounds
        }
    }

    public void PlayWalkSound()
    {
        if (walkSound != null) // Überprüfen, ob der Clip zugewiesen ist
        {
            audioSource.PlayOneShot(walkSound); // Abspielen des Lauf-Sounds
        }
    }

    public void PlayRunSound()
    {
        if (runSound != null) // Überprüfen, ob der Clip zugewiesen ist
        {
            audioSource.PlayOneShot(runSound); // Abspielen des Lauf-Sounds
        }
    }

    public void PlayDeathSound()
    {
        if (deathSound != null) // Überprüfen, ob der Clip zugewiesen ist
        {
            audioSource.PlayOneShot(deathSound); // Abspielen des Todessounds
        }
    }
}
