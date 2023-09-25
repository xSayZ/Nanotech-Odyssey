using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{    

    public enum FireState
    {
        Idle,
        Firing,
        Reloading
    }

    [Header("References")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private LaserWeapon laserWeapon;
    [SerializeField] public Transform firePoint;
    private FireState currentState = FireState.Idle;
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        laserWeapon = GetComponentInChildren<LaserWeapon>();
    }

    public void HandleWeaponState(bool canFire, bool canReload)
    {
        switch (currentState)
        {
            case FireState.Idle:
                if (canFire && Time.time >= laserWeapon.fireCooldown && laserWeapon.currentAmmo > 0)
                {
                    currentState = FireState.Firing;

                    laserWeapon.Fire(firePoint);
                    controller.OnFire();
                }
                if (canReload && laserWeapon.currentAmmo < laserWeapon.maxAmmo)
                {
                    currentState = FireState.Reloading;
                }
                break;
            case FireState.Firing:
                if (!canFire)
                {
                    currentState = FireState.Idle;
                }
                else if (canFire && Time.time >= laserWeapon.fireCooldown && laserWeapon.currentAmmo > 0)
                {
                    laserWeapon.Fire(firePoint);
                    controller.OnFire();
                }
                break;
            case FireState.Reloading:
                Debug.Log("reload");
                laserWeapon.Reload();
                controller.OnReload();
                currentState = FireState.Idle;
                break;
        }
    }
}
