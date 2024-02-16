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
    [SerializeField] private int maxHealth;
    [SerializeField] private float deathDelay;

    private int health;

    void Start()
    {
        health = maxHealth;
        slider.value = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        slider.value = health;
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            animator.SetTrigger("Death");
            FindObjectOfType<AudioManager>().Play("PlayerDie");
            Destroy(gameObject,deathDelay);
        }
    }

    public void TakeHeal()
    {
        if(health != maxHealth)
        {
            health++;
            slider.value = health;
        }
    }

}
