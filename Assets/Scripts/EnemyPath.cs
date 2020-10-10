using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] List<Waypoint> waypoints;
    const float nextPointTimeDelay = 1f;

    private void Start()
    {
        StartCoroutine(GetPath());
    }

    IEnumerator GetPath()
    {
        foreach(Waypoint waypoint in waypoints)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(nextPointTimeDelay);
        }
    }
}
