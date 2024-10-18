using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{

    private PlayerAudio playerAudio; // Referenz zum PlayerAudio Script

    public RunState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        // Hole die PlayerAudio Referenz
        playerAudio = player.GetComponent<PlayerAudio>();
        playerAudio.PlayRunSound(); // Abspielen des Angriffssounds

        player.anim.SetBool("isRunning", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("isRunning", false); // Running-Animation beenden
    }

    public override void Update()
    {
        base.Update();
        HandleMovement();

        // Wechsel zurück zu WalkState oder IdleState basierend auf Eingaben
        if (Mathf.Abs(xInput) > 0.1f || Mathf.Abs(yInput) > 0.1f)
        {
            if (!Input.GetKey(KeyCode.LeftShift)) // Wenn die Shift-Taste nicht gedrückt ist
            {
                StateMachine.ChangeState(player.walkState); // Wechsel in den Walk-State
            }
        }
        else // Wenn der Spieler keine Eingaben hat
        {
            StateMachine.ChangeState(player.idleState); // Wechsel in den Idle-State
        }
    }

    // Überschreibe die Berechnung der Geschwindigkeit nur für den Laufzustand, Damit er Schneller Rennt
    protected override Vector2 CalculateMovementVelocity(Vector2 inputDirection)
    {
        float runSpeed = player.moveSpeed * 1.5f; // 1.5-fache Geschwindigkeit
        float xVelocity = inputDirection.x * runSpeed; 
        float yVelocity = inputDirection.y * runSpeed;

        return new Vector2(xVelocity, yVelocity); // Gebe die Geschwindigkeit zurück
    }
}
