/*
/--------------------------------------------------\
| Die EnemyState-Klasse dient als abstrakte Basis-  |
| klasse für die verschiedenen Zustände der Gegner.  |
|                                                   |
| Funktionen der EnemyState:                        |
|                                                   |
| 1. Speichert Referenzen auf den zugehörigen Gegner |
|    und die Zustandsmaschine, um den aktuellen     |
|    Zustand zu verwalten.                          |
| 2. Stellt virtuelle Methoden zur Verfügung, die   |
|    in abgeleiteten Klassen überschrieben werden   |
|    können, um spezifisches Verhalten für jeden    |
|    Zustand zu definieren.                         |
| 3. Umfasst allgemeine Methoden wie `Enter`,      |
|    `LogicUpdate`, `PhysicsUpdate`, `Update` und   |
|    `Exit`, die bei Bedarf angepasst werden können.|
\--------------------------------------------------/
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    protected EnemyState(Enemy enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }

    public virtual void Update()
    {

    }

    public virtual void Exit() { }
}

