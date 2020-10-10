using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] Waypoint startingPoint;
    [SerializeField] Waypoint endingPoint;


    private void Start()
    {
        LoadBlock();
        ColorStartAndEnd();
    }

    private void LoadBlock()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            if (grid.ContainsKey(waypoint.GetGridPos()))
            {
                Debug.LogWarning("Skipping overlapping block" + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }

    }

    private void ColorStartAndEnd()
    {
        startingPoint.SetTopColor(Color.green);
        endingPoint.SetTopColor(Color.red);
    }
}
