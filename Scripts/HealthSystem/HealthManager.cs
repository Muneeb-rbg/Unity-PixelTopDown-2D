using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    #region Variables

    [Header("Health Settings")]
    public int currentHealth;
    private int baseMaxHealth = 100;

    [Header("UI References")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    public event Action<int> OnHealthChanged;

    // Reference to the enemy (optional, can be removed if not needed for Player)
    private Enemy enemyReference;
    private Player playerReference;

    #endregion

    #region Initialization

    // public void Initialize(int initialHealth, Enemy enemy = null)
    // {
    //     currentHealth = initialHealth;
    //     enemyReference = enemy; // Store the reference to the enemy if provided
    //     UpdateHealthDisplay();
    // }
    
    // public void Initialize(int initialHealth, Player player = null)
    // {
    //     currentHealth = initialHealth;
    //     playerReference = player; // Speichere die Referenz auf den Player, falls vorhanden
    //     UpdateHealthDisplay();
    // }

    public void Initialize(int initialHealth, Component character)
    {
        currentHealth = initialHealth;
        UpdateHealthDisplay();

        // Optional: Hier kannst du den Typ des Charakters überprüfen, wenn nötig
        if (character is Enemy)
        {
            // Speichere die Referenz auf den Gegner, wenn nötig
        }
        else if (character is Player)
        {
            // Speichere die Referenz auf den Spieler, wenn nötig
        }
    }
    
    #endregion

    #region Health Management

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        OnHealthChanged?.Invoke(currentHealth);
        UpdateHealthDisplay();

        // Überprüfen, ob der Spieler tot ist
        if (currentHealth <= 0 && playerReference != null)
        {
            playerReference.OnPlayerDeath(); // Rufe die OnPlayerDeath-Methode des Players auf
        }
    }

    
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > CalculateMaxHealth())
            currentHealth = CalculateMaxHealth();

        OnHealthChanged?.Invoke(currentHealth);
        UpdateHealthDisplay();
    }

    private int CalculateMaxHealth()
    {
        return baseMaxHealth; // Adjust for buffs/leveling
    }

    #endregion

    #region UI Updates

    private void UpdateHealthDisplay()
    {
        UpdateHealthSlider();
        UpdateHealthText();
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / CalculateMaxHealth();
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    #endregion
}