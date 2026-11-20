using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float huntPlayerRadius = 20f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed = 4f;
    Rigidbody2D rb;
    bool huntingPlayer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D playerRef = Physics2D.OverlapCircle(transform.position, huntPlayerRadius, playerLayer);
        huntingPlayer = playerRef != null;
        if (huntingPlayer)
        {
            Vector2 direction = (playerRef.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, huntPlayerRadius);
    }
}
