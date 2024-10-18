using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    // Der aktuelle Zustand des Spielers
    public PlayerState currentState { get; private set; }

    // Initialisiere die Zustandsmaschine mit dem Startzustand
    public void Initialize(PlayerState startingState)
    {
        ChangeState(startingState);
    }

    // Ändere den aktuellen Zustand zu einem neuen Zustand
    public void ChangeState(PlayerState newState)
    {
        // Stelle sicher, dass der neue Zustand nicht null ist
        if (newState == null)
        {
            return; // Rückgabe, wenn der neue Zustand null ist
        }

        // Verhindere den Übergang zum selben Zustand
        if (currentState == newState)
        {
            return; // Rückgabe, wenn der Zustand gleich bleibt
        }

        // Beende den aktuellen Zustand und betrete den neuen Zustand
        currentState?.Exit(); // Verwende null-bedingte Operatoren, um mögliche Nullreferenzen zu vermeiden
        currentState = newState;
        currentState.Enter();
    }

    // Aktualisiere den aktuellen Zustand
    public void Update()
    {
        currentState?.Update(); // Verwende null-bedingte Operatoren, um mögliche Nullreferenzen zu vermeiden
    }
    
    // Optional: Überprüfe, ob der aktuelle Zustand ein bestimmter Zustandstyp ist
    public bool IsCurrentState<T>() where T : PlayerState
    {
        return currentState is T;
    }
}
