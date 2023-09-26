using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternEnemy : MonoBehaviour
{
    public float searchingDuration = 4f;
    public float searchTimer;
    public int sightRange = 20;
    public float moveSpeed = 5f;
    public bool facingRight = true;
    public Transform[] wayPoints;
    public Transform eyes;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public SpriteRenderer spriteRendererFlag;
    public ContactFilter2D contactFilter;
    public Rigidbody2D rb;
    public BaseWeapon[] weapons;

    [HideInInspector] public Quaternion targetRotation;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public PatrolState patrolState;

    private void Awake()
    {
        rb = FindObjectOfType<Rigidbody2D>();
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolState;
    }

    public void EnterState()
    {
        currentState.OnEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter(collision);
    }
}
