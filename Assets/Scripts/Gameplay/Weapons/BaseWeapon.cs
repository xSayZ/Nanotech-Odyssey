using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] public float fireRate;
    [SerializeField] public float fireCooldown;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    public virtual void Fire(GameObject firePoint)
    {
        Debug.Log("Brrrr Brrrr");
    }

    public virtual void Reload()
    {
        Debug.Log("Tsch tsch");
    }
}
