using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerGun;
    [SerializeField] Transform enemy;

    void Update()
    {
        towerGun.LookAt(enemy);
    }
}
