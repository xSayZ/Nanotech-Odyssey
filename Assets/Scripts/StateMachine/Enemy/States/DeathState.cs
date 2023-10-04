using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    public DeathState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    void IEnemyState.OnEnter()
    {
        enemy.animator.Play("Swarmbot_Death");
        enemy.Death();
    }

    void IEnemyState.OnExit() { }

    void IEnemyState.OnTriggerEnter(Collider2D other) { }

    void IEnemyState.UpdateState() { }
}
