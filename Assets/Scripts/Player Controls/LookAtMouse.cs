using UnityEngine;

public class LookAtMouse : MonoBehaviour, ILookAt
{
    public void Look(Transform transform)
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 directionToFace = (mouseWorldPosition - transform.position).normalized;

        float angleBetweenPlayerAndMouse = Mathf.Atan2(directionToFace.y, directionToFace.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angleBetweenPlayerAndMouse - 90);
    }
}
