using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternBoss : MonoBehaviour
{
    public int sightRange = 20;
    public float moveSpeed = 5f;
    public float attackDelay = 10f;
    public int currentHealth;
    public SpriteRenderer spriteRendererFlag;
    public Transform eyes;
    public Transform attackTarget;
    public BaseWeapon[] weapons;
    public ContactFilter2D contactFilter;

    [HideInInspector] public float yFollowSpeed = 2.0f; // Adjust the Y-axis follow speed
    [HideInInspector] public float minX = -5.0f; // Adjust the minimum X-axis value
    [HideInInspector] public float maxX = 5.0f; // Adjust the maximum X-axis value
    [HideInInspector] public float xChangeInterval = 3.0f; // Adjust the interval for changing X-axis position
    [HideInInspector] public float xChangeTimer = 0.0f; // Timer for changing X-axis position


    [HideInInspector] public BossHealth bossHealth;
    [HideInInspector] public IBossState currentState;
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public AttackProjectileState attackProjectileState;
    [HideInInspector] public AttackFireState attackFireState;
    [HideInInspector] public AttackMissileState attackMissileState;


    private void Awake()
    {
        idleState = new IdleState(this);
        attackProjectileState = new AttackProjectileState(this);
        attackFireState = new AttackFireState(this);
        attackMissileState = new AttackMissileState(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;

        // Set the initial delay before the first attack
        idleState.attackTimer = attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();

        //if(currentHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }
}
