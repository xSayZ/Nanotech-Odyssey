using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IBossState
{
    private readonly StatePatternBoss boss;

    public IdleState(StatePatternBoss statePatternBoss)
    {
        boss = statePatternBoss;
    }

    public void UpdateState()
    {
        ChooseAttack();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void ToIdleState()
    {
        Debug.LogWarning("Can't transition to same state");
    }

    private void ToAttackProjectileState()
    {
        boss.currentState = boss.attackProjectileState;
    }

    private void ToAttackFireState()
    {
        boss.currentState = boss.attackFireState;
    }

    private void ToAttackMissileState()
    {
        boss.currentState = boss.attackMissileState;
    }

    private void ChooseAttack()
    {
        int randomAttack = Random.Range(0, boss.weapons.Length);
        Debug.Log("Random attack: " + randomAttack);

        if(randomAttack == 0)
        {
            ToAttackProjectileState();
        }
        else if (randomAttack == 1)
        {
            ToAttackFireState();
        }
        else if (randomAttack == 2)
        {
            ToAttackMissileState();
        }
    }
}
