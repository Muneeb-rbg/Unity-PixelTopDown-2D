using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewState : PlayerState
{

    public NewState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();
        HandleMovement();


    }

    public override void Exit()
    {
        base.Exit();

    }
}
