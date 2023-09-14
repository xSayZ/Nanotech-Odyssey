using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : BaseWeapon
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject projectile;

    public override void Fire()
    {
        projectile = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
