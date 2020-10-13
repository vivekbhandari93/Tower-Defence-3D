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

    List<Waypoint> path = new List<Waypoint>();


    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    [SerializeField] Waypoint startingPoint;
    [SerializeField] Waypoint endingPoint;

    public List<Waypoint> GetPath() 
    {
        if (path.Count == 0)
        {
            LoadBlock();
            ColorStartAndEnd();
            BreadthFirstSearch();
            CreatePath();
        }
        return path; 
    }

    private void LoadBlock()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            if (grid.ContainsKey(waypoint.GetGridPos()))
            {
                // TODO Skipping overlapping block
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
        //print("Journey starts at "+ startingPoint);

        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            //print("SearchCenter pop out!"+ searchCenter);
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
            //print("Journey ends at " + searchCenter);
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbour(neighbourCoordinates);
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
            //print("New neighbour added." + neighbour);
            neighbour.exploredFrom = searchCenter; 
        }
    }

    private void CreatePath()
    {
        path.Add(endingPoint);

        Waypoint previous = endingPoint.exploredFrom;
        while(previous != startingPoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }
        path.Add(startingPoint);
        path.Reverse();
    }
}
