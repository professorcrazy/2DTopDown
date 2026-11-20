using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float contactDamage = 10f;
    public bool dieOnContact = false;
    public float attackCooldown = 1f;
    bool canAttack = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player") && canAttack)
        if (collision.gameObject.GetComponent<IHealth>() != null && canAttack)
        {
            collision.gameObject.GetComponent<IHealth>()?.TakeDamage(contactDamage);
            if (dieOnContact)
            {
                Destroy(this.transform.root.gameObject);
            }
            StartCoroutine(AttackCooldown(attackCooldown));
        }
    }

    IEnumerator AttackCooldown(float delay)
    {
        canAttack = false;
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
}
