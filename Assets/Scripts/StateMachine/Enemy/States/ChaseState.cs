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

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void ToAlertState()
    {
        OnExit();
        enemy.currentState = enemy.alertState;
        enemy.currentState.OnEnter();
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

        Vector3 offset = new Vector3(2, 0, 0);
        Vector3 chasePos = Vector3.MoveTowards(enemy.transform.position, enemy.chaseTarget.position + offset, enemy.moveSpeed * Time.deltaTime);
        enemy.transform.position = chasePos;

        foreach (var weapon in enemy.weapons)
        {
            if (Time.time >= weapon.fireCooldown && weapon.currentAmmo > 0)
            {
                weapon.Fire(weapon.transform);
            }
        }
    }
}
