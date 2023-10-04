using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class IdleState : IEnemyState
    {
        private readonly StatePatternEnemy enemy;

        public IdleState(StatePatternEnemy statePatternEnemy)
        {
            enemy = statePatternEnemy;
        }

        public void OnEnter()
        {
            enemy.indicator.sprite = enemy.indicators[2];

            if (enemy.ToPatrol)
            {
                ToPatrolState();
            }

            if (enemy.enemyType == StatePatternEnemy.Enemy.Tanker)
            {
                if (enemy.hasBeenAlerted)
                {
                    enemy.animator.Play(enemy.IdleAnimation);
                }
                else
                {
                    enemy.animator.Play(enemy.InactiveIdleAnimation);
                }
            }
        }

        public void OnExit()
        {

        }

        public void OnTriggerEnter(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ToAlertState();
            }
        }

        public void UpdateState()
        {
            Look();
        }

        public void ToAlertState()
        {
            enemy.currentState = enemy.alertState;
        }

        public void ToChaseState()
        {
            enemy.currentState = enemy.chaseState;
        }

        public void ToPatrolState()
        {
            enemy.currentState = enemy.patrolState;
        }

        private void Look()
        {

            var hit = Physics2D.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.right, enemy.sightRange);
            Debug.DrawRay(enemy.eyes.transform.position, enemy.eyes.transform.right * enemy.sightRange, Color.yellow); // Optional: Visualize the ray.


            if (hit.collider != null && hit.transform.gameObject.CompareTag("Player"))
            {
                enemy.chaseTarget = hit.transform;
                ToChaseState();
                Debug.DrawRay(enemy.eyes.transform.position, enemy.eyes.transform.right * enemy.sightRange);
            }
        }
    }
}
