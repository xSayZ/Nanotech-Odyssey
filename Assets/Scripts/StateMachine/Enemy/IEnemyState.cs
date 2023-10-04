using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public interface IEnemyState
    {
        void UpdateState();
        void OnTriggerEnter(Collider2D other);
        void OnEnter();
        void OnExit();

    }
}

