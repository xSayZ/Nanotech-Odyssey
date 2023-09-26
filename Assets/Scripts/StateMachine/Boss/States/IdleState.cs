using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IBossState
{
    private readonly StatePatternBoss boss;
    public float attackTimer = 0.0f;

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
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
            // Reset the timer
            attackTimer = boss.attackDelay;

            int randomAttack = Random.Range(0, boss.weapons.Length);
            Debug.Log("Random attack: " + randomAttack);

            if (randomAttack == 0)
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

    public void Move()
    {
        if(boss.attackTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                boss.attackTarget = player.transform;
            }
            else
            {
                return;
            }
        }

        float newYPosition = Mathf.MoveTowards(boss.transform.position.y, boss.attackTarget.position.y, boss.yFollowSpeed * Time.deltaTime);
        Vector3 newPosition = new Vector3(boss.transform.position.x, newYPosition);
        boss.transform.position = newPosition;

        boss.xChangeTimer -= Time.deltaTime;

        if(boss.xChangeTimer <= 0)
        {
            ChangeRandomXPosition();
        }
    }

    private void ChangeRandomXPosition()
    {
        // Choose a random X-axis position between minX and maxX
        float randomX = Random.Range(boss.minX, boss.maxX);

        // Set the new X-axis position
        float newXPosition = Mathf.MoveTowards(boss.transform.position.x, randomX, boss.yFollowSpeed * Time.deltaTime);
        Vector3 newPosition = new Vector3(newXPosition, boss.transform.position.y);
        boss.transform.position = newPosition;

        // Reset the X-axis change timer
        boss.xChangeTimer = boss.xChangeInterval;
    }
}
