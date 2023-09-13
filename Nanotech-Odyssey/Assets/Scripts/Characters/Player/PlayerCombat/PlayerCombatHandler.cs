using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{    



    [Header("References")]
    [SerializeField] private PlayerController controller;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void Fire(bool canFire)
    {
        if (canFire)
        {
            // Implement Shooting Projectiles

            controller.OnFire();
        }
    }

    public void Reload(bool canReload)
    {
        if (canReload)
        {
            // If bullets are full, return
            // Otherwise reload

            controller.OnReload();
        }
    }
}
