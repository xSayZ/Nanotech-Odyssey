using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class StatePatternEnemy : MonoBehaviour
    {
        public enum Enemy
        {
            Swarmbot,
            Tanker
        }

        [Header("Variables")]
        public float searchingDuration = 4f;
        public float searchTimer;
        public float health = 3;
        public float deathTime = 1.2f;
        public int sightRange = 20;
        public float moveSpeed = 5f;
        [SerializeField] public bool ToPatrol;

        [HideInInspector] public bool facingRight = true;
        [HideInInspector] public bool hasBeenAlerted = false;


        [Space]
        [Header("References")]
        public Transform[] wayPoints;
        public BaseWeapon[] weapons;
        public Sprite[] indicators;
        public Transform eyes;
        public AudioSource soundToPlay;
        public Enemy enemyType;

        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public Animator animator;
        [HideInInspector] public Image indicator;
        [HideInInspector] public Quaternion targetRotation;
        [HideInInspector] public Transform chaseTarget;
        [HideInInspector] public IEnemyState currentState;
        [HideInInspector] public IdleState idleState;
        [HideInInspector] public ChaseState chaseState;
        [HideInInspector] public AlertState alertState;
        [HideInInspector] public PatrolState patrolState;
        [HideInInspector] public DeathState deathState;

        // Standard Animations
        [SerializeField] public string IdleAnimation;
        [SerializeField] public string FireAnimation;
        [SerializeField] public string RunAnimation;
        [SerializeField] public string DeathAnimation;

        // If Tanker
        [SerializeField] public string InactiveIdleAnimation;
        [SerializeField] public string ActivateAnimation;

        private void Awake()
        {
            rb = FindObjectOfType<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            indicator = GetComponentInChildren<Image>();
            soundToPlay = GetComponent<AudioSource>();
            idleState = new IdleState(this);
            chaseState = new ChaseState(this);
            alertState = new AlertState(this);
            patrolState = new PatrolState(this);
            deathState = new DeathState(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            if(enemyType == Enemy.Tanker)
            {
                ToPatrol = false;
            }

            currentState = idleState;
            EnterState();
        }

        public void EnterState()
        {
            currentState.OnEnter();
        }

        // Update is called once per frame
        void Update()
        {
            currentState.UpdateState();
            UpdateHealth();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentState.OnTriggerEnter(collision);

            if (collision.gameObject.CompareTag("Projectile"))
            {
                Projectile collidedProjectile = collision.gameObject.GetComponent<Projectile>();

                // Check if the projectile owner is not the enemy itself
                if (collidedProjectile.owner != gameObject)
                {
                    Debug.Log(collidedProjectile.owner.gameObject);
                    health--;
                    Destroy(collidedProjectile.gameObject, 0.3f);
                }
            }
        }

        private void UpdateHealth()
        {
            if (health <= 0)
            {
                currentState = deathState;
                EnterState();
            }
        }

        public void Death()
        {
            Destroy(gameObject, deathTime);
        }

    }
}
