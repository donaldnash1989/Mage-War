using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForPlayer : MonoBehaviour, ITargetSearch
{
    private GameObject _player;

    [SerializeField] private float _searchRadius;

    [SerializeField] private ContactFilter2D _contactFilter2D;
    private RaycastHit2D[] results = new RaycastHit2D[32];

    public Transform FindTarget()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
        else
        {
            Vector3 directionTowardsPlayer = (_player.transform.position - transform.position).normalized;

            if (Vector3.Distance(transform.position, _player.transform.position) < _searchRadius)
            {
                int hits = Physics2D.Raycast(transform.position, directionTowardsPlayer, _contactFilter2D, results, _searchRadius);
                if (hits > 0)
                {
                    for (int i = 0; i < hits; i++)
                    {

                        float distanceToObstacle = Vector3.Distance(results[i].point, transform.position);
                        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);

                        if (distanceToObstacle < distanceToPlayer)
                        {
                            return null;
                        }
                    }
                }
                return _player.transform;
            }
        }

        return null;
    }
}
