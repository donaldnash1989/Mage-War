using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    [SerializeField] private Vector2 _offset;
    [SerializeField] private int _cellSize;

    [SerializeField] private int _gridWidth = 0;
    [SerializeField] private int _gridHeight = 0;

    [SerializeField] private bool _debug = true;
    [SerializeField] private LayerMask _whatIsWall;

    private Vector2 _startPos;
    private Vector2 _endPos;

    private PathNode[,] nodes;

    private float _timer = 0.0f;
    private float _waitForUpdate = 0.25f;

    public void Start()
    {
        _gridWidth /= _cellSize;
        _gridHeight /= _cellSize;

        UpdateGrid();
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _waitForUpdate)
        {
            UpdateGrid();
            _timer = 0.0f;
        }
    }

    private void UpdateGrid()
    {
        if (_cellSize < 1) _cellSize = 1;

        nodes = new PathNode[_gridWidth, _gridHeight];

        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                Vector2 worldPosition = new Vector2(x, y) * _cellSize + _offset;
                Vector2Int gridPosition = new Vector2Int(x, y);

                RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(worldPosition, new Vector2(_cellSize, _cellSize), 0.0f, Vector2.up, 0.1f, _whatIsWall);

                bool isPassable = true;

                foreach (RaycastHit2D raycastHit2D in raycastHit2Ds)
                {
                    isPassable = false;
                }

                PathNode node = new PathNode(worldPosition, gridPosition, isPassable);

                nodes[x, y] = node;
            }
        }

        Path.SetGrid(nodes);
        Path.SetDimensions(_gridWidth, _gridHeight, _cellSize);
    }

    public void OnDrawGizmos()
    {
        if (_debug)
        {
            if (nodes != null && nodes.Length >= _gridHeight * _gridWidth)
            {
                float alpha = 0.3f;

                for (int y = 0; y < _gridHeight; y++)
                {
                    for (int x = 0; x < _gridWidth; x++)
                    {
                        Gizmos.color = new Color(1.0f, 1.0f, 1.0f, alpha);

                        if (!nodes[x, y].IsPassable) Gizmos.color = new Color(0.0f, 0.0f, 0.0f, alpha);
                        if (nodes[x, y].WorldPosition == _startPos) Gizmos.color = new Color(0.0f, 1.0f, 0.0f, alpha);
                        if (nodes[x, y].WorldPosition == _endPos) Gizmos.color = new Color(1.0f, 0.0f, 0.0f, alpha);

                        Gizmos.DrawWireCube(nodes[x, y].WorldPosition, new Vector3(_cellSize, _cellSize, _cellSize));
                        Gizmos.DrawCube(nodes[x, y].WorldPosition, new Vector3(_cellSize, _cellSize, _cellSize));
                    }
                }
            }
        }
    }
}
