using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{    



    [Header("References")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private BaseWeapon baseWeapon;
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        baseWeapon = GetComponentInChildren<BaseWeapon>();
    }

    public void PlayerToFire(bool canFire)
    {
        if (canFire)
        {
            // Implement Shooting Projectiles
            baseWeapon.Fire();

            controller.OnFire();
        }
    }

    public void PlayerToReload(bool canReload)
    {
        if (canReload)
        {
            // If bullets are full, return
            // Otherwise reload

            controller.OnReload();
        }
    }
}
