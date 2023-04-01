using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Vector2 WorldPosition { get; }

    public Vector2Int GridPosition { get; }

    public bool IsPassable { get; }

    public int HCost { get; set; }

    public int GCost { get; set; }

    public int FCost { get { return HCost + GCost; } }

    public PathNode prevNode { get; set; }

    public PathNode(Vector2 worldPosition, Vector2Int gridPosition, bool isPassable)
    {
        WorldPosition = worldPosition;
        GridPosition = gridPosition;
        IsPassable = isPassable;
    }

    public void UpdateHCost(int val)
    {
        HCost = val;
    }

    public void UpdateGCost(int val)
    {
        GCost = val;
    }
}
