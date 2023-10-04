using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePatternEnemy : MonoBehaviour
{
    public float searchingDuration = 4f;
    public float searchTimer;
    public float health = 3;
    public float deathTime = 1.2f;
    public int sightRange = 20;
    public float moveSpeed = 5f;
    public bool facingRight = true;
    public Transform[] wayPoints;
    public BaseWeapon[] weapons;
    public Sprite[] indicators;
    public Transform eyes;
    public Vector3 offset = new Vector3(0, 1f, 0);
    public ContactFilter2D contactFilter;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Image indicator;
    [HideInInspector] public Quaternion targetRotation;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public DeathState deathState;

    private void Awake()
    {
        rb = FindObjectOfType<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        indicator = GetComponentInChildren<Image>();
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);
        deathState = new DeathState(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolState;
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
            health--;
        }
    }


    private void UpdateHealth()
    {
        if(health <= 0)
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
