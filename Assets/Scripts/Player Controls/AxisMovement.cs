using UnityEngine;

public class AxisMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float radius;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask ignore;
    [SerializeField] private LayerMask whatIsWall;

    private Vector2 newDirection;

    public void Move(Transform transform)
    {
        newDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);

        RaycastHit2D[] results = new RaycastHit2D[32];
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(whatIsWall);

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(transform.position, new Vector2(newDirection.x, 0.0f), maxDistance, whatIsWall);
        foreach(RaycastHit2D hit in hitInfo)
        {
            newDirection += hit.normal;
        }

        hitInfo = Physics2D.RaycastAll(transform.position, new Vector2(0.0f, newDirection.y), maxDistance, whatIsWall);
        foreach (RaycastHit2D hit in hitInfo)
        {
            newDirection += hit.normal;
        }

        Vector3 newPosition = transform.position + (Vector3)(newDirection.normalized * Time.deltaTime * movementSpeed);

        transform.position = newPosition;
    }

    public void OnDrawGizmos()
    {
        /*
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, newDirection * maxDistance);
        Gizmos.DrawRay(transform.position + transform.right * 0.01f, newDirection * maxDistance);
        Gizmos.DrawRay(transform.position - transform.right * 0.01f, newDirection * maxDistance);
        Gizmos.DrawRay(transform.position + transform.right * 0.02f, newDirection * maxDistance);
        Gizmos.DrawRay(transform.position - transform.right * 0.02f, newDirection * maxDistance);

        Gizmos.color = Color.blue;        
        Gizmos.DrawRay(transform.position, transform.up * maxDistance);
        Gizmos.DrawRay(transform.position + transform.right * 0.01f, transform.up * maxDistance);
        Gizmos.DrawRay(transform.position - transform.right * 0.01f, transform.up * maxDistance);
        Gizmos.DrawRay(transform.position + transform.right * 0.02f, transform.up * maxDistance);
        Gizmos.DrawRay(transform.position - transform.right * 0.02f, transform.up * maxDistance);
        */

        Gizmos.DrawWireCube(transform.position + (Vector3)newDirection.normalized, transform.localScale);
    }
}