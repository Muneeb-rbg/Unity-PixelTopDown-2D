using System.Collections;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private PlayerAudio playerAudio; // Referenz zum PlayerAudio Script
    private float deathAnimationDuration = 2f; // Dauer der Tod-Animation

    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animationParameter) 
        : base(player, stateMachine, animationParameter) { }

    public override void Enter()
    {
        base.Enter();


        playerAudio = player.GetComponent<PlayerAudio>(); // Hole die PlayerAudio Referenz
        playerAudio.PlayDeathSound(); // Abspielen des Todessounds

        // Setze die "isDead"-Animation
        player.anim.SetBool("isDead", true);


        // Starte Coroutine für die Todesanimation
        player.StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        // Warte, bis die Todesanimation abgeschlossen ist
        yield return new WaitForSeconds(deathAnimationDuration);

        // Zerstöre das Spieler-Objekt oder führe eine andere Tod-Logik aus
        Object.Destroy(player.gameObject);
    }
}
