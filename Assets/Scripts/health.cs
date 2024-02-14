using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Slider slider;

    [Header("Settings")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 5;

    void Start()
    {
        health = maxHealth;
        slider.value = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        slider.value = health;
        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(gameObject,0.4f);
        }
    }

}
