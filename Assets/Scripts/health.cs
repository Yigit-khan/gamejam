using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Slider slider;

    public int health;
    public int maxHealth = 5;

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
            Destroy(gameObject);
        }
    }

}
