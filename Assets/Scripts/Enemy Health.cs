using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float deathDelay;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            if(gameObject.GetComponent<PimbaaMovement>() != null)
                gameObject.GetComponent<PimbaaMovement>().DeathStatus();

            if(gameObject.GetComponent<PinkFoeBehavior>() != null)
                gameObject.GetComponent<PinkFoeBehavior>().DeathStatus();

            animator.SetTrigger("Death");
            Destroy(gameObject, deathDelay);
        }
    }
}
