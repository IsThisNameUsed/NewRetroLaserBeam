using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemyBehaviour : EnemyBehaviour {

    protected override void  Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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
