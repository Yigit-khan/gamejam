using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement playerMovement;
    public Health health;

    [Header("Settings")]
    [SerializeField] private int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement.Knockback();
            if(collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockbackDirection(true);
            }
            if(collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockbackDirection(false);
            }
            health.TakeDamage(damage);
        }
    }
}
