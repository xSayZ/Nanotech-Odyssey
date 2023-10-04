using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("Player Variables")]
    [SerializeField] public int health = 3;
    [SerializeField] public int armor = 3;

    [Header("References")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerCombatHandler combat;
    [SerializeField] private SpriteRenderer spriteRenderer;

    Vector2 movementInput;
    Vector2 aimInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombatHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        UIManager uiManager = GameManager.GetInstance().GetComponent<UIManager>();

        if(uiManager != null)
        {
            uiManager.maxHP = health;
            uiManager.maxAmmo = combat.laserWeapon.maxAmmo;
            uiManager.UpdateArmor(armor);
            uiManager.UpdateBulletCount(combat.laserWeapon.currentAmmo);
            uiManager.UpdateHP(health);
        }
    }

    void Update()
    {
        animator.SetFloat("Speed", movementInput.sqrMagnitude);

        // Temporary death
        if(health <= 0)
        {
            animator.SetInteger("Health", 0);
        }

        AdjustSortingLayer();

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
        GameManager.GetInstance().GetComponent<UIManager>().UpdateBulletCount(combat.laserWeapon.currentAmmo);
    }

    public void OnReload()
    {
        animator.Play("Player_Reload_Weapon");
        GameManager.GetInstance().GetComponent<UIManager>().UpdateBulletCount(combat.laserWeapon.currentAmmo);
    }

    private void AdjustSortingLayer()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }

}
