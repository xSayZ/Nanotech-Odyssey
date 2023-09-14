using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCooldown;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int currentAmmo;

    public virtual void Fire()
    {
        Debug.Log("Brrrr Brrrr");
    }

    public virtual void Reload()
    {
        Debug.Log("Tsch tsch");
    }
}
