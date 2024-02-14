using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().Knockback();
            if(collision.transform.position.x <= transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerMovement>().KnockbackDirection(true);
            }
            if(collision.transform.position.x > transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerMovement>().KnockbackDirection(true);
            }
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

    }
}
