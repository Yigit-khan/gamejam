using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int damage;

    void Start()
    {
        Attack();

        Destroy(gameObject, 1f);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        StartCoroutine(AttackDuration());
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(.25f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().Knockback();
            if (collision.transform.position.x <= transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerMovement>().KnockbackDirection(true);
            }
            if (collision.transform.position.x > transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerMovement>().KnockbackDirection(false);
            }
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
