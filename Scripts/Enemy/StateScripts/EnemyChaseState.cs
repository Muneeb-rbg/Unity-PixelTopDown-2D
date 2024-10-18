using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    [SerializeField] private float chaseSpeed = 5f; // Speed for chasing the player
    [SerializeField] private float stoppingDistance = 0.1f; // Distance to stop before reaching the player


    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool("isChasing", true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.player == null)
        {
            Debug.LogError("Player not found!");
            stateMachine.ChangeState(enemy.patrolState); // Change state if player is not available
            return;
        }

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

        // Check if player is in attack range
        if (distanceToPlayer < enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.attackState); // Transition to attack state
        }
        // Check if player is out of detection range
        else if (distanceToPlayer > enemy.detectionRange)
        {
            stateMachine.ChangeState(enemy.patrolState); // Immediately switch to patrol state
        }
        // If player is within detection range, move towards the player
        else
        {
            MoveTowards(enemy.player.transform.position);
        }
    }

    private void MoveTowards(Vector3 target)
    {
        // Calculate direction and distance to move
        Vector2 direction = (target - enemy.transform.position).normalized;
        float distance = Vector3.Distance(enemy.transform.position, target);

        // Only move if we are not already at the stopping distance
        if (distance > stoppingDistance)
        {
            enemy.rb.MovePosition(Vector3.MoveTowards(enemy.transform.position, target, Time.deltaTime * chaseSpeed));
        }

        // Set animator parameters for direction
        enemy.anim.SetFloat("xInput", direction.x);
        enemy.anim.SetFloat("yInput", direction.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("isChasing", false);
    }
}
