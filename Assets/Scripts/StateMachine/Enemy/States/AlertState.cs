using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    public void OnEnter()
    {
        enemy.indicator.sprite = enemy.indicators[0];
    }

    public void OnExit()
    {

    }

    private void ToPatrolState()
    {
        OnExit();
        enemy.currentState = enemy.patrolState;
        enemy.currentState.OnEnter();
        enemy.searchTimer = 0;
    }

    private void ToChaseState()
    {
        OnExit();
        enemy.currentState = enemy.chaseState;
        enemy.currentState.OnEnter();
        enemy.searchTimer = 0;
    }

    private void Look()
    {

        var hit = Physics2D.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.right, enemy.sightRange);
        Debug.DrawRay(enemy.eyes.transform.position, hit.point * enemy.sightRange, Color.yellow); // Optional: Visualize the ray.

        if (hit.collider != null && hit.transform.gameObject.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
        else
        {
            Search();
        }
    }

    private void Search()
    {
        enemy.indicator.sprite = enemy.indicators[1];

        if (enemy.facingRight && (enemy.searchTimer <= enemy.searchingDuration / 2))
        {
            enemy.targetRotation = Quaternion.Euler(0, 180, 0); // Rotate left (180 degrees)
        }
        else if (!enemy.facingRight && (enemy.searchTimer >= enemy.searchingDuration / 2 ))
        {
            enemy.targetRotation = Quaternion.Euler(0, 0, 0); // Rotate right (0 degrees)
        }

        enemy.transform.rotation = enemy.targetRotation;

        // Switch rotation direction
        enemy.facingRight = !enemy.facingRight;

        enemy.searchTimer += Time.deltaTime;

        if (enemy.searchTimer >= enemy.searchingDuration)
        {
     
            ToPatrolState();
        }
    }
}
