using UnityEngine;

public class ShootAtPlayer : MonoBehaviour, IAttackBehaviour
{
    public GameObject projectile;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _rotationOffset;
    [SerializeField] private LayerMask _whatIsObstacle;
    private Quaternion _rotation;

    float timer = 1.0f;
    float lifeTime = 1.0f;

    public void Start()
    {
        lifeTime += UnityEngine.Random.Range(-0.2f, 0.2f);
    }

    public TargetState Attack(Transform target)
    {
        if (target == null) return TargetState.CANT_SEE;

        RotateTowardsPlayer(target);

        transform.position = KeepDistance(target);

        if (timer < lifeTime) timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Instantiate(projectile, transform.position, _rotation);
            timer = 0.0f;
        }

        return CheckVision(target);
    }

    private TargetState CheckVision(Transform target)
    {

        Vector3 directionTowardsPlayer = (target.position - transform.position).normalized;

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (Physics2D.Raycast(transform.position, directionTowardsPlayer, distanceToPlayer, _whatIsObstacle))
        {
            return TargetState.CANT_SEE;
        }
        return TargetState.CAN_SEE;
    }

    private Vector3 KeepDistance(Transform target)
    {

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer < 9.0f)
        {
            return transform.position + (-transform.up * Time.deltaTime * _movementSpeed);
        }

        if (distanceToPlayer > 10.0f)
        {
            return transform.position + (transform.up * Time.deltaTime * _movementSpeed);
        }

        return transform.position;
    }

    private void RotateTowardsPlayer(Transform target)
    {
        Vector3 directionTowardsPlayer = (target.position - transform.position).normalized;

        float angleTowardsPlayer = Mathf.Atan2(directionTowardsPlayer.y, directionTowardsPlayer.x) * Mathf.Rad2Deg;

        _rotation = Quaternion.Euler(0.0f, 0.0f, angleTowardsPlayer + _rotationOffset);

        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, _turnSpeed);
    }
}
