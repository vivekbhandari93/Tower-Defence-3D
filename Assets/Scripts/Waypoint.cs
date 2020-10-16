﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    public bool isPlaceable = true;

    public Waypoint exploredFrom;

    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize) 
            );
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            FindObjectOfType<TowerKeep>().AddTower(this);

        }
    }

    // This method is disabled in the editor (not required anymore).
    // Kept for future referance, purpose only.
    public void SetTopColor(Color color)
    {
        var topView = transform.Find("Top").GetComponent<MeshRenderer>();
        topView.material.color = color;
    }
}
