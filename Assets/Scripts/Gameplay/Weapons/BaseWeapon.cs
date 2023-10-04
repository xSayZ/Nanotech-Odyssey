using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [Space]
    [Range(0.1f, 15f)]
    [SerializeField] public float fireRate = 5f;
    [SerializeField] public float range = 5f;
    public float fireCooldown = 0f;
    [SerializeField] public int maxAmmo = 15;
    [HideInInspector] public int currentAmmo;

    [Space]
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public GameObject projectile;
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    public virtual void Fire(Transform firePoint)
    {
        fireCooldown = Time.time + 1f / fireRate;
        currentAmmo--;
        GameObject newProjectile = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
        newProjectile.GetComponent<Projectile>().owner = transform.root.gameObject; // Assign the owner
    }

    public virtual void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
