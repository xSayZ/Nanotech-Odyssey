using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMissileState : IBossState
{
    private readonly StatePatternBoss boss;

    public AttackMissileState(StatePatternBoss statePatternBoss)
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
        boss.weapons[2].Reload();
    }

    private void Shoot()
    {
        if (Time.time >= boss.weapons[2].fireCooldown && boss.weapons[2].currentAmmo > 0)
        {
            boss.weapons[2].Fire(boss.weapons[2].transform);
        }

        if (boss.weapons[2].currentAmmo <= 0)
        {
            ToIdleState();
        }
    }
}
