using UnityEngine;
using TMPro;
using UnityEngine.UI; // Für Slider
using UnityEngine.SceneManagement; // Für das Wechseln von Szenen
using System.Collections;

public class Player : MonoBehaviour
{
    #region Player Stats
    [Header("Player Stats")]
    public float moveSpeed = 12f;
    public float attackRange = 1.5f; // Attack range
    public int health = 100; // Player's health
    #endregion

    #region Health Management
    public Slider healthSlider; // Reference to health slider
    public TextMeshProUGUI healthText; // Reference to TextMeshPro for health display
    private HealthManager healthManager; // Handles health-related logic
    #endregion

    // Für deathScene und isdead flag um sicherzugehen dass transition gut aussieht
    private bool isDead = false;


    #region Components
    public Animator anim { get; private set; } // Player Animator
    public Rigidbody2D rb { get; private set; } // Rigidbody2D for physics
    public PlayerAudio playerAudio; // Referenz zum PlayerAudio Skript
    private CharacterLeveling CharacterLeveling; // Referenz zum CharacterLeveling Skript
    #endregion

    #region Player State Machine
    public PlayerStateMachine stateMachine { get; private set; } // State machine handling player states

    public IdleState idleState { get; private set; }
    public WalkState walkState { get; private set; }
    public RunState runState { get; private set; }
    public AttackState attackState { get; private set; }
    public PlayerDeadState playerDeadState { get; private set; } // DeadState Referenz
    #endregion

    protected float xInput;
    protected float yInput;

    private void Awake()
    {
        // Komponenten initialisieren
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new PlayerStateMachine();
        
        // Holen der PlayerAudio-Referenz
        playerAudio = GetComponent<PlayerAudio>();

        // Initialisiere die Zustände
        idleState = new IdleState(this, stateMachine, "isIdle");
        walkState = new WalkState(this, stateMachine, "isWalking");
        runState = new RunState(this, stateMachine, "isRunning");
        attackState = new AttackState(this, stateMachine, "isAttacking");
        playerDeadState = new PlayerDeadState(this, stateMachine, "isDead");

        // Setze den Anfangszustand der State Machine
        stateMachine.Initialize(idleState); // idleState als Startzustand
    }


private void Start()
{
    // HealthManager initialisieren
    healthManager = gameObject.AddComponent<HealthManager>();
    healthManager.healthSlider = healthSlider;
    healthManager.healthText = healthText;
    healthManager.Initialize(health, this);

    // Verknüpfe den globalen CharacterLeveling
    CharacterLeveling = CharacterLeveling.instance;

    if (CharacterLeveling == null)
    {
        Debug.LogError("CharacterLeveling instance not found!");
        return;
    }

    Debug.Log($"Initial Player Health: {health}, Level: {CharacterLeveling.currentLevel}, EXP: {CharacterLeveling.currentEXP}");
}


    private void Update()
    {
        // Eingaben abfragen und direkt den geschützten Variablen zuweisen
        xInput = Input.GetAxisRaw("Horizontal"); // A/D oder Pfeiltasten
        yInput = Input.GetAxisRaw("Vertical"); // W/S oder Pfeiltasten

        // Setze die Eingaben in den aktuellen Zustand
        stateMachine.currentState.SetInput(xInput, yInput);

        // Update der aktuellen StateMachine
        stateMachine.Update();

        // Testweise Heilung bei Drücken der H-Taste
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(20); // Heilt den Spieler um 20 Lebenspunkte
        }
    }

    // Setze die Geschwindigkeit des Rigidbody2D
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity); 
    }

    // Methode für das Erleiden von Schaden & Kontrolle wenn leben 0 ist -> spiel OnPlayerDeath
    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);

        // Überprüfe, ob die HP auf 0 gefallen sind
        if (healthManager.currentHealth <= 0 && !isDead)
        {
            OnPlayerDeath();
        }
    }

    // Wenn Spieler Stirbt
    public void OnPlayerDeath()
    {
        if (isDead) return;

        isDead = true;
        anim.SetBool("isDead", true);
        playerAudio.PlayDeathSound(); // Abspielen des Todessounds
        stateMachine.ChangeState(playerDeadState);
        StartCoroutine(FadeInDeathScene());
    }


    // Methode für das Heilen
    public void Heal(int amount)
    {
        healthManager.Heal(amount);
    }



// Death-Menu anzeigen mit Transition-Effekt
private IEnumerator FadeInDeathScene()
{
    // Warte, bis die Tod-Anime fertig ist, bevor das Menü angezeigt wird
    yield return new WaitForSeconds(1f); // Verzögerung anpassen

    // Wechsle zur DeathScene-Szene
    SceneManager.LoadScene("DeathScene"); // Achte darauf, dass der Szenenname korrekt ist
}

// CharacterLeveling Level sytem    
    public void DefeatEnemy(int expReward)
    {
        CharacterLeveling.GainEXP(expReward);
    }

}
