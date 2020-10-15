using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerGun;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem projectile;

    Transform targetEnemy;

    void Update()
    {
        GetTargetEnemy();
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

    private void GetTargetEnemy()
    {
        var enemies = FindObjectsOfType<EnemyDamage>();
        if(enemies.Length == 0) { return; }

        Transform closestEnemy = enemies[0].transform;

        foreach (EnemyDamage currentEnemy in enemies)
        {
            closestEnemy = CheckAndGetClosestEnemy(closestEnemy, currentEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform CheckAndGetClosestEnemy(Transform transformA, Transform transformB)
    {
        var distanceFromA = Vector3.Distance(transform.position, transformA.position);
        var distanceFromB = Vector3.Distance(transform.position, transformB.position);

        if(distanceFromA < distanceFromB) 
        { 
            return transformA; 
        }
        return transformB;
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
