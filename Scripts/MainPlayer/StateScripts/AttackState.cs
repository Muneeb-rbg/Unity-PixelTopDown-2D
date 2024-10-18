using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    private float attackDuration = 0.5f; // Dauer der Angriffsanimation
    private float attackTimer;

    private PlayerAudio playerAudio; // Referenz zum PlayerAudio Script

    public AttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        
        // Hole die PlayerAudio Referenz
        playerAudio = player.GetComponent<PlayerAudio>();
        playerAudio.PlayAttackSound(); // Abspielen des Angriffssounds
        
        player.anim.SetBool("isAttacking", true);
        attackTimer = attackDuration; // Timer zurücksetzen
        AttackEnemy(); // Füge hier den Aufruf der Angriffs-Methode hinzu
    }


    public override void Update()
    {
        attackTimer -= Time.deltaTime; // Timer herunterzählen

        // Überprüfen, ob der Timer abgelaufen ist
        if (attackTimer <= 0)
        {
            StateMachine.ChangeState(player.idleState); // Wechsel zurück zu Idle
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("isAttacking", false);
    }

    private void AttackEnemy()
    {
        Debug.Log("Player is attacking the enemy!");

        // Überprüfen, ob der Feind in Reichweite ist und im Trefferbereich ist
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.transform.position, player.attackRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy")) // Stelle sicher, dass dein Feind-Tag "Enemy" ist
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(10); // Beispiel: 10 Schaden
                }
            }
        }
    }
}
