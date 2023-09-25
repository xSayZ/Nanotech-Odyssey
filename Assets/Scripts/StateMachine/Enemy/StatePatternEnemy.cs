using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternEnemy : MonoBehaviour
{
    public float searchingDuration = 4f;
    public int sightRange = 20;
    public float moveSpeed = 5f;
    public Transform[] wayPoints;
    public Transform eyes;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public SpriteRenderer spriteRendererFlag;
    public ContactFilter2D contactFilter;

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public PatrolState patrolState;

    private void Awake()
    {
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolState;
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
