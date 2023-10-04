using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class IdleState : IDoorState
    {
        private readonly DoorStatePattern door;
        public IdleState(DoorStatePattern doorStatePattern)
        {
            door = doorStatePattern;
        }

        public void OnTriggerEnter(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnEnter();
                ToOpenState();
            }
        }

        public void OnTriggerExit(Collider2D other)
        {

        }

        public void OnEnter()
        {
            door.animator.Play(door.IdleAnimation);
        }

        public void OnExit()
        {

        }

        private void ToOpenState()
        {
            OnExit();
            door.currentState = door.openState;
            door.currentState.OnEnter();
        }

    }

}