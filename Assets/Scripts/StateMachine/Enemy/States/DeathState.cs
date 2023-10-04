using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class DeathState : IEnemyState
    {
        private readonly StatePatternEnemy enemy;
        public DeathState(StatePatternEnemy statePatternEnemy)
        {
            enemy = statePatternEnemy;
        }

        void IEnemyState.OnEnter()
        {
            enemy.animator.Play(enemy.DeathAnimation);
            enemy.Death();
        }

        void IEnemyState.OnExit() { }

        void IEnemyState.OnTriggerEnter(Collider2D other) { }

        void IEnemyState.UpdateState() { }
    }
}
