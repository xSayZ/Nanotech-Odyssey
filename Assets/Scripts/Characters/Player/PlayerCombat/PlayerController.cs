using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Running,
        Dashing
    }

    [Header("Player Variables")]
    [SerializeField] private int health = 3;

    [Header("References")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerCombatHandler combat;

    Vector2 movementInput;
    Vector2 aimInput;
    public PlayerState playerState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombatHandler>();
    }

    void Update()
    {
        animator.SetFloat("Speed", movementInput.sqrMagnitude);

        // Temporary death
        if(health == 0)
        {
            animator.SetInteger("Health", 0);
        }

    }

    private void FixedUpdate()
    {
        movementInput = InputManager.GetInstance().GetMoveDirection();
        aimInput = InputManager.GetInstance().GetAimDirection();
        bool fireInput = InputManager.GetInstance().GetFirePressed();
        bool reloadInput = InputManager.GetInstance().GetReloadPressed();

        movement.HandleFixedUpdate(movementInput, aimInput);
        combat.HandleWeaponState(fireInput, reloadInput);
    }

    public void OnFire()
    {
        animator.Play("Player_Fire_Weapon");
    }

    public void OnReload()
    {
        animator.Play("Player_Reload_Weapon");
    }

}
