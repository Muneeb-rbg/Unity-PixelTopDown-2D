/*
/--------------------------------------------------\
| Die EnemyStateMachine verwaltet die Zustände der  |
| Gegner im Spiel.                                  |
|                                                   |
| Funktionen der EnemyStateMachine:                 |
|                                                   |
| 1. Initialisiert den aktuellen Zustand und führt   |
|    die `Enter`-Methode des Anfangszustands aus.   |
| 2. Ermöglicht den Wechsel zu einem neuen Zustand,  |
|    indem die `Exit`-Methode des aktuellen         |
|    Zustands aufgerufen wird, gefolgt von der      |
|    `Enter`-Methode des neuen Zustands.           |
| 3. Aktualisiert den aktuellen Zustand in jedem     |
|    Frame durch Aufruf der `Update`-Methode.       |
| 4. Bietet eine Methode, um zu überprüfen, ob der   |
|    aktuelle Zustand ein bestimmter Zustandstyp ist.|
\--------------------------------------------------/
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(EnemyState newState)
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

        currentState.Exit();
        currentState = newState;
        newState.Enter();
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

