using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerKeep : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int maxNumberOfTowers = 5;
    Queue<Tower> queueTowers = new Queue<Tower>();

    [SerializeField] Transform towersTransform;

    public void AddTower(Waypoint waypoint)
    {
        if (queueTowers.Count < maxNumberOfTowers)
        {
            InstantiateNewTower(waypoint);
        }
        else
        {
            MoveTower(waypoint);
        }
    }

    private void InstantiateNewTower(Waypoint waypoint)
    {
        var towerInstance = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity);
        
        towerInstance.SetTowerWaypoint(waypoint);
        waypoint.isPlaceable = false;

        queueTowers.Enqueue(towerInstance);

        towerInstance.transform.parent = towersTransform;
    }

    private void MoveTower(Waypoint waypoint)
    {
        var oldTower = queueTowers.Dequeue();
        oldTower.GetTowerWaypoint().isPlaceable = true;

        oldTower.SetTowerWaypoint(waypoint);
        waypoint.isPlaceable = false;

        oldTower.transform.position = waypoint.transform.position;

        queueTowers.Enqueue(oldTower);
        
    }
}
