using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackCooldown = 1f;
    private float lastAttackTime;
    private Vector2 movementInput;
    private Player player; // Spielerreferenz hinzufügen

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) 
    {
    }

    public override void Enter()
    {
        base.Enter();
        lastAttackTime = Time.time;

        // Setze die "Attack"-Animation
        enemy.anim.SetBool("isAttacking", true);

        // Dynamisch die Spielerreferenz abrufen
        player = GameObject.FindObjectOfType<Player>();
        UpdateMovementInput();
    }

public override void LogicUpdate()
{
    base.LogicUpdate();

    // Überprüfen, ob der Spieler noch existiert
    if (player == null)
    {
        // Spieler existiert nicht mehr, wechsle in den Patrol-Zustand
        stateMachine.ChangeState(enemy.patrolState); // Hier wird in den PatrolState gewechselt
        return;
    }

    float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

    if (distanceToPlayer > enemy.attackRange)
    {
        // Wenn der Spieler außer Angriffsreichweite ist, wechsle zurück zum Verfolgen
        enemy.anim.SetBool("isAttacking", false); // Animation zurücksetzen
        stateMachine.ChangeState(enemy.chaseState);
    }
    else
    {
        // Wenn der Angriffscooldown vorbei ist, greife den Spieler an
        if (Time.time - lastAttackTime > attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }
}


    private void UpdateMovementInput()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - enemy.transform.position).normalized;
            movementInput = new Vector2(direction.x, direction.y);
            enemy.anim.SetFloat("xInput", movementInput.x);
            enemy.anim.SetFloat("yInput", movementInput.y);
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("Enemy is attacking the player!");

        // Überprüfen, ob der Spieler in Reichweite ist und im Trefferbereich ist
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.attackRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player")) // Stelle sicher, dass dein Spieler-Tag "Player" ist
            {
                Player player = hit.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(10); // Beispiel: 10 Schaden
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("isAttacking", false); // Animation zurücksetzen, wenn der Zustand verlässt
    }
}
