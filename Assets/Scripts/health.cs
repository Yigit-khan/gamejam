using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public int health;
    public int maxHealth = 5;

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
