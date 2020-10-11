using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    Waypoint searchCenter;
    bool isRunning = true;


    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    [SerializeField] Waypoint startingPoint;
    [SerializeField] Waypoint endingPoint;


    private void Start()
    {
        LoadBlock();
        ColorStartAndEnd();
        BreadthFirstSearch();

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


    private void BreadthFirstSearch()
    {
        queue.Enqueue(startingPoint);
        print("Startingpoint push in!"+ startingPoint);

        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            print("SearchCenter pop out!"+ searchCenter);  // TODO remove
            CheckForEndPoint();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void CheckForEndPoint()
    {
        if(searchCenter == endingPoint)
        {
            isRunning = false;
            print("SeachCenter is endPoint." + searchCenter); // TODO remove
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            try
            {
                QueueNewNeighbour(neighbourCoordinates);
            }
            catch
            {
                // TODO key in grid not found!
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            // TODO not to add in queue
        }
        else
        {
            queue.Enqueue(neighbour);
            print("New neighbour added." + neighbour); // TODO remove
            neighbour.exploredFrom = searchCenter; 
        }
    }
}
