using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : BaseWeapon
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject projectile;

    public override void Fire(GameObject firePoint)
    {
        fireCooldown = Time.time + 1f / fireRate;
        currentAmmo--;
        projectile = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
    }

    public override void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
