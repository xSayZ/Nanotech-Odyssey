using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectileState : IBossState
{
    private readonly StatePatternBoss boss;

    public AttackProjectileState(StatePatternBoss statePatternBoss)
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
        boss.weapons[0].Reload();
    }

    private void Shoot()
    {
        if (Time.time >= boss.weapons[0].fireCooldown && boss.weapons[0].currentAmmo > 0)
        {
            boss.weapons[0].Fire(boss.weapons[0].transform);
        }

        if (boss.weapons[0].currentAmmo <= 0)
        {
            ToIdleState();
        }
    }

    public void Move()
    {
        
    }
}
