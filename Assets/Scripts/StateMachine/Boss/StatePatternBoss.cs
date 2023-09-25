using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternBoss : MonoBehaviour
{
    public int sightRange = 20;
    public float moveSpeed = 5f;
    public int currentHealth;
    public SpriteRenderer spriteRendererFlag;
    public Transform eyes;
    public Transform attackTarget;
    public BaseWeapon[] weapons;
    public ContactFilter2D contactFilter;


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
