using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class OpenState : IDoorState
    {
        private readonly DoorStatePattern door;

        public OpenState(DoorStatePattern doorStatePattern)
        {
            door = doorStatePattern;
        }

        public void OnTriggerEnter(Collider2D other) { }

        public void OnTriggerExit(Collider2D other)
        {
            OnExit();
            door.StartCoroutine(TransitionState());
        }

        public void OnEnter()
        {
            // Play sound
            door.animator.Play(door.OpenAnimation);
        }

        public void OnExit() { door.animator.Play(door.CloseAnimation); }

        private void ToIdleState()
        {
            door.currentState = door.idleState;
        }

        private IEnumerator TransitionState()
        {
            // Coroutine logic for OpenState
            yield return new WaitForSeconds(1.2f); // Adjust the duration as needed

            ToIdleState();
            door.currentState.OnEnter();
        }
    }
}