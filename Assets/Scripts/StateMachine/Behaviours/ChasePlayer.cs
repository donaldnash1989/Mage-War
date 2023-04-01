using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour, IChaseBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _rotationOffset;
    [SerializeField] private LayerMask _whatIsObstacle;
    private Quaternion _rotation;

    public TargetState Chase(Transform target)
    {
        if (target == null) return TargetState.CANT_SEE;

        Vector3 directionTowardsPlayer = (target.position - transform.position).normalized;

        float angleTowardsPlayer = Mathf.Atan2(directionTowardsPlayer.y, directionTowardsPlayer.x) * Mathf.Rad2Deg;

        _rotation = Quaternion.Euler(0.0f, 0.0f, angleTowardsPlayer + _rotationOffset);

        Vector3 newPosition = transform.position + (transform.up * Time.deltaTime * _movementSpeed);

        if (Vector3.Distance(transform.position, target.position) < 15.0f) return TargetState.CAN_ATTACK;

        if (Physics2D.Raycast(transform.position, directionTowardsPlayer, Vector3.Distance(transform.position, target.position), _whatIsObstacle))
        {
            newPosition = transform.position;
            _rotation = transform.rotation;
            return TargetState.CANT_SEE;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, _turnSpeed);

        transform.position = newPosition;

        return TargetState.CAN_SEE;
    }
}
