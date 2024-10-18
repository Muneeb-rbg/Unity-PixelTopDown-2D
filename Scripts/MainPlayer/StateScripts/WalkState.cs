using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerState
{

    private PlayerAudio playerAudio; // Referenz zum PlayerAudio Script

    public WalkState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        // Hole die PlayerAudio Referenz
        playerAudio = player.GetComponent<PlayerAudio>();
        playerAudio.PlayWalkSound(); // Abspielen des Angriffssounds


        player.anim.SetBool("isWalking", true);
    }


    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("isWalking", false); // Walking-Animation beenden
    }

    public override void Update()
    {
        base.Update();

        HandleMovement();

        // Bewege den Spieler, solange Eingaben vorhanden sind
        if (Mathf.Abs(xInput) > 0.1f || Mathf.Abs(yInput) > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Prüfe, ob der Spieler sprintet
            {
                StateMachine.ChangeState(player.runState); // Wechsel in den Run-State
            }
        }
        else
        {
            // Wenn keine Bewegungseingabe vorhanden ist, gehe zurück zum Idle-State
            StateMachine.ChangeState(player.idleState);
        }
    }
}
