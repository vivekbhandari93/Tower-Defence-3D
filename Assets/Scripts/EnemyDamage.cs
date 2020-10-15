using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 50;
    [SerializeField] ParticleSystem hitVFX;
    [SerializeField] ParticleSystem deathVFXPrefab;

    private void OnParticleCollision(GameObject other)
    {
        hitPoints -= 1;
        hitVFX.Play();
        if (hitPoints <= Mathf.Epsilon)
        {
            Destroy(gameObject);
            var deathVFXInstance = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            deathVFXInstance.Play();
        }
    }
}
