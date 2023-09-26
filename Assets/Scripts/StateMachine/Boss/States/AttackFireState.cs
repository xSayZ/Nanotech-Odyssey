using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFireState : IBossState
{
    private readonly StatePatternBoss boss;

    public AttackFireState(StatePatternBoss statePatternBoss)
    {
        boss = statePatternBoss;
    }

    public void UpdateState()
    {
        Shoot();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void ToIdleState()
    {
        boss.currentState = boss.idleState;
        boss.weapons[1].Reload();
    }

    private void Shoot()
    {
        ToIdleState();
    }

    public void Move()
    {

    }
}
