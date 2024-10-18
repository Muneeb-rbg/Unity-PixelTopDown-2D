using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    private Vector2 lastDirection; // Speichert die letzte Richtung

    public IdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.anim.SetBool("isIdle", true);
        
        // Stelle sicher, dass wir die xInput und yInput von lastDirection nutzen
        player.anim.SetFloat("xInput", lastDirection.x);
        player.anim.SetFloat("yInput", lastDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("isIdle", false);
    }

    public override void Update()
    {
        base.Update();
        
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        // Wenn der Spieler sich bewegt, speichere die Richtung
        if (Mathf.Abs(xInput) > 0.1f || Mathf.Abs(yInput) > 0.1f)
        {
            lastDirection = new Vector2(xInput, yInput); // Update der letzten Richtung
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StateMachine.ChangeState(player.runState);
            }
            else
            {
                StateMachine.ChangeState(player.walkState);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            StateMachine.ChangeState(player.attackState);
        }

        // Setze die xInput- und yInput-Parameter für die Animation
        player.anim.SetFloat("xInput", lastDirection.x);
        player.anim.SetFloat("yInput", lastDirection.y);
    }
}
