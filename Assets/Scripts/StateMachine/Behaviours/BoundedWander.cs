using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class BoundedWander : MonoBehaviour, IWanderBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _rotationOffset;

    private Vector2 currentPoint;
    private Vector2 _destination;
    private Vector2 _start;
    private Quaternion _heading;

    private List<PathNode> path = new List<PathNode>();

    private Color _pathColor;

    private float _timer = 0.0f;
    private float _waitForUpdate = 0.25f;

    private Vector3 _lastSeen;

    public void Start()
    {
        _pathColor = new Color(UnityEngine.Random.Range(0.0f, 0.5f), UnityEngine.Random.Range(0.0f, 0.5f), UnityEngine.Random.Range(0.0f, 0.5f), 1.0f);
    }

    public void Init(Transform target)
    {
        path.Clear();
        if (target != null)
        {
            GetDestinationToPlayer(target);
        }
    }

    public void Wander()
    {

        if (path.Count < 1)
        {
            GetNewDestination();
        }

        _timer += Time.deltaTime;
        if(_timer >= _waitForUpdate)
        {
            UpdatePath();
            _timer = 0.0f;
        }

        if (Vector2.Distance(transform.position, currentPoint) < 1.0f)
        {
            if (path.Count > 0) GetNextPoint();
        }
        _heading = GetNewHeading(currentPoint);

        transform.rotation = Quaternion.Slerp(transform.rotation, _heading, _turnSpeed);

        transform.position += transform.up * Time.deltaTime * _movementSpeed;
    }

    private void GetNextPoint()
    {
        path.Remove(path[path.Count - 1]);
        if(path.Count > 0)
        {
            currentPoint = path[path.Count - 1].WorldPosition;
            _start = currentPoint;
        }
    }

    private void GetNewDestination()
    {
        _start = transform.position;
        _destination = Path.GetPosition();
    }

    private void GetDestinationToPlayer(Transform target)
    {
        _lastSeen = target.position;
        _start = transform.position;
        _destination = _lastSeen;
    }

    private void UpdatePath()
    {
        path = Path.FindPath(_start, _destination);
        if ( path is null || path.Count < 1 ) return;
        currentPoint = path[path.Count - 1].WorldPosition;
    }

    private Quaternion GetNewHeading(Vector2 nextPosition)
    {
        Vector2 directionTowardsDestination = (nextPosition - (Vector2)transform.position).normalized;

        float angleTowardsDestination = Mathf.Atan2(directionTowardsDestination.y, directionTowardsDestination.x) * Mathf.Rad2Deg;

        return Quaternion.Euler(0.0f, 0.0f, angleTowardsDestination + _rotationOffset);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _pathColor;

        if(path != null && path.Count > 0)
        {
            foreach(PathNode node in path)
            {
                Gizmos.DrawCube(node.WorldPosition, new Vector3(1, 1, 1));
            }
        }

        if(_lastSeen != Vector3.zero)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawCube(_destination, new Vector3(1, 1, 1));
        }
    }
}