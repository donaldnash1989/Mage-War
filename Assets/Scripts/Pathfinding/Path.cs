using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private static List<PathNode> _openList = new List<PathNode>();
    private static List<PathNode> _closedList = new List<PathNode>();

    private static PathNode currentNode;

    private static PathNode[,] _grid;
    private static int _gridWidth;
    private static int _gridHeight;
    private static int _cellSize;

    public static List<PathNode> FindPath(PathNode start, PathNode end)
    {
        currentNode = start;

        //set heuristic of start;
        currentNode.HCost = GetHCost(currentNode, end);
        currentNode.GCost = 0;

        _openList.Clear();
        _openList.Add(start);

        for (int i = 0; i < _grid.Length; i++)
        {
            if (currentNode == end)
            {
                return CreatePath(currentNode);
            }

            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (x == 0 & y == 0) continue;

                    int xPos = currentNode.GridPosition.x + x;
                    int yPos = currentNode.GridPosition.y + y;

                    if (xPos < 0 || yPos < 0) continue;
                    if (xPos >= _gridWidth || yPos >= _gridHeight) continue;

                    bool p = _grid[xPos, yPos].IsPassable;

                    if (p)
                    {
                        if (!_closedList.Contains(_grid[xPos, yPos]))
                        {
                            if(!_openList.Contains(_grid[xPos, yPos]))
                            {
                                _grid[xPos, yPos].HCost = GetHCost(_grid[xPos, yPos], end);
                                _grid[xPos, yPos].GCost = GetGCost(currentNode, Mathf.Abs(x) + Math.Abs(y));
                                _grid[xPos, yPos].prevNode = currentNode;
                                _openList.Add(_grid[xPos, yPos]);
                            }
                            else
                            {
                                //Update GCost
                                PathNode node = _openList.Find(o => o == _grid[xPos, yPos]);
                                int dist = Mathf.Abs(node.GridPosition.x - currentNode.GridPosition.x) + Mathf.Abs(node.GridPosition.y - currentNode.GridPosition.y);
                                dist *= 10;
                                dist = Mathf.Clamp(dist, 10, 14);
                                int newGCost = node.GCost + dist;
                                if (currentNode.GCost > newGCost)
                                {
                                    currentNode.GCost = newGCost;
                                    currentNode.prevNode = node;
                                }
                            }
                        }
                    }
                }
            }

            currentNode = GetCheapestNode(currentNode);
        }

        return new List<PathNode>();
    }

    internal static void SetDimensions(int gridWidth, int gridHeight, int cellSize)
    {
        _gridWidth = gridWidth;
        _gridHeight = gridHeight;
        _cellSize = cellSize;
    }

    internal static void SetGrid(PathNode[,] nodes)
    {
        _grid = nodes;
    }

    private static List<PathNode> CreatePath(PathNode pathNode)
    {
        List<PathNode> path = new List<PathNode>();

        PathNode currentPathNode = pathNode;

        while (currentPathNode != null)
        {
            path.Add(currentPathNode);
            currentPathNode = currentPathNode.prevNode;
        }
        _openList.Clear();
        _closedList.Clear();
        foreach(PathNode node in _grid)
        {
            node.HCost = 0;
            node.GCost = 0;
            node.prevNode = null;
        }
        return path;
    }

    private static PathNode GetCheapestNode(PathNode currentNode)
    {
        _openList.Remove(currentNode);
        _closedList.Add(currentNode);

        PathNode lowestNode = new PathNode(Vector2.zero, Vector2Int.zero, false);
        lowestNode.HCost = int.MaxValue;

        foreach(PathNode node in _openList)
        {
            if (node.FCost < lowestNode.FCost) lowestNode = node;
        }
        return lowestNode;
    }

    private static int GetGCost(PathNode currentNode, int dist)
    {
        dist *= 10;
        dist = Mathf.Clamp(dist, 10, 14);
        return currentNode.GCost + dist;
    }

    private static int GetHCost(PathNode pathNode, PathNode end)
    {
        int hCost = Mathf.Abs(end.GridPosition.x - pathNode.GridPosition.x) + Mathf.Abs(end.GridPosition.y - pathNode.GridPosition.y);

        return hCost * 10;
    }

    public static List<PathNode> FindPath(Vector2 position, Vector2 destination)
    {
        position /= _cellSize;
        destination /= _cellSize;

        position.x = Mathf.Clamp(position.x, 0, _gridWidth);
        position.y = Mathf.Clamp(position.y, 0, _gridHeight);

        destination.x = Mathf.Clamp(destination.x, 0, _gridWidth);
        destination.y = Mathf.Clamp(destination.y, 0, _gridHeight);

        PathNode start = _grid[(int)position.x, (int)position.y];
        PathNode end = _grid[(int)destination.x, (int)destination.y];

        if (!end.IsPassable) new List<PathNode>();

        return FindPath(start, end);
    }

    public static Vector2 GetPosition()
    {
        PathNode node = _grid[UnityEngine.Random.Range(0, _gridWidth), UnityEngine.Random.Range(0, _gridHeight)];
        while (!node.IsPassable)
        {
            node = _grid[UnityEngine.Random.Range(0, _gridWidth), UnityEngine.Random.Range(0, _gridHeight)];
        }
        return node.WorldPosition;
    }
}
