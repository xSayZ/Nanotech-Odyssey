using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [Space]
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

    public virtual void Fire(GameObject firePoint)
    {
        fireCooldown = Time.time + 1f / fireRate;
        currentAmmo--;
        projectile = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);

        Debug.Log("Brrrr Brrrr");
    }

    public virtual void Reload()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Tsch tsch");
    }
}
