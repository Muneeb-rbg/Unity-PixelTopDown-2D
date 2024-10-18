using System.Collections;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private Vector3 patrolPoint;
    private const float patrolRange = 5f; // Range for patrol points
    private const float closeEnoughDistance = 0.5f; // Distance to consider the patrol point reached
    private bool isSpotted; // Variable to track if the player is spotted
    [SerializeField][Range(0, 100)] private float seenRange = 10f; // Range for spotting the player
    private Player player; // Spielerreferenz hinzufügen

    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.FindObjectOfType<Player>(); // Dynamisch die Spielerreferenz abrufen
        SetRandomPatrolPoint();
        enemy.anim.SetBool("isPatrolling", true); // Set patrol animation
    }

    public override void LogicUpdate()
{
    base.LogicUpdate();

    // Überprüfen, ob der Zielspieler noch existiert
    if (player == null)
    {
        stateMachine.ChangeState(enemy.patrolState); // Hier wird in den PatrolState gewechselt
        return;
    }

    // Check if the player is within detection range
    if (Vector3.Distance(enemy.transform.position, player.transform.position) < enemy.detectionRange)
    {
        stateMachine.ChangeState(enemy.chaseState); // Transition to chase state
    }
    else
    {
        // Check if the player is spotted using Raycasting
        CheckForTargets();

        if (isSpotted)
        {
            stateMachine.ChangeState(enemy.chaseState); // Transition to chase state if spotted
        }
        else
        {
            // Hier kannst du die Bewegung in der Logik aktualisieren
            MoveTowards(patrolPoint); // Move towards the patrol point

            // Check if the enemy reached the patrol point
            if (Vector3.Distance(enemy.transform.position, patrolPoint) < closeEnoughDistance)
            {
                SetRandomPatrolPoint(); // Set a new patrol point
            }
        }
    }
}



    private void MoveTowards(Vector3 target)
    {
        // Calculate direction towards the target
        Vector2 direction = (target - enemy.transform.position).normalized;
        enemy.rb.MovePosition(Vector3.MoveTowards(enemy.transform.position, target, Time.deltaTime * enemy.speed)); // Use enemy's speed

        // Update animator parameters for direction
        enemy.anim.SetFloat("xInput", direction.x);
        enemy.anim.SetFloat("yInput", direction.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("isPatrolling", false); // Stop patrol animation
    }

    private void SetRandomPatrolPoint()
    {
        // Generate a random patrol point within a certain range
        patrolPoint = new Vector3(
            enemy.transform.position.x + Random.Range(-patrolRange, patrolRange),
            enemy.transform.position.y,
            enemy.transform.position.z + Random.Range(-patrolRange, patrolRange)
        );
    }

    private void CheckForTargets()
    {
        // Use Raycasting to check for visibility of the player
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, (player.transform.position - enemy.transform.position).normalized, seenRange);
        if (hit.collider != null && hit.collider.CompareTag(player.tag))
        {
            isSpotted = true; // Player is spotted
            enemy.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0); // Change color to indicate spotted
        }
        else
        {
            isSpotted = false; // Player is not spotted
            enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1); // Reset color
        }
    }
}
