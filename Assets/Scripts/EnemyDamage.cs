using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 50;

    private void OnParticleCollision(GameObject other)
    {
        hitPoints -= 1;
        if (hitPoints <= Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }
}
