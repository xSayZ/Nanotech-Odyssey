using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * projectileSpeed * Time.deltaTime;
    }
}
