using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Chase();
    }

    public void OnTriggerEnter(Collider2D collision)
    {

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    private void Look()
    {
        var hits = new List<RaycastHit2D>();
        Vector2 enemyToTarget = (enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position;
        if (Physics2D.Raycast(enemy.eyes.transform.position, enemyToTarget, enemy.contactFilter, hits, enemy.sightRange) > 0)
        {
            enemy.chaseTarget = hits[0].transform;
        }
        else
        {
            ToAlertState();
        }
    }

    private void Chase()
    {
        enemy.spriteRendererFlag.material.color = Color.red;
        //enemy.transform.position = enemy.chaseTarget.position;
    }
}
