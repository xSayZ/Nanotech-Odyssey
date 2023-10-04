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
        enemy.indicator.sprite = enemy.indicators[2];
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
        Vector2 enemyToPlayer = (enemy.chaseTarget.position - enemy.eyes.transform.position).normalized;

        var hit = Physics2D.Raycast(enemy.eyes.transform.position, enemyToPlayer, enemy.sightRange);

        if (hit.collider != null && hit.transform.gameObject.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform; // Set the chase target.
            Debug.DrawRay(enemy.eyes.transform.position, enemyToPlayer * hit.distance, Color.red); // Optional: Visualize the ray.
            Debug.Log("Player hit");
        }
        else if (hit.collider == null || !hit.transform.gameObject.CompareTag("Player")) 
        {
            ToAlertState();
        }
    }

    private void Chase()
    {
        Vector3 offset = new Vector3(2, -0.2f, 0);
        Vector3 chasePos = Vector3.MoveTowards(enemy.transform.position, enemy.chaseTarget.position + offset, enemy.moveSpeed * Time.deltaTime);
        enemy.transform.position = chasePos;
        enemy.animator.Play("Swarmbot_Run");

        foreach (var weapon in enemy.weapons)
        {
            if (Time.time >= weapon.fireCooldown && weapon.currentAmmo > 0)
            {
                weapon.Fire(enemy.eyes.transform);
                enemy.animator.Play("Swarmbot_Fire");

            }
        }
    }
}
