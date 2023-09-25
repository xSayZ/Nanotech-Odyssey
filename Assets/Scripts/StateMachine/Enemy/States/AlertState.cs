using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private float searchTimer;
    bool rotateLeft = true;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0;
    }

    private void ToAlertState()
    {
        Debug.LogWarning("Can't transition to same state");
    }

    private void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0;
    }

    private void Look()
    {
        var hits = new List<RaycastHit2D>();
        if (Physics2D.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, enemy.contactFilter, hits, enemy.sightRange) > 0)
        {
            enemy.chaseTarget = hits[0].transform;
            ToChaseState();
        }
    }

    private void Search()
    {
        enemy.spriteRendererFlag.material.color = Color.yellow;

        Quaternion targetRotation;

        if (rotateLeft)
        {
            targetRotation = Quaternion.Euler(0, 180, 0); // Rotate left (180 degrees)
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0); // Rotate right (0 degrees)
        }

        enemy.transform.rotation = targetRotation;

        // Switch rotation direction
        rotateLeft = !rotateLeft;

        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.searchingDuration)
        {
            ToPatrolState();
        }
    }
}
