using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class DoorStatePattern : MonoBehaviour
    {
        public float openTime;
        public SpriteRenderer spriteRendererFlag;

        [SerializeField] public string IdleAnimation;
        [SerializeField] public string OpenAnimation;
        [SerializeField] public string CloseAnimation;

        [HideInInspector] public SpriteRenderer doorSprite;
        [HideInInspector] public Animator animator;
        [HideInInspector] public IDoorState currentState;
        [HideInInspector] public IdleState idleState;
        [HideInInspector] public OpenState openState;
        [HideInInspector] public CloseState closeState;

        private void Awake()
        {
            doorSprite = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponentInChildren<Animator>();
            idleState = new IdleState(this);
            openState = new OpenState(this);
        }

        void Start()
        {
            currentState = idleState;
        }

        public void EnterState()
        {
            currentState.OnEnter();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentState.OnTriggerEnter(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            currentState.OnTriggerExit(collision);
        }
    }
}
