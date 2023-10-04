using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public interface IDoorState
    {

        void OnTriggerEnter(Collider2D other);
        void OnTriggerExit(Collider2D other);

        void OnEnter();

        void OnExit();

    }
}
