using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerGun;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem projectile;

    void Update()
    {
        if (targetEnemy)
        {
            towerGun.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
        {
            shoot(false);
        }
    }

    private void FireAtEnemy()
    {
        float distance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if(distance <= attackRange)
        {
            shoot(true);
        }
        else
        {
            shoot(false);
        }
    }

    private void shoot(bool isActive)
    {
        var emissionModule = projectile.emission;
        emissionModule.enabled = isActive;

    }
}
