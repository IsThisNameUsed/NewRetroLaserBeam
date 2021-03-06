﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyBehaviour : EnemyBehaviour {

    public float timerBeforeShooting = 2;

    public GameObject bullet;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool EnemyIsActive(bool _state)
    {
        animator.SetBool("isShooting", _state);
        return _state;
    }

    public void Shoot()
    { 
        Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.target = targetedPlayer.body;
        bulletScript.targetedPlayer = targetedPlayer;
        bulletScript.damage = damagePoint;
        bulletScript.isActiv = true;
    }
}
