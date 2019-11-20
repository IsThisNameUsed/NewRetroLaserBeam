using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyBehaviour : EnemyBehaviour {

    public float timerBeforeShooting = 2;

    public GameObject bullet;

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

    public void Shoot()
    { 
        Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.target = targetedPlayer.body;
        bulletScript.isActiv = true;
    }
}
