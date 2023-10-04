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

    #region
    [Header("References")]
    [SerializeField] private PlayerController controller;
    [SerializeField] public LaserWeapon laserWeapon;
    [SerializeField] public Transform firePoint;
    private FireState currentState = FireState.Idle;
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        laserWeapon = GetComponentInChildren<LaserWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile collidedProjectile = collision.gameObject.GetComponent<Projectile>();

            // Check if the projectile owner is not the enemy itself
            if (collidedProjectile.owner != gameObject)
            {
                UIManager uiManager = GameManager.GetInstance().GetComponent<UIManager>();

                if (controller.armor >= 1)
                {
                    controller.armor--;
                    uiManager.UpdateArmor(controller.armor);
                }
                else if (controller.armor <= 0)
                {
                    controller.health--;
                    uiManager.UpdateHP(controller.health);
                }

                Destroy(collidedProjectile.gameObject);
            }
        }
    }
    #endregion
    public void HandleWeaponState(bool canFire, bool canReload)
    {
        switch (currentState)
        {
            case FireState.Idle:
                if (canFire && Time.time >= laserWeapon.fireCooldown && laserWeapon.currentAmmo > 0)
                {
                    currentState = FireState.Firing;

                    controller.audioSource.PlayOneShot(laserWeapon.audioClip);
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
                    controller.audioSource.PlayOneShot(laserWeapon.audioClip);
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
