using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemyBehaviour : EnemyBehaviour {

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

    public override bool  EnemyIsActive(bool _state) 
    {
        animator.SetBool("isActive", _state);
        return _state;
    }

    public bool EnemyIsClose(bool _state)
    {
        animator.SetBool("isClose", _state);
        return _state;
    }
}
