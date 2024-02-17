using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Slider slider;
    [SerializeField] private GameManager gameManager;

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
            StartCoroutine(PlayerDies());
            gameObject.GetComponent<PlayerMovement>().enabled = false;
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

    IEnumerator PlayerDies()
    {
        yield return new WaitForSeconds(deathDelay-.1f);
        gameManager.PlayerIsDead();
    }

}
