using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{

    #region Variables
    protected PlayerStateMachine StateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;

    private string animBoolName;
    protected bool triggerCalled;
    
    protected float stateTimer;
    #endregion

    #region Constructor
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.StateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    #endregion

    #region State Methods
    
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        // Aktualisiere die Eingaben
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // Setze die Animator-Parameter für die Animationen
        player.anim.SetFloat("xInput", xInput);
        player.anim.SetFloat("yInput", yInput);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    #endregion

    #region Input Methods
    public void SetInput(float x, float y)
    {
        xInput = x;
        yInput = y;
    }
    #endregion

    #region Movement Methods
    // Diese Methode verarbeitet die Eingaben und setzt die Geschwindigkeit des Spielers basierend auf der Bewegungsrichtung.
    public virtual void HandleMovement()
    {
        Vector2 inputDirection = GetInputDirection(); // Berechne die Eingabere Richtung
        Vector2 movementVelocity = CalculateMovementVelocity(inputDirection); // Berechne die Bewegungs Geschwindigkeit

        // Setze die Geschwindigkeit des Spielers
        player.SetVelocity(movementVelocity.x, movementVelocity.y);
    }

    // Diese Methode verarbeitet die Eingaben und gibt die Bewegungsrichtung zurück.
    private Vector2 GetInputDirection()
    {
        float xInput = Input.GetAxisRaw("Horizontal"); // A/D oder Pfeiltasten
        float yInput = Input.GetAxisRaw("Vertical"); // W/S oder Pfeiltasten

        return new Vector2(xInput, yInput).normalized; // Normalisiere die Richtung
    }

    // Diese Methode berechnet die Geschwindigkeit des Spielers basierend auf der Eingabere Richtung.
    protected virtual Vector2 CalculateMovementVelocity(Vector2 inputDirection)
    {
        // Berechne die Geschwindigkeit
        float xVelocity = inputDirection.x * player.moveSpeed;
        float yVelocity = inputDirection.y * player.moveSpeed;

        return new Vector2(xVelocity, yVelocity); // Gebe die Geschwindigkeit zurück
    }
    #endregion


}
