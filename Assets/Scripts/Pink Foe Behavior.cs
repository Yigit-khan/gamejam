using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFoeBehavior : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject tentacle;
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private float attackCooldown;

    private bool isDead = false;
    private bool inCooldown = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player" && !inCooldown && !isDead)
        {
            animator.SetTrigger("Attack");
            GameObject newTentacle = Instantiate(tentacle, transform.position + new Vector3(2.25f * transform.localScale.x, 0), Quaternion.identity);
            newTentacle.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            StartCoroutine(AttackCooldown());
        }
    }
    
    IEnumerator AttackCooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        inCooldown = false;
    }

    public void DeathStatus()
    {
        isDead = true;
    }
}
