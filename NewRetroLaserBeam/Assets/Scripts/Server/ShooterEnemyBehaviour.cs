using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyBehaviour : EnemyBehaviour {

    public float timerBeforeShooting = 2;

    private void Start()
    {
        Instanciation();
    }

    private void Update()
    {
        if (hitCooldown > 0)
        {
            hitCooldown -= Time.deltaTime;
        }
        CheckHealth();
    }

    public override bool EnemyIsActive(bool _state)
    {
        animator.SetBool("isShooting", _state);
        return _state;
    }
}
