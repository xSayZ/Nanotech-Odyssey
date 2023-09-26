using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;

    public PatrolState (StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Patrol();
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    private void Look()
    {
        var hits = new List<RaycastHit2D>();
        if (Physics2D.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.right, enemy.contactFilter, hits, enemy.sightRange) > 0)
        {
            enemy.chaseTarget = hits[0].transform;
            ToChaseState();
        }
    }
    void Patrol()
    {
        enemy.spriteRendererFlag.material.color = Color.green;

        // Get the next position based on the waypoint
        Vector3 nextPosition = enemy.wayPoints[nextWayPoint].position;

        // Move the enemy towards the next position
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, nextPosition, enemy.moveSpeed * Time.deltaTime);

        Rotate(nextPosition);

        if (nextWayPoint <= enemy.wayPoints.Length - 1)
        {
            // Check if the enemy is close enough to the current waypoint
            float distanceToWaypoint = Vector3.Distance(enemy.transform.position, nextPosition);

            if (distanceToWaypoint < 0.1f) // Adjust this threshold as needed
            {
                // The enemy has reached the current waypoint, so move to the next one
                nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
            }
        }

    }

    void Rotate(Vector3 nextPosition)
    {
        // Calculate the direction vector from the enemy's position to the next position
        Vector2 direction = (nextPosition - enemy.transform.position).normalized;

        // Calculate the angle in degrees based on the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Limit the rotation angle to either 0 degrees or 180 degrees
        angle = Mathf.Round(angle / 180.0f) * 180.0f;

        // Rotate the enemy to face the limited angle, only on the Z-axis (2D)
        enemy.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}
