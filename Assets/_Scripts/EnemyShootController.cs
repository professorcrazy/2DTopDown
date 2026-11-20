using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootController : MonoBehaviour
{
    bool canShoot = true;


    [SerializeField] private float shootPlayerRadius = 5f;
    [SerializeField] private LayerMask playerLayer;
    public bool playerInRange = false;

    [SerializeField] private EnemyShoot[] enemyFirepoints;

    [SerializeField] private float rotationSpeed = 5f;

    public float firerate;

    private void Start()
    {
        firerate = 1f / firerate;
        if (enemyFirepoints == null || enemyFirepoints.Length == 0)
        {
            enemyFirepoints = GetComponentsInChildren<EnemyShoot>();
        }
    }

    void FixedUpdate()
    {

        Collider2D playerRef = Physics2D.OverlapCircle(transform.position, shootPlayerRadius, playerLayer);
        playerInRange = playerRef != null;
        if (playerInRange)
        {
            transform.Rotate(Vector3.forward, rotationSpeed);
            if (canShoot)
            {
                foreach (EnemyShoot firepoint in enemyFirepoints)
                {
                    firepoint.Shoot(firerate);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootPlayerRadius);
    }

}
