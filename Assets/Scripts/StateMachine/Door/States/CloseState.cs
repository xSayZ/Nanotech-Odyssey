using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class CloseState : IDoorState
    {
        private readonly DoorStatePattern door;
        public CloseState(DoorStatePattern doorStatePattern)
        {
            door = doorStatePattern;
        }

        public void OnEnter()
        {
            door.animator.Play("Door_Small_Close");
        }

        public void OnExit()
        {
        }

        public void OnTriggerEnter(Collider2D other)
        {
        }

        public void OnTriggerExit(Collider2D other)
        {
        }

    }
}
