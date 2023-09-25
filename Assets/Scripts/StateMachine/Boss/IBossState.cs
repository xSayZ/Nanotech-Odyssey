using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState
{
    void UpdateState();

    void OnTriggerEnter2D(Collider2D other);
}
