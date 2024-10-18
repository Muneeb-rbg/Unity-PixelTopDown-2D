using UnityEngine;
using TMPro;
using UnityEngine.UI; 
using System.Collections;

public class Enemy : MonoBehaviour
{
    // --- Enemy Stats ---
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float speed = 2f; 
    public int health = 100;
    public int expReward = 50; 

    // --- Health Management ---
    public Slider healthSlider; 
    public TextMeshProUGUI healthText;
    private HealthManager healthManager;

    // --- Player Reference ---
    public Player player; // Setze auf protected, damit States darauf zugreifen können

    // --- Components ---
    public Animator anim { get; protected set; } // Setze auf protected, damit States darauf zugreifen können
    public Rigidbody2D rb { get; protected set; }

    // --- State Machine and States ---
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyPatrolState patrolState { get; private set; }
    public EnemyChaseState chaseState { get; private set; }
    public EnemyAttackState attackState { get; private set; }
    public EnemyDeadState deadState { get; private set; }

    private void Awake()
    {
        // Initialize Components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

        // Initialize States
        stateMachine = new EnemyStateMachine();
        patrolState = new EnemyPatrolState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        deadState = new EnemyDeadState(this, stateMachine); 
    }

    private void Start()
    {
        // Start in Patrol State
        stateMachine.Initialize(patrolState);

        // Initialize Health Manager
        healthManager = gameObject.AddComponent<HealthManager>();
        healthManager.healthSlider = healthSlider; 
        healthManager.healthText = healthText; 
        healthManager.Initialize(health, this); 
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);

        if (healthManager.currentHealth <= 0)
        {
            OnEnemyDeath(); 
        }
    }

    public void Heal(int amount)
    {
        healthManager.Heal(amount);
    }

    // --- Enemy Death Logic ---
private void OnEnemyDeath()
{
    anim.SetBool("isDead", true); 
    stateMachine.ChangeState(deadState);
    StartCoroutine(HandleDeath()); // Start the coroutine to handle death
}

private IEnumerator HandleDeath()
{
    // Assuming you have a way to get the duration of the death animation,
    // you can either hardcode it or fetch it dynamically.
    // For example:
    float deathAnimationDuration = 1f; // Adjust this to match your animation duration

    // Wait for the death animation to finish
    yield return new WaitForSeconds(deathAnimationDuration);

    Die(); // Call method to handle EXP and object destruction
}

private void Die()
{
    if (player != null)
    {
        player.DefeatEnemy(expReward); 
    }

    Destroy(gameObject); 
}


    // --- Player Death Logic ---
    public void OnPlayerDeath()
    {
        stateMachine.ChangeState(patrolState);
        player = null; 
    }
}