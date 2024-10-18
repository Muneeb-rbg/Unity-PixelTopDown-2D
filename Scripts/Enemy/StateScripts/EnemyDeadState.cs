using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    private Vector2 movementInput;
    private float deathAnimationDuration = 2f; // Dauer der Tod-Animation
    private HealthManager healthManager; // Referenz zum HealthManager

    public EnemyDeadState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
        // Optional: Holen Sie sich die HealthManager-Referenz
        healthManager = enemy.GetComponent<HealthManager>();
    }

    public override void Enter()
    {
        base.Enter();
        
        // Setze die "isDead"-Animation
        enemy.anim.SetBool("isDead", true);

        // Berechne die Richtung zum Spieler und aktualisiere den Animator mit xInput und yInput
        Vector2 direction = (enemy.player.transform.position - enemy.transform.position).normalized;
        movementInput = new Vector2(direction.x, direction.y);
        enemy.anim.SetFloat("xInput", movementInput.x);
        enemy.anim.SetFloat("yInput", movementInput.y);

        // Starte Coroutine für die Todesanimation
        enemy.StartCoroutine(HandleDeath());
    }

    public override void Update()
    {
        base.Update();
        // Hier könnten Sie eventuell zusätzliche Logik hinzufügen, falls nötig
    }

    public override void Exit()
    {
        base.Exit();
    }

    private IEnumerator HandleDeath()
    {
        // Warte, bis die Todesanimation abgeschlossen ist
        yield return new WaitForSeconds(deathAnimationDuration);

        // Zerstöre das Gegner-Objekt
        Object.Destroy(enemy.gameObject);
    }
}
