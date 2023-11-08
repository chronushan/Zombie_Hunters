using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float scanRange = 10f;
    [SerializeField] private LayerMask targetLayer;
    private RaycastHit2D[] targets;
    public Transform nearestTarget;

    private float nearestDistance;
    private float distance;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0f, targetLayer);

        if (targets.Length > 0)
        {
            nearestTarget = targets[0].transform;
            nearestDistance = Vector2.Distance(transform.position, nearestTarget.position);

            for (var i = 1; i < targets.Length; i++)
            {
                distance = Vector2.Distance(transform.position, targets[i].transform.position);

                if (distance < nearestDistance)
                {
                    nearestTarget = targets[i].transform;
                    nearestDistance = distance;
                }
            }
        }
        else
        {
            nearestTarget = null;
        }
        
    }
}
